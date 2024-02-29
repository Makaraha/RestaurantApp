using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using AvaloniaApplication.ViewModels.Tabs.Orders;
using AvaloniaApplication.ViewModels.Tabs.Products;

namespace AvaloniaApplication.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(OrdersViewModel ordersViewModel, 
            MeasurementUnitsViewModel measurementUnitsViewModel,
            ProductsViewModel productsViewModel) 
        {
            OrdersViewModel = ordersViewModel;
            MeasurementUnitsViewModel = measurementUnitsViewModel;
            ProductsViewModel = productsViewModel;
        }

        public OrdersViewModel OrdersViewModel { get; }

        public MeasurementUnitsViewModel MeasurementUnitsViewModel { get; }

        public ProductsViewModel ProductsViewModel { get; }
    }
}
