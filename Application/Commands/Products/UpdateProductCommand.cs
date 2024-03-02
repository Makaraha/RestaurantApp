using Domain;
using Domain.Services;
using MediatR;

namespace Application.Commands.Products
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MeasurementUnitId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(new Product()
            {
                Id = request.Id,
                Name = request.Name,
                MeasurementUnitId = request.MeasurementUnitId
            });
        }
    }
}
