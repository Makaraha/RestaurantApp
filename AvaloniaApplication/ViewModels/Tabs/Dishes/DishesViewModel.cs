using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Dishes.Ingredients;
using AvaloniaApplication.ViewModels.Tabs.DishTypes;
using AvaloniaApplication.ViewModels.Tabs.Products;
using AvaloniaApplication.Views.Tabs.Dishes;
using Domain;
using Domain.Interfaces;
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
            return new DishViewModel(entity, _dishTypesViewModel, _repository, ShowIngredients);
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

        private void ShowIngredients(DishViewModel dish)
        {
            IngredientsViewModel = new IngredientsViewModel(_productsViewModel, dish, _ingredientsRepository, _ingredients.Where(x => x.DishId == dish.Id), ShowDishes);
            IngredientsViewModel.OnDeleted += (viewModel) => _ingredients.RemoveMany(_ingredients.Where(x => x.Id == viewModel.Id));
            IngredientsViewModel.OnInserted += (entity) => _ingredients.Add(entity);
            
            IsIngredientsVisible = true;
            IsDishesVisible = false;
        }

        private void ShowDishes()
        {
            IsDishesVisible = true;
            IsIngredientsVisible = false;
        }
    }
}
