using Domain;
using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class UpdateMeasurementUnitCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateMeasurementUnitCommandHandler : IRequestHandler<UpdateMeasurementUnitCommand>
    {
        private IRepository<MeasurementUnit> _repository;

        public UpdateMeasurementUnitCommandHandler(IRepository<MeasurementUnit> repository) 
        {
            _repository = repository;
        }

        public Task Handle(UpdateMeasurementUnitCommand request, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(new MeasurementUnit()
            {
                Id = request.Id,
                Name = request.Name,
            });
        }
    }
}
