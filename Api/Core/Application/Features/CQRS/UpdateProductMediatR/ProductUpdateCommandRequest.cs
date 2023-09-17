using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using MediatR;

namespace Api.Core.Application.Features.CQRS.UpdateProductMediatR
{
    public class ProductUpdateCommandRequest : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommandRequest,Product>
    {
        private readonly IRepository<Product> _repository;

        public ProductUpdateCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(ProductUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByFilter(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new Exception("Null döndü");
            }
            entity.Price = request.Price;
            entity.CategoryId = request.CategoryId;
            entity.Stock = request.Stock;
            entity.Name = request.Name;
            await _repository.UpdateAsync(entity);
            return entity;
        }
    }
}
