using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Products;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Dishes.Ingredients
{
    public class IngredientViewModel : BaseEntityViewModel<Ingredient>
    {
        private ProductsViewModel _products;
        private IngredientsViewModel _ingredients;

        public IngredientViewModel(Ingredient entity, IngredientsViewModel ingreidentsViewModel, ProductsViewModel products, IRepository<Ingredient> repository) : base(entity, repository)
        {
            _products = products;
            _ingredients = ingreidentsViewModel;
        }

        public ObservableCollection<ProductViewModel> AvailableProducts =>
            new ObservableCollection<ProductViewModel>(
            _products.Entities.Except(_ingredients.Entities.Select(x => x.Product)).Append(Product));

        public int Id => _entity.Id;

        public ProductViewModel Product
        {
            get => _products.Entities.First(x => x.Id == _entity.ProductId);
            set
            {
                if (value == null || _entity.ProductId == value.Id)
                    return;

                UpdateEntity(_entity with { ProductId = value.Id });
            }
        }

        public decimal ProductCost => Product.Cost;

        public string ProductMeasurementUnit => Product.MeasurementUnit.Name;

        public decimal ProductTotalCost => ProductCost * (decimal)Amount;

        public float Amount
        {
            get => _entity.Amount;
            set
            {
                if (_entity.Amount == value)
                    return;

                UpdateEntity(_entity with { Amount = value });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Product));
            this.RaisePropertyChanged(nameof(ProductMeasurementUnit));
            this.RaisePropertyChanged(nameof(ProductCost));
            this.RaisePropertyChanged(nameof(Amount));
            this.RaisePropertyChanged(nameof(ProductTotalCost));

            foreach (var ingreident in _ingredients.Entities)
                ingreident.RaisePropertyChanged(nameof(AvailableProducts));

            _ingredients.RaisePropertyChanged(nameof(_ingredients.PrimeCostText));
        }
    }
}
