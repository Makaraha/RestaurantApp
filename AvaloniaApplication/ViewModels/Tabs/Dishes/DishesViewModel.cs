using System;
using System.Linq;
using AvaloniaApplication.ViewModels.BaseViewModels;
using AvaloniaApplication.ViewModels.Tabs.DishTypes;
using Domain;
using Domain.Services;

namespace AvaloniaApplication.ViewModels.Tabs.Dishes
{
    public class DishesViewModel : BaseEntitiesViewModel<Dish, DishViewModel>
    {
        private DishTypesViewModel _dishTypesViewModel;

        public DishesViewModel(IRepository<Dish> repository, DishTypesViewModel dishTypeViewModel) : base(repository)
        {
            _dishTypesViewModel = dishTypeViewModel;

            Initialize();
        }

        protected override async void Initialize()
        {
            await _dishTypesViewModel.WaitForInitializationAsync();

            base.Initialize();
        }

        protected override DishViewModel CreateEntityViewModel(Dish entity)
        {
            return new DishViewModel(entity, _dishTypesViewModel, _repository);
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
    }
}
