using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Interfaces;
using Domain.Services;
using DynamicData;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.BaseViewModels
{
    public abstract class BaseEntitiesViewModel<TEntity, TViewModel> : BaseViewModel
        where TEntity : IEntity
        where TViewModel : BaseEntityViewModel<TEntity>
    {
        protected TaskCompletionSource _initializationTcs;
        protected IRepository<TEntity> _repository;

        public BaseEntitiesViewModel(IRepository<TEntity> repository) 
        {
            _repository = repository;
            _initializationTcs = new TaskCompletionSource();

            AddEntityCommand = ReactiveCommand.Create(AddEntity);
        }

        public event Action<TViewModel>? OnDeleted;

        public event Action<TEntity>? OnInserted;

        public ICommand AddEntityCommand { get; protected set; }

        public ObservableCollection<TViewModel> Entities { get; protected set; }
            = new ObservableCollection<TViewModel>();

        public Task WaitForInitializationAsync()
        {
            return _initializationTcs.Task;
        }

        protected virtual async void Initialize()
        {
            Entities.AddRange((await _repository.ListAsync()).Select(CreateSubscribedEntityViewModel));
            _initializationTcs.SetResult();
        }

        protected virtual async Task AddEntity()
        {
            try
            {
                var entity = await _repository.AddAsync(CreateNewEntity());
                Entities.Add(CreateSubscribedEntityViewModel(entity));
                OnInserted?.Invoke(entity);
                RaiseUpdate();
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
        }

        protected abstract TEntity CreateNewEntity();

        protected abstract TViewModel CreateEntityViewModel(TEntity entity);

        protected virtual TViewModel CreateSubscribedEntityViewModel(TEntity entity)
        {
            var viewModel = CreateEntityViewModel(entity);
            viewModel.OnDeleted += () =>
            {
                Entities.Remove(viewModel);
                OnDeleted?.Invoke(viewModel);
                RaiseUpdate();
            };
            return viewModel;
        }

        public virtual void RaiseUpdate()
        {

        }
    }
}
