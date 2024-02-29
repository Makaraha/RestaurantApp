using Domain;
using Domain.Services;
using MediatR;

namespace Application.Commands.Products
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private IRepository<Product> _repository;

        public DeleteProductCommandHandler(IRepository<Product> repository) 
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.ProductId);
        }
    }
}
