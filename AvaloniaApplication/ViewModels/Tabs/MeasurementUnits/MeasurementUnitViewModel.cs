using AvaloniaApplication.ViewModels.BaseViewModels;
using Domain;
using Domain.Services;
using ReactiveUI;

namespace AvaloniaApplication.ViewModels.Tabs.MeasurementUnits
{
    public class MeasurementUnitViewModel : BaseEntityViewModel<MeasurementUnit>
    {
        public MeasurementUnitViewModel(MeasurementUnit entity, IRepository<MeasurementUnit> repository) : base(entity, repository)
        {
        }

        public int Id => _entity.Id;

        public string Name
        {
            get => _entity.Name;
            set
            {
                if(_entity.Name == value) 
                    return;

                UpdateEntity(_entity with { Name = value });
            }
        }

        protected override void RaiseUpdate()
        {
            this.RaisePropertyChanged(nameof(Id));
            this.RaisePropertyChanged(nameof(Name));
        }
    }
}
