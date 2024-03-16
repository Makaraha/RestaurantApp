using System;
using System.Windows.Input;
using AvaloniaApplication.ViewModels.BaseViewModels;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders
{
    public class OrderViewModel : BaseEntityViewModel<Order>
    {
        public OrderViewModel(Order order,
            IRepository<Order> repository,
            Action<OrderViewModel> showDishesAction) : base(order, repository)
        {
            ShowDishesCommand = ReactiveCommand.Create(() => showDishesAction(this));
        }

        public ICommand ShowDishesCommand { get; }

        public int Id => _entity.Id;

        public string Name => $"Order #{_entity.Id}";

        public DateTime OrderTime => _entity.OrderTime;

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Name));
            this.RaisePropertyChanged(nameof(OrderTime));
        }
    }
}
