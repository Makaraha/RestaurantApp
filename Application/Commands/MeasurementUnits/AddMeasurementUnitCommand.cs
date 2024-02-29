using Domain;
using Domain.Services;
using MediatR;

namespace Application.Commands.MeasurementUnits
{
    public class AddMeasurementUnitCommand : IRequest<MeasurementUnit>
    {
        public string Name { get; set; }
    }

    public class AddMeasurementUnitCommandHandler : IRequestHandler<AddMeasurementUnitCommand, MeasurementUnit>
    {
        private IRepository<MeasurementUnit> _repository;

        public AddMeasurementUnitCommandHandler(IRepository<MeasurementUnit> repository)
        {
            _repository = repository;
        }

        public async Task<MeasurementUnit> Handle(AddMeasurementUnitCommand request, CancellationToken cancellationToken)
        {
            var id = await _repository.AddAsync(new MeasurementUnit()
            {
                Name = request.Name,
            });

            return new MeasurementUnit()
            {
                Id = id,
                Name = request.Name
            };
        }
    }
}
