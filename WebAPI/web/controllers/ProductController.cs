using application.usecases.products.abstractions;
using Microsoft.AspNetCore.Mvc;
using web.mappers.abstractions;
using web.models.products;
using web.authorization;

namespace web.controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IGetProducts _getProducts;
        private readonly IGetProductById _getProductById;

        private readonly IProductResponseMapper _productResponseMapper;

        public ProductController(IGetProducts getProducts,
            IGetProductById getProductById,
            IProductResponseMapper productResponseMapper)
        {
            _getProducts = getProducts;
            _getProductById = getProductById;
            _productResponseMapper = productResponseMapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = _getProductById.Execute(id);

            return Ok(_productResponseMapper.Map(await product));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = _getProducts.Execute();

            return Ok(_productResponseMapper.Map(await products));
        }
    }
}
