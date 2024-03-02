using System;
using System.Windows.Input;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.DishTypes
{
    public class DishTypeViewModel : ReactiveObject
    {
        private IRepository<DishType> _repository;

        public DishTypeViewModel(DishType dishType, IRepository<DishType> repository)
        {
            _repository = repository;

            _name = dishType.Name;
            Id = dishType.Id;

            DeleteDishTypeCommand = ReactiveCommand.Create(DeleteDishType);
        }

        public int Id { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                UpdateDishType(value);
            }
        }

        public ICommand DeleteDishTypeCommand { get; private set; }

        public event Action? OnDeleted;

        private async void DeleteDishType()
        {
            try
            {
                await _repository.DeleteAsync(Id);
                OnDeleted?.Invoke();
            }
            catch { }
        }

        private async void UpdateDishType(string newName)
        {
            try
            {
                await _repository.UpdateAsync(new DishType()
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
