using application.usecases.products.abstractions;
using domain.entities;
using domain.repositories.products;

namespace application.usecases.products
{
    public class GetProducts : IGetProducts
    {
        private readonly IGetProductsRepository _getProductsRepository;

        public GetProducts(IGetProductsRepository getProductsRepository)
        {
            _getProductsRepository = getProductsRepository;
        }

        public async Task<List<Product>> Execute()
        {
            return await _getProductsRepository.Execute();
        }
    }
}
