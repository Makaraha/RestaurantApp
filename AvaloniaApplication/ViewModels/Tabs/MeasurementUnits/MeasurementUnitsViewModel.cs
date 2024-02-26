using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Application.Commands;
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
        private int _lastId = 0;

        public MeasurementUnitsViewModel(IMediator mediator) 
        {
            _mediator = mediator;
            Initialize();
        }

        public ICommand AddMeasurementUnitCommand { get; private set; }

        public ObservableCollection<MeasurementUnit> MeasurementUnits { get; private set; } = new ObservableCollection<MeasurementUnit>();

        private async void Initialize()
        {
            MeasurementUnits.AddRange(await _mediator.Send(new GetMeasurementUnitsQuery()));
            AddMeasurementUnitCommand = ReactiveCommand.Create(AddMeasurementUnit);

            if (MeasurementUnits.Any())
                _lastId = MeasurementUnits.Max(x => x.Id);
        }

        private async void AddMeasurementUnit()
        {
            var name = $"MeasurementUnit_{_lastId + 1}";
            var unit = await _mediator.Send(new AddMeasurementUnitCommand() { Name = name });
            MeasurementUnits.Add(unit);
            _lastId = unit.Id;
        }
    }
}
