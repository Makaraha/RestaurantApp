using System;
using System.Collections.ObjectModel;
using Domain;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders
{
    public class OrdersViewModel : ReactiveObject
	{
		public OrdersViewModel()
		{
           
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