using AvaloniaApplication.ViewModels.Tabs.Orders;

namespace AvaloniaApplication.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(OrdersViewModel ordersViewModel) 
        {
            OrdersViewModel = ordersViewModel;
        }

        public OrdersViewModel OrdersViewModel { get; }
    }
}
