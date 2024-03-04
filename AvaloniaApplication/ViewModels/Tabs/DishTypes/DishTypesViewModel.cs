using AvaloniaApplication.ViewModels.BaseViewModels;
using Domain;
using Domain.Services;

namespace AvaloniaApplication.ViewModels.Tabs.DishTypes
{
    public class DishTypesViewModel : BaseEntitiesViewModel<DishType, DishTypeViewModel>
	{
        public DishTypesViewModel(IRepository<DishType> repository) : base(repository)
        {
            Initialize();
        }

        protected override DishTypeViewModel CreateEntityViewModel(DishType entity)
        {
            return new DishTypeViewModel(entity, _repository);
        }

        protected override DishType CreateNewEntity()
        {
            return new DishType() { Name = $"DishType {Entities.Count + 1}" };
        }
    }
}