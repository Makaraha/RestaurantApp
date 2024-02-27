using System;
using System.Windows.Input;
using Application.Commands;
using Domain;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.MeasurementUnits
{
    public class MeasurementUnitViewModel : ReactiveObject
    {
        private IMediator _mediator;

        public MeasurementUnitViewModel(MeasurementUnit measurementUnit, IMediator mediator)
        {
            _mediator = mediator;


            _name = measurementUnit.Name;
            Id = measurementUnit.Id;

            DeleteMeasurementUnitCommand = ReactiveCommand.Create(DeleteMeasurementUnit);
        }

        public int Id { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                UpdateMeasurementUnit(value);
            }
        }

        public ICommand DeleteMeasurementUnitCommand { get; private set; }

        public event Action? OnDeleted;

        private async void DeleteMeasurementUnit()
        {
            await _mediator.Send(new DeleteMeasurementUnitCommand() { MeasurementUnitId = Id });
            OnDeleted?.Invoke();
        }

        private async void UpdateMeasurementUnit(string newName)
        {
            var oldName = _name;
            _name = newName;
            try
            {
                await _mediator.Send(new UpdateMeasurementUnitCommand()
                {
                    Id = Id,
                    Name = newName
                });
            }
            catch
            {
                _name = oldName;
                this.RaisePropertyChanged(nameof(Name));
            }
        }
    }
}
