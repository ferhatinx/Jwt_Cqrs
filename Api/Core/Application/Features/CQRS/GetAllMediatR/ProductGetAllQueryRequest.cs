using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using MediatR;

namespace Api.Core.Application.Features.CQRS.GetAllMediatR
{
    public class ProductGetAllQueryRequest : IRequest<List<Product>>
    {
    }
    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQueryRequest, List<Product>>
    {
        private readonly IRepository<Product> _productRepository;

        public ProductGetAllQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(ProductGetAllQueryRequest request, CancellationToken cancellationToken)
        {
          return  await _productRepository.GetAllAsync();
        }
    }
}
