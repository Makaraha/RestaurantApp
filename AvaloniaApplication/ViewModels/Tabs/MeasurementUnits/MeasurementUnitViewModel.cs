using System;
using System.Windows.Input;
using Application.Commands.MeasurementUnits;
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
            try
            {
                await _mediator.Send(new DeleteMeasurementUnitCommand() { MeasurementUnitId = Id });
                OnDeleted?.Invoke();
            }
            catch { }
        }

        private async void UpdateMeasurementUnit(string newName)
        {
            try
            {
                await _mediator.Send(new UpdateMeasurementUnitCommand()
                {
                    Id = Id,
                    Name = newName
                });
                _name = newName;
            }
            catch { }
            finally
            {
                this.RaisePropertyChanged(nameof(Name));
            }
        }
    }
}
