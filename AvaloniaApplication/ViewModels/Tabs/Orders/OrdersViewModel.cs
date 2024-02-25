using System;
using System.Collections.ObjectModel;
using Domain;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders
{
    public class OrdersViewModel : ReactiveObject
	{
        private IMediator _mediator;

		public OrdersViewModel(IMediator mediator)
		{
           _mediator = mediator;
		}

		public ObservableCollection<OrderViewModel> Orders => new ObservableCollection<OrderViewModel>
            {
                new OrderViewModel(new Order { Id = 1, OrderTime = DateTime.Now }),
                new OrderViewModel(new Order { Id = 2, OrderTime = DateTime.Now }),
                new OrderViewModel(new Order { Id = 1, OrderTime = DateTime.Now }),
                new OrderViewModel(new Order { Id = 2, OrderTime = DateTime.Now }),
            };

        public string Test => "Hello test";
    }
}