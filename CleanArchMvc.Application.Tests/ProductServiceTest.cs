using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Runtime.CompilerServices;

namespace CleanArchMvc.Application.Tests
{
    public class ProductServiceTest
    {
        private IProductService _productService;
        private Mock<IProductService> _serviceMock;
        private List<ProductDTO> _products;
        private ProductDTO _product;

        public ProductServiceTest()
        {
            _product = new ProductDTO
            {
                Id = 1,
                Name = Faker.Name.FullName(),
                Description = Faker.Name.FullName(),
                Price = Faker.RandomNumber.Next(1, 999999999),
                Stock = Faker.RandomNumber.Next(1, 9999999),
                CategoryId = 1,
            };

            _products = new List<ProductDTO>()
            {
                new ProductDTO()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    Price = 1,
                    Stock = 1,
                    CategoryId = 1,
                },
                new ProductDTO()
                {
                    Id = 2,
                    Name = Faker.Name.FullName(),
                    Description = Faker.Name.FullName(),
                    Price = Faker.RandomNumber.Next(1, 999999999),
                    Stock = Faker.RandomNumber.Next(1, 99999),
                    CategoryId = 2,
                }
            };
        }

        [Fact]
        public async Task CreateProduct_WithValidParameters_ResultOK()
        {
            var product = new ProductDTO
            {
                Id = 1,
                Name = Faker.Name.FullName(),
                Description = Faker.Name.FullName(),
                Price = Faker.RandomNumber.Next(1, 999999999),
                Stock = Faker.RandomNumber.Next(1, 9999999),
                CategoryId = 1,
            };

            _serviceMock = new Mock<IProductService>();
            _serviceMock.Setup(m => m.Add(product)).ReturnsAsync(product);
            _productService = _serviceMock.Object;

            var result = await _productService.Add(product);
            Assert.Equal(product, result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.CategoryId, result.CategoryId);
        }

        [Fact]
        public async Task UpdateProduct_WithValidParameters_ResultOk()
        {
            var 

            _serviceMock = new Mock<IProductService>();
            _serviceMock.Setup(m => m.Add(_product)).ReturnsAsync(_product);
            _productService = _serviceMock.Object;

            var result = await _productService.Add(_product);
            Assert.Equal(_product, result);
            Assert.Equal(_product.Id, result.Id);
            Assert.Equal(_product.Name, result.Name);
            Assert.Equal(_product.Description, result.Description);
            Assert.Equal(_product.Price, result.Price);
            Assert.Equal(_product.CategoryId, result.CategoryId);

            _product.Name = "Nome alterado";
            _product.Description = "Descricao alterada";

            _serviceMock = new Mock<IProductService>();
            _serviceMock.Setup(m => m.Update(_product)).ReturnsAsync(_product);
            _productService = _serviceMock.Object;

            result = await _productService.Update(_product);
            Assert.Equal(_product, result);
            Assert.Equal(_product.Id, result.Id);
            Assert.Equal(_product.Name, result.Name);
            Assert.Equal(_product.Description, result.Description);
            Assert.Equal(_product.Price, result.Price);
            Assert.Equal(_product.CategoryId, result.CategoryId);
        }

        [Fact]
        public async Task GetProducts_WithValidParameters_ResultOk()
        {
            _serviceMock = new Mock<IProductService>();
            _serviceMock.Setup(m => m.GetProducts()).ReturnsAsync(_products);
            _productService = _serviceMock.Object;

            var result = await _productService.GetProducts();
            Assert.Equal(_products, result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task GetProductById_WithValidParameters_ResultOK()
        {
            _serviceMock = new Mock<IProductService>();
            _serviceMock.Setup(m => m.GetById(_product.Id)).ReturnsAsync(_product);
            _productService = _serviceMock.Object;

            var result = await _productService.GetById(_product.Id);
            Assert.Equal(_product, result);
            Assert.Equal(_product.Id, result.Id);
            Assert.Equal(_product.Name, result.Name);
            Assert.Equal(_product.Description, result.Description);
            Assert.Equal(_product.Price, result.Price);
            Assert.Equal(_product.Stock, result.Stock);
            Assert.Equal(_product.CategoryId, result.CategoryId);
        }

        [Fact]
        public async Task DeleteProduct_WithValidParameters_ResultOk()
        {
            _serviceMock = new Mock<IProductService>();
            _serviceMock.Setup(m => m.Remove(_product.Id));
            _productService = _serviceMock.Object;

            var action = async () => await _productService.Remove(_product.Id);
            action.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task DeleteProduct_WithInvalidParameters_ResultException()
        {
            _serviceMock = new Mock<IProductService>();
            _serviceMock.Setup(m => m.Remove(It.IsAny<int>()));
            _productService = _serviceMock.Object;

            var action = async () => await _productService.Remove(0);
            action.Should().ThrowAsync<Exception>();
        }
    }
}
