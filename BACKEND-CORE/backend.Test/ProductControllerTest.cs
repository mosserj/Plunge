using backend.Core.Interfaces;
using System;
using Xunit;
using backend.Controllers;
using Moq;
using backend.Core.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace backend.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;

        private List<Product> _mockProducts;

        public ProductControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);

            _mockProducts = new List<Product>();
            var product = new Product
            {
                CategoryId = 1,
                Price = 100,
                ProductName = "testProductName"
            };
            _mockProducts.Add(product);
        }

        [Fact]
        public void GetTest()
        {
            // Arrange
            _mockProductService.Setup(x => x.Get()).Returns(_mockProducts);

            // Act
            IActionResult result = _controller.Get();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var list = Assert.IsAssignableFrom<List<Product>>(objectResult.Value);

            // Assert
            Assert.Equal(list[0].ProductName, "testProductName");
            Assert.Equal(objectResult.StatusCode, 200);
        }

        [Fact]
        public void GetTest_Status404NotFound()
        {
            // Act
            IActionResult result = _controller.Get();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);

            // Assert
            Assert.Equal(objectResult.StatusCode, 404);
        }
    }
}
