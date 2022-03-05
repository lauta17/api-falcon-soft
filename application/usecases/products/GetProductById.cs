using application.usecases.products.abstractions;
using domain.entities;
using domain.repositories.products;

namespace application.usecases.products
{
    public class GetProductById : IGetProductById
    {
        private readonly IGetProductByIdRepository _getProductByIdRepository;

        public GetProductById(IGetProductByIdRepository getProductByIdRepository)
        {
            _getProductByIdRepository = getProductByIdRepository;
        }

        public async Task<Product> Execute(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("productId must be positive value.");
            }

            return await _getProductByIdRepository.Execute(id);
        }
    }
}
