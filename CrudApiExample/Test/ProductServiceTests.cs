// Tests/ProductServiceTests.cs
using CrudApiExample.Models;
using CrudApiExample.Repositories;
using CrudApiExample.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CrudApiExample.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Price = 10.0m },
                new Product { Id = 2, Name = "Product2", Price = 20.0m }
            };
            _mockRepo.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _service.GetAllProductsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        // Add more tests for other methods
    }
}
