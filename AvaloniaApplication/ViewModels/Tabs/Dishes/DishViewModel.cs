using AvaloniaApplication.ViewModels.BaseViewModels;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.Dishes
{
    public class DishViewModel : BaseEntityViewModel<Dish>
    {
        public DishViewModel(Dish dish, IRepository<Dish> repository) : base(dish, repository)
        { }

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

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Name));
        }
    }
}
