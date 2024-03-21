using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Dishes.Ingredients;
using AvaloniaApplication.ViewModels.Tabs.DishTypes;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Dishes
{
    public class DishViewModel : BaseEntityViewModel<Dish>
    {
        private DishTypesViewModel _dishTypesViewModel;
        private DishesViewModel _dishesViewModel;
        private IngredientsViewModel _ingreidentsViewModel;

        public DishViewModel(Dish dish, 
            DishTypesViewModel dishTypesViewModel, 
            DishesViewModel dishesViewModel,
            IngredientsViewModel ingredientsViewModel,
            IRepository<Dish> repository) : base(dish, repository)
        {
            _dishTypesViewModel = dishTypesViewModel;
            _dishesViewModel = dishesViewModel;
            _ingreidentsViewModel = ingredientsViewModel;

            ShowIngredientsCommand = ReactiveCommand.Create(() => _dishesViewModel.ShowIngredients(_ingreidentsViewModel));
        }

        public ICommand ShowIngredientsCommand { get; }

        public int Id => _entity.Id;

        public string Name
        {
            get => _entity.Name;
            set
            {
                UpdateEntity(_entity with { Name = value });
            }
        }

        public ObservableCollection<DishTypeViewModel> AvailableDishTypes => new ObservableCollection<DishTypeViewModel>(
            _dishTypesViewModel.Entities
            .Where(dt => !_dishesViewModel.Entities.Any(d => d.Name == Name && d.DishTypeId == dt.Id))
            .Append(DishType));

        public int DishTypeId => _entity.DishTypeId;

        public DishTypeViewModel DishType
        {
            get => _dishTypesViewModel.Entities.First(x => x.Id == _entity.DishTypeId);
            set
            {
                if (value == null || value.Id == _entity.DishTypeId)
                    return;

                UpdateEntity(_entity with { DishTypeId = value.Id });
            }
        }

        public decimal ExtraCharge
        {
            get => _entity.ExtraCharge;
            set
            {
                if (value == _entity.ExtraCharge)
                    return;

                UpdateEntity(_entity with {  ExtraCharge = value });
            }
        }

        public decimal PrimeCost => _ingreidentsViewModel.PrimeCost;

        public decimal TotalCost => PrimeCost + ExtraCharge;

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Name));
            this.RaisePropertyChanged(nameof(DishType));
            this.RaisePropertyChanged(nameof(ExtraCharge));
            this.RaisePropertyChanged(nameof(PrimeCost));
            this.RaisePropertyChanged(nameof(TotalCost));
            
            foreach(var dish in _dishesViewModel.Entities.Where(x => x.Name == Name))
            {
                dish.RaisePropertyChanged(nameof(AvailableDishTypes));
            }
        }
    }
}
