using System;
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

        public DishViewModel(Dish dish, 
            DishTypesViewModel dishTypesViewModel, 
            IRepository<Dish> repository,
            Action<DishViewModel> showIngredientsAction) : base(dish, repository)
        {
            _dishTypesViewModel = dishTypesViewModel;

            ShowIngredientsCommand = ReactiveCommand.Create(() => showIngredientsAction(this));
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

        public ObservableCollection<DishTypeViewModel> AvailableDishTypes => _dishTypesViewModel.Entities;

        public DishTypeViewModel? DishType
        {
            get => AvailableDishTypes.FirstOrDefault(x => x.Id == _entity.DishTypeId);
            set
            {
                if (value == null || value.Id == _entity.DishTypeId)
                    return;

                UpdateEntity(_entity with { DishTypeId = value.Id });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Name));
            this.RaisePropertyChanged(nameof(DishType));
        }
    }
}
