using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Dishes.Ingredients;
using AvaloniaApplication.ViewModels.Tabs.DishTypes;
using AvaloniaApplication.ViewModels.Tabs.Products;
using Domain;
using Domain.Services;
using DynamicData;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Dishes
{
    public class DishesViewModel : BaseEntitiesViewModel<Dish, DishViewModel>
    {
        private DishTypesViewModel _dishTypesViewModel;
        private ProductsViewModel _productsViewModel;
        private IngredientsViewModel _ingredientsViewModel;
        private IRepository<Ingredient> _ingredientsRepository;
        private List<Ingredient> _ingredients;

        private bool _isIngredientsVisible;
        private bool _isDishesVisible;

        public DishesViewModel(IRepository<Dish> repository, 
            IRepository<Ingredient> ingredientsRepository,
            ProductsViewModel productsViewModel, 
            DishTypesViewModel dishTypeViewModel) : base(repository)
        {
            _dishTypesViewModel = dishTypeViewModel;
            _ingredientsRepository = ingredientsRepository;
            _productsViewModel = productsViewModel;

            IsIngredientsVisible = false;
            IsDishesVisible = true;

            Initialize();
        }

        public bool IsDishesVisible
        {
            get => _isDishesVisible;
            set
            {
                if (_isDishesVisible == value)
                    return;

                _isDishesVisible = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsIngredientsVisible
        {
            get => _isIngredientsVisible;
            set
            {
                if (_isIngredientsVisible == value)
                    return;

                _isIngredientsVisible = value;
                this.RaisePropertyChanged();
            }
        }

        public IngredientsViewModel? IngredientsViewModel
        {
            get => _ingredientsViewModel;
            set
            {
                if (value == null || _ingredientsViewModel == value)
                    return;

                _ingredientsViewModel = value;
                this.RaisePropertyChanged();
            }
        }

        protected override async void Initialize()
        {
            await _dishTypesViewModel.WaitForInitializationAsync();

            _ingredients = await _ingredientsRepository.ListAsync();

            base.Initialize();
        }

        protected override DishViewModel CreateEntityViewModel(Dish entity)
        {
            var ingredientsViewModel = new IngredientsViewModel(_productsViewModel, entity, _ingredientsRepository, this);
            ingredientsViewModel.OnDeleted += (viewModel) => _ingredients.RemoveMany(_ingredients.Where(x => x.Id == viewModel.Id));
            ingredientsViewModel.OnInserted += (entity) => _ingredients.Add(entity);

            return new DishViewModel(entity, _dishTypesViewModel, this, ingredientsViewModel, _repository);
        }

        protected override Dish CreateNewEntity()
        {
            if (!_dishTypesViewModel.Entities.Any())
                throw new Exception("Impossible to create new dish without any dish type. Create dish type first");

            return new Dish()
            {
                Name = $"Dish {Entities.Count + 1}",
                DishTypeId = _dishTypesViewModel.Entities.First().Id
            };
        }

        public IEnumerable<Ingredient> GetIngredients(int dishId)
        {
            return _ingredients.Where(x => x.DishId == dishId);
        }

        public void ShowIngredients(IngredientsViewModel viewModel)
        {
            IngredientsViewModel = viewModel;

            IsIngredientsVisible = true;
            IsDishesVisible = false;
        }

        public void ShowDishes()
        {
            IsDishesVisible = true;
            IsIngredientsVisible = false;
        }
    }
}
