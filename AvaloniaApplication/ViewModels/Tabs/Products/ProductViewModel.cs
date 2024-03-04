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

        public ProductViewModel(IRepository<Product> repository, Product product, MeasurementUnitsViewModel measurementUnits) 
            : base(product, repository)
        {
            _measurementUnitsViewModel = measurementUnits;
        }

        public int Id => _entity.Id;

        public string Name
        {
            get => _entity.Name;
            set
            {
                if(_entity.Name == value) 
                    return;

                UpdateEntity(new Product()
                {
                    Id = Id,
                    Name = value,
                    MeasurementUnitId = MeasurementUnit.Id
                });
            }
        }

        public ObservableCollection<MeasurementUnitViewModel> AvailableMeasurementUnits => _measurementUnitsViewModel.Entities;

        public MeasurementUnitViewModel MeasurementUnit
        {
            get => AvailableMeasurementUnits.First(x => x.Id == _entity.MeasurementUnitId);
            set
            {
                if (value == null || _entity.MeasurementUnitId == value.Id)
                    return;

                UpdateEntity(new Product() 
                { 
                    Id = Id, 
                    Name = Name, 
                    MeasurementUnitId = value.Id
                });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Id));
            this.RaisePropertyChanged(nameof(Name));
            this.RaisePropertyChanged(nameof(MeasurementUnit));
        }
    }
}
