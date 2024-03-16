using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.Dishes;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Orders.Dishes
{
    public class DishOrderViewModel : BaseEntityViewModel<DishOrder>
    {
        private DishesViewModel _dishes;

        public DishOrderViewModel(DishOrder entity, DishesViewModel dishes, IRepository<DishOrder> repository) : base(entity, repository)
        {
            _dishes = dishes;
        }

        public ObservableCollection<DishViewModel> AvailableDishes => _dishes.Entities;

        public int Id => _entity.Id;

        public DishViewModel? Dish
        {
            get => AvailableDishes.FirstOrDefault(x => x.Id == _entity.DishId);
            set
            {
                if (value == null || _entity.DishId == value.Id)
                    return;

                UpdateEntity(_entity with { DishId = value.Id });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Dish));
        }
    }
}
