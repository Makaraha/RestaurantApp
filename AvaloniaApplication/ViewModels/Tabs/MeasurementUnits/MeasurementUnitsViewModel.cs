using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Commands;
using Application.Commands.MeasurementUnits;
using Application.Queries.MeasurementUnits;
using Domain;
using DynamicData;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.MeasurementUnits
{
    public class MeasurementUnitsViewModel : ReactiveObject
    {
        private IMediator _mediator;
        private TaskCompletionSource _initializationTcs;

        public MeasurementUnitsViewModel(IMediator mediator) 
        {
            _mediator = mediator;
            _initializationTcs = new TaskCompletionSource();
            Initialize();
        }

        public ICommand AddMeasurementUnitCommand { get; private set; }

        public MeasurementUnit SelectedItem { get; private set; }

        public ObservableCollection<MeasurementUnitViewModel> MeasurementUnits { get; private set; } 
            = new ObservableCollection<MeasurementUnitViewModel>();

        public Task WaitForInitializationAsync()
        {
            return _initializationTcs.Task;
        }

        private async void Initialize()
        {
            var units = await _mediator.Send(new GetMeasurementUnitsQuery());
            MeasurementUnits.AddRange(units.Select(x => CreateMeasurementUnitViewModel(x)));
            AddMeasurementUnitCommand = ReactiveCommand.Create(AddMeasurementUnit);

            _initializationTcs.SetResult();
        }

        private async void AddMeasurementUnit()
        {
            var name = $"MeasurementUnit_{MeasurementUnits.Count + 1}";
            var unit = await _mediator.Send(new AddMeasurementUnitCommand() { Name = name });
            MeasurementUnits.Add(CreateMeasurementUnitViewModel(unit));
        }

        private MeasurementUnitViewModel CreateMeasurementUnitViewModel(MeasurementUnit measurementUnit)
        {
            var viewModel = new MeasurementUnitViewModel(measurementUnit, _mediator);
            viewModel.OnDeleted += () => MeasurementUnits.Remove(viewModel);
            return viewModel;
        }
    }
}
