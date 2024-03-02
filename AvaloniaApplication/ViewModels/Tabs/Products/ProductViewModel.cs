using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Commands.Products;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using Domain;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Products
{
    public class ProductViewModel : ReactiveObject
    {
        private MeasurementUnitsViewModel _measurementUnitsViewModel;
        private IMediator _mediator;

        public ProductViewModel(IMediator mediator, Product product, MeasurementUnitsViewModel measurementUnits)
        {
            _measurementUnitsViewModel = measurementUnits;
            _mediator = mediator;

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
                await _mediator.Send(new UpdateProductCommand()
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
                this.RaisePropertyChanged(nameof(Name));
                this.RaisePropertyChanged(nameof(MeasurementUnit.Id));
            }
        }

        private async Task DeleteProduct()
        {
            await _mediator.Send(new DeleteProductCommand()
            {
                ProductId = Id
            });

            OnDeleted?.Invoke(this);
        }
    }
}
