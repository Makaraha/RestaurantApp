using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.DishTypes;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Dishes
{
    public class DishViewModel : BaseEntityViewModel<Dish>
    {
        private DishTypesViewModel _dishTypesViewModel;

        public DishViewModel(Dish dish, DishTypesViewModel dishTypesViewModel, IRepository<Dish> repository) : base(dish, repository)
        {
            _dishTypesViewModel = dishTypesViewModel;
        }

        public int Id => _entity.Id;

        public string Name
        {
            get => _entity.Name;
            set
            {
                UpdateEntity(new Dish()
                {
                    Id = Id,
                    Name = value,
                    DishTypeId = 1
                });
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
