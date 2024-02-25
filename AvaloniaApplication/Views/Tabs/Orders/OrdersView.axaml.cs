using Avalonia.Controls;
using AvaloniaApplication.ViewModels.Tabs.Orders;

namespace AvaloniaApplication.Views.Tabs.Orders
{
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            DataContext = new OrdersViewModel();
            InitializeComponent();
        }
    }
}
