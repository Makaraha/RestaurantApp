using Domain;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders
{
    public class OrderViewModel : ReactiveObject
    {
        private Order _order;

        public OrderViewModel(Order order) 
        {
            _order = order;

            OrderTitle = $"Order #{order.Id}";
            OrderTime = order.OrderTime.ToString("dd-MM-yyyy HH:mm");
        }

        public string OrderTitle { get; set; }

        public string OrderTime { get; set; }
    }
}
