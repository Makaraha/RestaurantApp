using Domain;
using Domain.Services;
using MediatR;

namespace Application.Queries.MeasurementUnits
{
    public class GetMeasurementUnitsQuery : IRequest<List<MeasurementUnit>>
    {
    }

    public class GetMeasurementUnitsQueryHandler : IRequestHandler<GetMeasurementUnitsQuery, List<MeasurementUnit>>
    {
        private IRepository<MeasurementUnit> _repository;

        public GetMeasurementUnitsQueryHandler(IRepository<MeasurementUnit> repository) 
        {
            _repository = repository;
        }

        public Task<List<MeasurementUnit>> Handle(GetMeasurementUnitsQuery request, CancellationToken cancellationToken)
        {
            return _repository.ListAsync();
        }
    }
}
