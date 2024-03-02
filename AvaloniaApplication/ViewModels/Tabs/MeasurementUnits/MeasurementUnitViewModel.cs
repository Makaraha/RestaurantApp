using System;
using System.Windows.Input;
using Domain;
using Domain.Services;
using MediatR;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.MeasurementUnits
{
    public class MeasurementUnitViewModel : ReactiveObject
    {
        private IRepository<MeasurementUnit> _repository;

        public MeasurementUnitViewModel(MeasurementUnit measurementUnit, IRepository<MeasurementUnit> repository)
        {
            _repository = repository;

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
                await _repository.DeleteAsync(Id);
                OnDeleted?.Invoke();
            }
            catch { }
        }

        private async void UpdateMeasurementUnit(string newName)
        {
            try
            {
                await _repository.UpdateAsync(new MeasurementUnit()
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
