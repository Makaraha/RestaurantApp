using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Products
{
    public class ProductViewModel : BaseEntityViewModel<Product>
    {
        private MeasurementUnitsViewModel _measurementUnitsViewModel;
        private ProductsViewModel _productsViewModel;

        public ProductViewModel(IRepository<Product> repository, Product product, ProductsViewModel productsViewModel, MeasurementUnitsViewModel measurementUnits) 
            : base(product, repository)
        {
            _measurementUnitsViewModel = measurementUnits;
            _productsViewModel = productsViewModel;
        }

        public int Id => _entity.Id;

        public string Name
        {
            get => _entity.Name;
            set
            {
                if(_entity.Name == value) 
                    return;

                UpdateEntity(_entity with { Name = value });
            }
        }

        public decimal Cost
        {
            get => _entity.Cost;
            set
            {
                if(_entity.Cost == value) 
                    return;

                UpdateEntity(_entity with { Cost = value });
            }
        }

        public ObservableCollection<MeasurementUnitViewModel> AvailableMeasurementUnits => _measurementUnitsViewModel.Entities;

        public int MeasurementUnitId => _entity.MeasurementUnitId;

        public MeasurementUnitViewModel MeasurementUnit
        {
            get => _measurementUnitsViewModel.Entities.First(x => x.Id == _entity.MeasurementUnitId);
            set
            {
                if (value == null || _entity.MeasurementUnitId == value.Id)
                    return;

                UpdateEntity(_entity with { MeasurementUnitId = value.Id });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Id));
            this.RaisePropertyChanged(nameof(Name));
            this.RaisePropertyChanged(nameof(MeasurementUnit));
            this.RaisePropertyChanged(nameof(Cost));
        }
    }
}
