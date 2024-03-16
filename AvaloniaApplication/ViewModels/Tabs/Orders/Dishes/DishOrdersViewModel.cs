using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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
        private OrderViewModel _order;

        public DishOrdersViewModel(DishesViewModel dishesViewModel, OrderViewModel order, IRepository<DishOrder> repository, IEnumerable<DishOrder> dishOrders,
            Action showDishesAction) : base(repository)
        {
            _dishes = dishesViewModel;
            _order = order;
            Entities = new ObservableCollection<DishOrderViewModel>(dishOrders.Select(CreateSubscribedEntityViewModel));

            BackCommand = ReactiveCommand.Create(showDishesAction);
            Title = $"Dishes of order #'{order.Id}'";

            Initialize();
        }

        public string Title { get; }

        public ICommand BackCommand { get; }

        protected override async void Initialize()
        {
            await _dishes.WaitForInitializationAsync();

            _initializationTcs.SetResult();
        }

        protected override DishOrderViewModel CreateEntityViewModel(DishOrder entity)
        {
            return new DishOrderViewModel(entity, _dishes, _repository);
        }

        protected override DishOrder CreateNewEntity()
        {
            var dish = _dishes.Entities.Where(x => !Entities.Select(dishOrder => dishOrder?.Dish?.Id).Contains(x.Id)).FirstOrDefault();

            if (dish == null)
                throw new Exception("This dish alerady in order");

            return new DishOrder()
            {
                OrderId = _order.Id,
                DishId = dish.Id
            };
        }
    }
}
