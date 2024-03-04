using AvaloniaApplication.ViewModels.BaseViewModels;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.DishTypes
{
    public class DishTypeViewModel : BaseEntityViewModel<DishType>
    {
        public DishTypeViewModel(DishType dishType, IRepository<DishType> repository) : base(dishType, repository)
        { }

        public int Id => _entity.Id;

        public string Name
        {
            get => _entity.Name;
            set
            {
                UpdateEntity(_entity with { Name = value });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Name));
        }
    }
}
