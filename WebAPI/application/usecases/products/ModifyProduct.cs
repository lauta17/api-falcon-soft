using application.usecases.orders.abstractions;
using application.usecases.products.abstractions;
using domain.entities;
using domain.exceptions;
using domain.repositories.orders;
using domain.repositories.products;

namespace application.usecases.products
{
    public class ModifyProduct : IModifyProduct
    {
        private readonly IUpdateProductRepository _updateProductRepository;
        private readonly IGetOrderByIdRepository _getOrderByIdRepository;
        private readonly IUpdateOrderRepository _updateOrderRepository;

        public ModifyProduct(IUpdateProductRepository updateProductRepository,
            IGetOrderByIdRepository getOrderByIdRepository,
            IUpdateOrderRepository updateOrderRepository)
        {
            _updateProductRepository = updateProductRepository;
            _getOrderByIdRepository = getOrderByIdRepository;
            _updateOrderRepository = updateOrderRepository;
        }

        public async Task Execute(int orderId, Product product)
        {
            if (product.Price <= 0)
            {
                throw new ArgumentException("Price must be positive value.");
            }

            var order = await _getOrderByIdRepository.Execute(orderId);

            if (!order.Products.Any(p => p.Id == product.Id)) 
            {
                throw new BusinessException($"Product not found in order with productId: {product.Id}, orderId: {orderId}");
            }

            order.UpdateProduct(product);

            await Task.WhenAll(
                _updateProductRepository.Execute(product),
                _updateOrderRepository.Execute(order));
        }
    }
}
