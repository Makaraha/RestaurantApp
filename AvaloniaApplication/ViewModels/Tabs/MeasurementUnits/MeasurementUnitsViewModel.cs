using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Queries.MeasurementUnits;
using Domain;
using Domain.Services;
using DynamicData;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.MeasurementUnits
{
    public class MeasurementUnitsViewModel : ReactiveObject
    {
        private IRepository<MeasurementUnit> _repository;
        private TaskCompletionSource _initializationTcs;

        public MeasurementUnitsViewModel(IRepository<MeasurementUnit> repository) 
        {
            _repository = repository;
            _initializationTcs = new TaskCompletionSource();
            Initialize();
        }

        public ICommand AddMeasurementUnitCommand { get; private set; }

        public ObservableCollection<MeasurementUnitViewModel> MeasurementUnits { get; private set; } 
            = new ObservableCollection<MeasurementUnitViewModel>();

        public Task WaitForInitializationAsync()
        {
            return _initializationTcs.Task;
        }

        private async void Initialize()
        {
            var units = await _repository.ListAsync();
            MeasurementUnits.AddRange(units.Select(CreateMeasurementUnitViewModel));
            AddMeasurementUnitCommand = ReactiveCommand.Create(AddMeasurementUnit);

            _initializationTcs.SetResult();
        }

        private async void AddMeasurementUnit()
        {
            var name = $"MeasurementUnit_{MeasurementUnits.Count + 1}";
            var unit = await _repository.AddAsync(new MeasurementUnit() { Name = name });
            MeasurementUnits.Add(CreateMeasurementUnitViewModel(unit));
        }

        private MeasurementUnitViewModel CreateMeasurementUnitViewModel(MeasurementUnit measurementUnit)
        {
            var viewModel = new MeasurementUnitViewModel(measurementUnit, _repository);
            viewModel.OnDeleted += () => MeasurementUnits.Remove(viewModel);
            return viewModel;
        }
    }
}
