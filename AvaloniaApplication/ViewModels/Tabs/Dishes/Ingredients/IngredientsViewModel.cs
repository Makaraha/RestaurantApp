using System;
using System.Collections.Generic;
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
        private ProductsViewModel _products;
        private DishViewModel _dish;

        public IngredientsViewModel(ProductsViewModel products, DishViewModel dish, IRepository<Ingredient> repository, IEnumerable<Ingredient> ingredients,
            Action showDishesAction) : base(repository)
        {
            _products = products;
            _dish = dish;
            Entities = new ObservableCollection<IngredientViewModel>(ingredients.Select(CreateSubscribedEntityViewModel));

            BackCommand = ReactiveCommand.Create(showDishesAction);
            Title = $"Ingredients of '{dish.Name}'";

            Initialize();
        }

        public string Title { get; }

        public ICommand BackCommand { get; }

        protected override async void Initialize()
        {
            await _products.WaitForInitializationAsync();

            _initializationTcs.SetResult();
        }

        protected override IngredientViewModel CreateEntityViewModel(Ingredient entity)
        {
            return new IngredientViewModel(entity, _products, _repository);
        }

        protected override Ingredient CreateNewEntity()
        {
            var product = _products.Entities.Where(x => !Entities.Select(ingr => ingr?.Product?.Id).Contains(x.Id)).FirstOrDefault();

            if (product == null)
                throw new Exception("Impossible to create new ingredient");

            return new Ingredient() { ProductId = product.Id, DishId = _dish.Id, Amount = 1 };
        }
    }
}
