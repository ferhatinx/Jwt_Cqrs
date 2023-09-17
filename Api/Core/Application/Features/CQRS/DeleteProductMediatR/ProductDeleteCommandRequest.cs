using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using MediatR;

namespace Api.Core.Application.Features.CQRS.DeleteProductMediatR
{
    public class ProductDeleteCommandRequest : IRequest
    {
        public int Id { get; set; }
        public ProductDeleteCommandRequest(int id)
        {
            Id = id;    
        }
    }
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public ProductDeleteCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(ProductDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
              

            }
     
            throw new Exception("Kayıt bulunamadı");
        }
    }
}
