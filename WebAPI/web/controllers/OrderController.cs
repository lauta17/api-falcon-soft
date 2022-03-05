using application.usecases.orders.abstractions;
using application.usecases.products.abstractions;
using Microsoft.AspNetCore.Mvc;
using web.authorization;
using web.mappers.abstractions;
using web.models;
using web.models.products;

namespace web.controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IGetFilteredOrders _getOrders;
        private readonly IQueryStringPaginationMapper _queryStringPaginationMapper;
        private readonly IQueryStringFiltersMapper _queryStringFiltersMapper;
        private readonly IOrderResponseMapper _orderResponseMapper;
        private readonly IModifyProduct _modifyProduct;
        private readonly IProductRequestMapper _productRequestMapper;

        public OrderController(IGetFilteredOrders getOrders,
             IQueryStringPaginationMapper queryStringPaginationMapper,
             IQueryStringFiltersMapper queryStringFiltersMapper,
             IOrderResponseMapper orderResponseMapper,
             IModifyProduct modifyProduct,
             IProductRequestMapper productRequestMapper)
        {
            _getOrders = getOrders;
            _queryStringPaginationMapper = queryStringPaginationMapper;
            _queryStringFiltersMapper = queryStringFiltersMapper;
            _orderResponseMapper = orderResponseMapper;
            _modifyProduct = modifyProduct;
            _productRequestMapper = productRequestMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]QueryStringPagination pagination, [FromQuery]QueryStringFilters filter) 
        {
            var result = await _getOrders.Execute(
                    _queryStringPaginationMapper.Map(pagination),
                    _queryStringFiltersMapper.Map(filter)
                );

            return Ok(_orderResponseMapper.Map(result));
        }

        [HttpPut("{orderId}/product/{productId}")]
        public async Task<IActionResult> Put(int orderId, int productId, ProductRequest productRequest)
        {
            var product = _productRequestMapper.Map(productId, productRequest);

            await _modifyProduct.Execute(orderId, product);

            return Ok();
        }
    }
}
