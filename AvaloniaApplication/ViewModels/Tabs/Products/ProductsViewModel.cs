using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Queries.Products;
using AvaloniaApplication.ViewModels.Tabs.MeasurementUnits;
using Domain;
using Domain.Services;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Products
{
    public class ProductsViewModel : ReactiveObject
    {
        private IRepository<Product> _repository;
        private MeasurementUnitsViewModel _measurementUnitsViewModel;

        public ProductsViewModel(IRepository<Product> repository, MeasurementUnitsViewModel measurementUnitsViewModel)
        {
            _repository = repository;
            _measurementUnitsViewModel = measurementUnitsViewModel;

            Initialize();
        }

        private async void Initialize()
        {
            var products = await _repository.ListAsync();
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

                var product = await _repository.AddAsync(new Product()
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
            var viewModel = new ProductViewModel(_repository, product, _measurementUnitsViewModel);
            viewModel.OnDeleted += WhenProductDeleted;
            return viewModel;
        }
    }
}
