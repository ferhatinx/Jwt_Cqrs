using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using MediatR;

namespace Api.Core.Application.Features.CQRS.CreateProductMediatR
{
    public class ProductCreateCommandRequest : IRequest<bool>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, bool>
    {
        private readonly IRepository<Product> _repository;

        public ProductCreateCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
        {
           await _repository.CreateAsync(new()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price,
                CategoryId = request.CategoryId,
            });
            return true;
        }
    }
}
