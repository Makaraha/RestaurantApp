using System;
using System.Linq;
using System.Windows.Input;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Orders.Dishes;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders
{
    public class OrderViewModel : BaseEntityViewModel<Order>
    {
        private OrdersViewModel _orders;
        private DishOrdersViewModel _dishOrders;

        public OrderViewModel(Order order,
            IRepository<Order> repository,
            DishOrdersViewModel dishOrders,
            OrdersViewModel orders) : base(order, repository)
        {
            _dishOrders = dishOrders;

            ShowDishesCommand = ReactiveCommand.Create(() => orders.ShowDishOrders(dishOrders));
        }

        public ICommand ShowDishesCommand { get; }

        public int Id => _entity.Id;

        public string Name => $"Order #{_entity.Id}";

        public decimal Cost => _dishOrders.Entities.Any() ? _dishOrders.Entities.Sum(x => x.TotalCost) : 0;

        public DateTimeOffset OrderData
        {
            get => _entity.OrderTime.ToLocalTime();
            set
            {
                if (_entity.OrderTime == value.UtcDateTime)
                    return;

                UpdateEntity(_entity with { OrderTime = value.UtcDateTime });
            }
        }

        public TimeSpan OrderTime
        {
            get => OrderData.TimeOfDay;
            set
            {
                if(OrderData.TimeOfDay == value) return;

                OrderData = OrderData.Date + value;
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Name));
            this.RaisePropertyChanged(nameof(OrderData));
            this.RaisePropertyChanged(nameof(OrderTime));
            this.RaisePropertyChanged(nameof(Cost));
        }
    }
}
