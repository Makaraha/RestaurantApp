using System;
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

        public IngredientViewModel(Ingredient entity, ProductsViewModel products, IRepository<Ingredient> repository) : base(entity, repository)
        {
            _products = products;
        }

        public ObservableCollection<ProductViewModel> AvailableProducts => _products.Entities;

        public int Id => _entity.Id;

        public ProductViewModel? Product
        {
            get => AvailableProducts.FirstOrDefault(x => x.Id == _entity.ProductId);
            set
            {
                if (value == null || _entity.DishId == value.Id)
                    return;

                UpdateEntity(_entity with { DishId = value.Id });
            }
        }

        public string? ProductMeasurementUnit => Product?.MeasurementUnit?.Name;

        public float Amount
        {
            get => _entity.Amount;
            set
            {
                if (_entity.Amount != value)
                    return;

                UpdateEntity(_entity with { Amount = value });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Product));
            this.RaisePropertyChanged(nameof(ProductMeasurementUnit));
        }
    }
}
