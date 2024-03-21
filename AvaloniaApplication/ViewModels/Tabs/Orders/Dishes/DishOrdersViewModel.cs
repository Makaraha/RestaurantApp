using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Dishes;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders.Dishes
{
    public class DishOrdersViewModel : BaseEntitiesViewModel<DishOrder, DishOrderViewModel>
    {
        private DishesViewModel _dishes;
        private Order _order;
        private OrdersViewModel _orders;

        public DishOrdersViewModel(DishesViewModel dishesViewModel, OrdersViewModel orders, Order order, IRepository<DishOrder> repository) : base(repository)
        {
            _dishes = dishesViewModel;
            _order = order;
            _orders = orders;

            BackCommand = ReactiveCommand.Create(orders.ShowOrders);
            Title = $"Dishes of order #'{order.Id}'";

            Initialize();
        }

        public string Title { get; }

        public decimal TotalCost => Entities.Any() ? Entities.Sum(x => x.TotalCost) : 0;

        public string TotalCostText => $"Total cost: {TotalCost}";

        public ICommand BackCommand { get; }

        protected override async void Initialize()
        {
            await _dishes.WaitForInitializationAsync();

            Entities = new ObservableCollection<DishOrderViewModel>(_orders.GetDishOrders(_order.Id).Select(CreateSubscribedEntityViewModel));

            _initializationTcs.SetResult();
        }

        protected override DishOrderViewModel CreateEntityViewModel(DishOrder entity)
        {
            return new DishOrderViewModel(entity, this, _dishes, _repository);
        }

        protected override DishOrder CreateNewEntity()
        {
            var dish = _dishes.Entities.Where(x => !Entities.Select(dishOrder => dishOrder?.Dish?.Id).Contains(x.Id)).FirstOrDefault();

            if (dish == null)
                throw new Exception("This dish alerady in order");

            return new DishOrder()
            {
                OrderId = _order.Id,
                DishId = dish.Id,
                Amount = 1
            };
        }

        public override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(TotalCostText));

            _orders.Entities.First(x => x.Id == _order.Id).RaisePropertyChanged(nameof(OrderViewModel.Cost));

            base.RaiseUpdate();
        }
    }
}
