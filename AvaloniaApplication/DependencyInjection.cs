using System.Reflection;
using AvaloniaApplication.ViewModels;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using AvaloniaApplication.ViewModels.Tabs.Orders;
using AvaloniaApplication.ViewModels.Tabs.Products;
using AvaloniaApplication.Views;
using AvaloniaApplication.Views.Tabs.MeasurementUnits;
using AvaloniaApplication.Views.Tabs.Orders;
using AvaloniaApplication.Views.Tabs.Products;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApplication
{
    public static class DependencyInjection
    {
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MeasurementUnitsViewModel>();
            services.AddSingleton<ProductsViewModel>();
            services.AddSingleton<OrdersViewModel>();
            services.AddSingleton<MainViewModel>();
        }

        public static void AddViews(this IServiceCollection services)
        {
            services.AddSingleton<MeasurementUnitsView>();
            services.AddSingleton<ProductsView>();
            services.AddSingleton<OrdersView>();
            services.AddSingleton<MainView>();

            services.AddSingleton<MainWindow>();
        }
    }
}
