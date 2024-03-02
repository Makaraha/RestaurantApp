using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain;
using Domain.Services;
using DynamicData;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.DishTypes
{
    public class DishTypesViewModel : ReactiveObject
	{
        private IRepository<DishType> _repository;
        private TaskCompletionSource _initializationTcs;

        public DishTypesViewModel(IRepository<DishType> repository)
        {
            _repository = repository;
            _initializationTcs = new TaskCompletionSource();
            Initialize();
        }

        public ICommand AddDishTypeCommand { get; private set; }

        public ObservableCollection<DishTypeViewModel> DishTypes { get; private set; }
            = new ObservableCollection<DishTypeViewModel>();

        public Task WaitForInitializationAsync()
        {
            return _initializationTcs.Task;
        }

        private async void Initialize()
        {
            var dishTypes = await _repository.ListAsync();
            DishTypes.AddRange(dishTypes.Select(CreateDishTypeViewModel));
            AddDishTypeCommand = ReactiveCommand.Create(AddDishType);

            _initializationTcs.SetResult();
        }

        private async void AddDishType()
        {
            var name = $"DishType {DishTypes.Count + 1}";
            var dishType = await _repository.AddAsync(new DishType() { Name = name });
            DishTypes.Add(CreateDishTypeViewModel(dishType));
        }

        private DishTypeViewModel CreateDishTypeViewModel(DishType dishType)
        {
            var viewModel = new DishTypeViewModel(dishType, _repository);
            viewModel.OnDeleted += () => DishTypes.Remove(viewModel);
            return viewModel;
        }
    }
}