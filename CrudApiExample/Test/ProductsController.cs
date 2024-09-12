// Tests/ProductsControllerTests.cs
using CrudApiExample.Controllers;
using CrudApiExample.Models;
using CrudApiExample.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CrudApiExample.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductsController(_mockService.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Price = 10.0m },
                new Product { Id = 2, Name = "Product2", Price = 20.0m }
            };
            _mockService.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Price = 10.0m };
            _mockService.Setup(service => service.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal("Product1", returnedProduct.Name);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Price = 10.0m };
            _mockService.Setup(service => service.AddProductAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateProduct(product);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetProduct", createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_WhenIdMismatch()
        {
            // Act
            var result = await _controller.UpdateProduct(2, new Product { Id = 1 });

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Price = 10.0m };
            _mockService.Setup(service => service.UpdateProductAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProduct(1, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteProductAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
