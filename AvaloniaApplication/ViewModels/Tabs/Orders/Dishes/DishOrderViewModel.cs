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
        private DishOrdersViewModel _dishOrders;

        public DishOrderViewModel(DishOrder entity, DishOrdersViewModel dishOrders, DishesViewModel dishes, IRepository<DishOrder> repository) : base(entity, repository)
        {
            _dishes = dishes;
            _dishOrders = dishOrders;
        }

        public ObservableCollection<DishViewModel> AvailableDishes => new ObservableCollection<DishViewModel>(
            _dishes.Entities.Except(_dishOrders.Entities.Select(x => x.Dish)).Append(Dish));

        public int Id => _entity.Id;

        public DishViewModel Dish
        {
            get => _dishes.Entities.First(x => x.Id == _entity.DishId);
            set
            {
                if (value == null || _entity.DishId == value.Id)
                    return;

                UpdateEntity(_entity with { DishId = value.Id });
            }
        }

        public float Amount
        {
            get => _entity.Amount;
            set
            {
                if (value == _entity.Amount)
                    return;

                UpdateEntity(_entity with { Amount = value });
            }
        }

        public decimal Cost => Dish.TotalCost;

        public decimal TotalCost => Cost * (decimal)Amount;

        public string DishType => Dish.DishType.Name;

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Dish));
            this.RaisePropertyChanged(nameof(Cost));
            this.RaisePropertyChanged(nameof(DishType));
            this.RaisePropertyChanged(nameof(Amount));
            this.RaisePropertyChanged(nameof(TotalCost));

            _dishOrders.RaiseUpdate();
        }
    }
}
