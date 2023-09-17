using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using MediatR;

namespace Api.Core.Application.Features.CQRS.GetProductMediatR
{
    public class ProductGetQueryRequest : IRequest<Product>
    {
        public int Id { get; set; }
        public ProductGetQueryRequest(int id)
        {
            Id = id;    
        }
    }
    public class ProductGetQueryHandler : IRequestHandler<ProductGetQueryRequest, Product>
    {
        private readonly IRepository<Product> _productRepository;

        public ProductGetQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductGetQueryRequest request, CancellationToken cancellationToken)
        {
           var data = await _productRepository.GetByIdAsync(request.Id);
            if(data == null)
            {
               throw new Exception("Kayıt bulunamadı");
            }
            return data;
        }
    }
}
