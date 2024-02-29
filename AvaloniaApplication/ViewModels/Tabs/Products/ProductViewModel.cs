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
            Name = product.Name;
            MeasurementUnitId = product.MeasurementUnitId;

            DeleteProductCommand = ReactiveCommand.Create(async () => await DeleteProduct());
            MeasurementUnit = AvailableMeasurementUnits.FirstOrDefault();
        }

        public event Action<ProductViewModel>? OnDeleted;

        public int Id { get; }

        public int MeasurementUnitId { get; set; }

        public string Name { get; set; }

        public ObservableCollection<MeasurementUnitViewModel> AvailableMeasurementUnits => _measurementUnitsViewModel.MeasurementUnits;

        public MeasurementUnitViewModel? MeasurementUnit { get; set; }

        public ICommand DeleteProductCommand { get; set; }

        private async Task UpdateProduct()
        {
            
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
