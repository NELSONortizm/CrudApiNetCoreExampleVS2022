
using CrudApiExample.Models;
using CrudApiExample.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApiExample.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _repository.GetAllProductsAsync();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _repository.GetProductByIdAsync(id);
        }

        public Task AddProductAsync(Product product)
        {
            return _repository.AddProductAsync(product);
        }

        public Task UpdateProductAsync(Product product)
        {
            return _repository.UpdateProductAsync(product);
        }

        public Task DeleteProductAsync(int id)
        {
            return _repository.DeleteProductAsync(id);
        }
    }
}
