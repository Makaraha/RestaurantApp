using System;
using System.Linq;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using Domain;
using Domain.Services;

namespace AvaloniaApplication.ViewModels.Tabs.Products
{
    public class ProductsViewModel : BaseEntitiesViewModel<Product, ProductViewModel>
    {
        private MeasurementUnitsViewModel _measurementUnitsViewModel;

        public ProductsViewModel(IRepository<Product> repository, MeasurementUnitsViewModel measurementUnitsViewModel) : base(repository)
        {
            _measurementUnitsViewModel = measurementUnitsViewModel;

            Initialize();
        }

        protected override async void Initialize()
        {
            await _measurementUnitsViewModel.WaitForInitializationAsync();

            base.Initialize();
        }

        protected override ProductViewModel CreateEntityViewModel(Product entity)
        {
            return new ProductViewModel(_repository, entity, this, _measurementUnitsViewModel);
        }

        protected override Product CreateNewEntity()
        {
            if (!_measurementUnitsViewModel.Entities.Any())
                throw new Exception("It is impossible to create new product without any measurement unit. Create measurmenet unit first");

            return new Product()
            {
                Name = $"Product {Entities.Count + 1}",
                MeasurementUnitId = _measurementUnitsViewModel.Entities.First().Id,
                Cost = 0
            };
        }
    }
}
