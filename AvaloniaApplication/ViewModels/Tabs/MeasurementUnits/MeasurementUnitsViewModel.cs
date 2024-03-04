using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AvaloniaApplication.ViewModels.BaseViewModels;
using Domain;
using Domain.Services;

namespace AvaloniaApplication.ViewModels.Tabs.MeasurementUnits
{
    public class MeasurementUnitsViewModel : BaseEntitiesViewModel<MeasurementUnit, MeasurementUnitViewModel>
    {
        public MeasurementUnitsViewModel(IRepository<MeasurementUnit> repository)  : base(repository)
        {
            Initialize();
        }


        protected override MeasurementUnit CreateNewEntity()
        {
            return new MeasurementUnit()
            {
                Name = $"Measurement unit {Entities.Count + 1}"
            };
        }

        protected override MeasurementUnitViewModel CreateEntityViewModel(MeasurementUnit entity)
        {
            return new MeasurementUnitViewModel(entity, _repository);
        }
    }
}
