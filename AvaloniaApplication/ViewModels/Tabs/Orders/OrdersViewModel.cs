using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Dishes;
using AvaloniaApplication.ViewModels.Tabs.Orders.Dishes;
using Domain;
using Domain.Services;
using DynamicData;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders
{
    public class OrdersViewModel : BaseEntitiesViewModel<Order, OrderViewModel>
    {
        private DishesViewModel _dishesViewModel;
        private DishOrdersViewModel _orderDishesViewModel;
        private IRepository<DishOrder> _dishOrdersRepository;
        private List<DishOrder> _dishOrders;

        private bool _isDishOrdersVisible;
        private bool _isOrdersVisible;

        public OrdersViewModel(IRepository<Order> repository,
            IRepository<DishOrder> dishOrdersRepository,
            DishesViewModel dishesViewModel) : base(repository)
        {
            _dishOrdersRepository = dishOrdersRepository;
            _dishesViewModel = dishesViewModel;

            IsDishOrdersVisible = false;
            IsOrdersVisible = true;

            Initialize();
        }

        public bool IsOrdersVisible
        {
            get => _isOrdersVisible;
            set
            {
                if (_isOrdersVisible == value)
                    return;

                _isOrdersVisible = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsDishOrdersVisible
        {
            get => _isDishOrdersVisible;
            set
            {
                if (_isDishOrdersVisible == value)
                    return;

                _isDishOrdersVisible = value;
                this.RaisePropertyChanged();
            }
        }

        public DishOrdersViewModel? OrderDishesViewModel
        {
            get => _orderDishesViewModel;
            set
            {
                if (value == null || _orderDishesViewModel == value)
                    return;

                _orderDishesViewModel = value;
                this.RaisePropertyChanged();
            }
        }

        protected override async void Initialize()
        {
            await _dishesViewModel.WaitForInitializationAsync();

            _dishOrders = await _dishOrdersRepository.ListAsync();

            base.Initialize();
        }

        protected override OrderViewModel CreateEntityViewModel(Order entity)
        {
            var orderDishesViewModel = new DishOrdersViewModel(_dishesViewModel, this, entity, _dishOrdersRepository);
            orderDishesViewModel.OnDeleted += (viewModel) => _dishOrders.RemoveMany(_dishOrders.Where(x => x.Id == viewModel.Id));
            orderDishesViewModel.OnInserted += (entity) => _dishOrders.Add(entity);

            return new OrderViewModel(entity, _repository, orderDishesViewModel, this);
        }

        protected override Order CreateNewEntity() 
        {
            return new Order()
            {
                OrderTime = DateTime.UtcNow
            };
        }

        public IEnumerable<DishOrder> GetDishOrders(int orderId)
        {
            return _dishOrders.Where(x => x.OrderId == orderId);
        }

        public void ShowDishOrders(DishOrdersViewModel viewModel)
        {
            OrderDishesViewModel = viewModel;

            IsDishOrdersVisible = true;
            IsOrdersVisible = false;
        }

        public void ShowOrders()
        {
            IsOrdersVisible = true;
            IsDishOrdersVisible = false;
        }
    }
}