using Domain;
using Domain.Services;
using MediatR;

namespace Application.Queries.Products
{
    public class GetProductsQuery : IRequest<List<Product>>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private IRepository<Product> _repository;

        public GetProductsQueryHandler(IRepository<Product> repository) 
        {
            _repository = repository;
        }

        public Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return _repository.ListAsync();
        }
    }
}
