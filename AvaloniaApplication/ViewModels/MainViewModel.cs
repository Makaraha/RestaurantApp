using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using AvaloniaApplication.ViewModels.Tabs.Orders;

namespace AvaloniaApplication.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(OrdersViewModel ordersViewModel, 
            MeasurementUnitsViewModel measurementUnitsViewModel) 
        {
            OrdersViewModel = ordersViewModel;
            MeasurementUnitsViewModel = measurementUnitsViewModel;
        }

        public OrdersViewModel OrdersViewModel { get; }

        public MeasurementUnitsViewModel MeasurementUnitsViewModel { get; }
    }
}
