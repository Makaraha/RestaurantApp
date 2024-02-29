using Domain;
using Domain.Services;
using MediatR;

namespace Application.Commands.Products
{
    public class AddProductCommand : IRequest<Product>
    {
        public int MeasurementUnitId { get; set; }

        public string Name { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
    {
        private IRepository<Product> _repository;

        public AddProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var id = await _repository.AddAsync(new Product() 
            {
                MeasurementUnitId = request.MeasurementUnitId,
                Name = request.Name
            });

            return new Product()
            {
                Id = id,
                Name = request.Name,
                MeasurementUnitId = request.MeasurementUnitId
            };
        }
    }
}
