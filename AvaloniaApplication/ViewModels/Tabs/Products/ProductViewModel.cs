using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Products
{
    public class ProductViewModel : ReactiveObject
    {
        private MeasurementUnitsViewModel _measurementUnitsViewModel;
        private IRepository<Product> _repository;

        public ProductViewModel(IRepository<Product> repository, Product product, MeasurementUnitsViewModel measurementUnits)
        {
            _measurementUnitsViewModel = measurementUnits;
            _repository = repository;

            Id = product.Id;
            _name = product.Name;

            DeleteProductCommand = ReactiveCommand.Create(async () => await DeleteProduct());
            _measurementUnit = AvailableMeasurementUnits.First(x => x.Id == product.MeasurementUnitId);
        }

        public event Action<ProductViewModel>? OnDeleted;

        public int Id { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if(_name == value) 
                    return;

                UpdateProduct(value, _measurementUnit);
            }
        }

        public ObservableCollection<MeasurementUnitViewModel> AvailableMeasurementUnits => _measurementUnitsViewModel.MeasurementUnits;

        private MeasurementUnitViewModel _measurementUnit;
        public MeasurementUnitViewModel MeasurementUnit
        {
            get => _measurementUnit;
            set
            {
                if (_measurementUnit == value)
                    return;

                UpdateProduct(_name, value);
            }
        }

        public ICommand DeleteProductCommand { get; set; }

        private async void UpdateProduct(string name, MeasurementUnitViewModel measurementUnit)
        {
            try
            {
                await _repository.UpdateAsync(new Product()
                {
                    Id = Id,
                    Name = name,
                    MeasurementUnitId = measurementUnit.Id,
                });

                _name = name;
                _measurementUnit = measurementUnit;
            }
            catch { }
            finally
            {
                this.RaisePropertyChanged(nameof(MeasurementUnit));
                this.RaisePropertyChanged(nameof(Name));
            }
        }

        private async Task DeleteProduct()
        {
            await _repository.DeleteAsync(Id);
            OnDeleted?.Invoke(this);
        }
    }
}
