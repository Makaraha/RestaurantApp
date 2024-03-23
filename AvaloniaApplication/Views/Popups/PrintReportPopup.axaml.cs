using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using AvaloniaApplication.ViewModels.Tabs.Orders;
using Infrastructure.Models;
using Infrastructure.Services;

namespace AvaloniaApplication.Views.Popups
{
    public partial class PrintReportPopup : Window
    {
        private OrdersViewModel _ordersViewModel;
        private ReportService _reportService;

        public PrintReportPopup()
        {
            InitializeComponent();
        }

        public PrintReportPopup(OrdersViewModel ordersViewModel, ReportService reportService)
        {
            _ordersViewModel = ordersViewModel;
            _reportService = reportService;

            var entities = _ordersViewModel.Entities.OrderBy(x => x.OrderData).ToList();

            InitializeComponent();

            Calendar.DisplayDateStart = entities.Min(x => x.OrderData).DateTime;
            Calendar.DisplayDateEnd = entities.Max(x => x.OrderData).DateTime;
            Calendar.IsTodayHighlighted = false;

            for (int i = 0; i < entities.Count - 1; i++)
            {
                if (entities[i + 1].OrderData.Date - entities[i].OrderData.Date > TimeSpan.FromDays(1))
                    Calendar.BlackoutDates.Add(new CalendarDateRange(entities[i].OrderData.Date.AddDays(1), entities[i + 1].OrderData.Date.AddDays(-1)));
            }

            ReportButton.Click += (_, _) => PrintReport();
        }

        private void PrintReport()
        {
            var orders = _ordersViewModel.Entities.Where(x => Calendar.SelectedDates.Contains(x.OrderData.Date))
                .Select(x => new OrderModel()
                {
                    Name = x.Name,
                    TotalCost = x.Cost,
                    Dishes = x.Dishes.Select(d => new DishModel()
                    {
                        Name = d.Dish.Name,
                        Type = d.Dish.DishType.Name,
                        Amount = d.Amount,
                        Cost = d.Cost
                    }).ToList()
                });

            var path = _reportService.GeneratePdfReport(orders);

            var process = new Process();
            process.StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true
            };
            process.Start();
        }
    }
}
