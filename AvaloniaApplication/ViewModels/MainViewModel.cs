using AvaloniaApplication.ViewModels.Tabs.Dishes;
using AvaloniaApplication.ViewModels.Tabs.DishTypes;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using AvaloniaApplication.ViewModels.Tabs.Orders;
using AvaloniaApplication.ViewModels.Tabs.Products;

namespace AvaloniaApplication.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(OrdersViewModel ordersViewModel, 
            MeasurementUnitsViewModel measurementUnitsViewModel,
            ProductsViewModel productsViewModel,
            DishTypesViewModel dishTypesViewModel,
            DishesViewModel dishesViewModel) 
        {
            OrdersViewModel = ordersViewModel;
            MeasurementUnitsViewModel = measurementUnitsViewModel;
            ProductsViewModel = productsViewModel;
            DishTypesViewModel = dishTypesViewModel;
            DishesViewModel = dishesViewModel;
        }

        public OrdersViewModel OrdersViewModel { get; }

        public MeasurementUnitsViewModel MeasurementUnitsViewModel { get; }

        public ProductsViewModel ProductsViewModel { get; }

        public DishTypesViewModel DishTypesViewModel { get; }

        public DishesViewModel DishesViewModel { get; }
    }
}
