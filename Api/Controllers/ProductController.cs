using Api.Core.Application.Features.CQRS.CreateProductMediatR;
using Api.Core.Application.Features.CQRS.DeleteProductMediatR;
using Api.Core.Application.Features.CQRS.GetAllMediatR;
using Api.Core.Application.Features.CQRS.GetProductMediatR;
using Api.Core.Application.Features.CQRS.UpdateProductMediatR;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member")]
  
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response =await _mediator.Send(new ProductGetAllQueryRequest());    
            return Ok(response);
        }
        [HttpPost("GetProduct")]
        public async Task<IActionResult> GetProduct(ProductGetQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new ProductDeleteCommandRequest(id));
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProducts(ProductUpdateCommandRequest rq)
        {
           var response = await _mediator.Send(rq);
            return Ok(response);
        }
    }
}
    