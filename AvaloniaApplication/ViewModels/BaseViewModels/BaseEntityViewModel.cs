using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AvaloniaApplication.Views.Popups;
using Domain.Interfaces;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.BaseViewModels
{
    public abstract class BaseEntityViewModel<T> : BaseViewModel
        where T : IEntity
    {
        protected T _entity;
        private IRepository<T> _repository;

        public BaseEntityViewModel(T entity, IRepository<T> repository) 
        {
            _entity = entity;
            _repository = repository;

            DeleteEntityCommand = ReactiveCommand.Create(() => DeleteEntity(_entity.Id));
        }

        public ICommand DeleteEntityCommand { get; protected set; }

        public event Action? OnDeleted;

        protected virtual async void UpdateEntity(T entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                _entity = entity;
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
            finally
            {
                RaiseUpdate();
            }
        }

        protected virtual async Task DeleteEntity(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                OnDeleted?.Invoke();
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
        }

        protected abstract void RaiseUpdate();
    }
}
