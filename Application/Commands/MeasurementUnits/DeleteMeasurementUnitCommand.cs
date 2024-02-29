using Domain;
using Domain.Services;
using MediatR;

namespace Application.Commands.MeasurementUnits
{
    public class DeleteMeasurementUnitCommand : IRequest
    {
        public int MeasurementUnitId { get; set; }
    }

    public class DeleteMeasurementUnitCommandHandler : IRequestHandler<DeleteMeasurementUnitCommand>
    {
        private IRepository<MeasurementUnit> _repository;

        public DeleteMeasurementUnitCommandHandler(IRepository<MeasurementUnit> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteMeasurementUnitCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.MeasurementUnitId);
        }
    }
}
