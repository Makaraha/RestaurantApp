using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Products;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Dishes.Ingredients
{
    public class IngredientsViewModel : BaseEntitiesViewModel<Ingredient, IngredientViewModel>
    {
        private DishesViewModel _dishesViewModel;
        private ProductsViewModel _products;
        private Dish _dish;

        public IngredientsViewModel(ProductsViewModel products, Dish dish, IRepository<Ingredient> repository, DishesViewModel dishesViewModel) : base(repository)
        {
            _products = products;
            _dishesViewModel = dishesViewModel;
            _dish = dish;

            BackCommand = ReactiveCommand.Create(dishesViewModel.ShowDishes);
            Title = $"Ingredients of '{dish.Name}'";

            Initialize();
        }

        public string Title { get; }

        public ICommand BackCommand { get; }

        public decimal PrimeCost => Entities.Any() ? Entities.Sum(x => x.ProductTotalCost) : 0;

        public string PrimeCostText => $"Prime cost: {PrimeCost}";

        protected override async void Initialize()
        {
            await _products.WaitForInitializationAsync();

            Entities = new ObservableCollection<IngredientViewModel>(_dishesViewModel.GetIngredients(_dish.Id).Select(CreateSubscribedEntityViewModel));

            _initializationTcs.SetResult();
        }

        protected override IngredientViewModel CreateEntityViewModel(Ingredient entity)
        {
            return new IngredientViewModel(entity, this, _products, _repository);
        }

        protected override Ingredient CreateNewEntity()
        {
            var product = _products.Entities.Where(x => !Entities.Select(ingr => ingr?.Product?.Id).Contains(x.Id)).FirstOrDefault();

            if (product == null)
                throw new Exception("Impossible to create new ingredient");

            return new Ingredient() { ProductId = product.Id, DishId = _dish.Id, Amount = 1 };
        }

        public override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(PrimeCostText));

            _dishesViewModel.Entities.First(x => x.Id == _dish.Id).RaisePropertyChanged(nameof(DishViewModel.PrimeCost));
        }
    }
}
