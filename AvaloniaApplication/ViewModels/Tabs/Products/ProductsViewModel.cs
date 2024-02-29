using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Commands.Products;
using Application.Queries.Products;
using Avalonia.Metadata;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using Domain;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Products
{
    public class ProductsViewModel : ReactiveObject
    {
        private IMediator _mediator;
        private MeasurementUnitsViewModel _measurementUnitsViewModel;

        public ProductsViewModel(IMediator mediator, MeasurementUnitsViewModel measurementUnitsViewModel)
        {
            _mediator = mediator;
            _measurementUnitsViewModel = measurementUnitsViewModel;

            Initialize();
        }

        private async void Initialize()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            AddProductCommand = ReactiveCommand.Create(async () => await AddProduct());

            await _measurementUnitsViewModel.WaitForInitializationAsync();
            Products = new ObservableCollection<ProductViewModel>(products.Select(x => CreateProductViewModel(x)));
        }

        public ObservableCollection<ProductViewModel> Products { get; set; } = new ObservableCollection<ProductViewModel>();

        public ICommand AddProductCommand { get; private set; }

        private void WhenProductDeleted(ProductViewModel product)
        {
            Products.Remove(product);
        }

        private async Task AddProduct()
        {
            //TODO: message
            if (!_measurementUnitsViewModel.MeasurementUnits.Any())
                return;

            try
            {
                var name = $"Product {Products.Count + 1}";
                var measurementUnitId = _measurementUnitsViewModel.MeasurementUnits.First().Id;

                var product = await _mediator.Send(new AddProductCommand()
                {
                    MeasurementUnitId = measurementUnitId,
                    Name = name
                });

                Products.Add(CreateProductViewModel(product));
            }
            catch { }
        }

        private ProductViewModel CreateProductViewModel(Product product)
        {
            var viewModel = new ProductViewModel(_mediator, product, _measurementUnitsViewModel);
            viewModel.OnDeleted += WhenProductDeleted;
            return viewModel;
        }
    }
}
