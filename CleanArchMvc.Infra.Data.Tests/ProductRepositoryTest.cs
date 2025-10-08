using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Infra.Data.Repositories;

namespace CleanArchMvc.Infra.Data.Tests
{
    public class ProductRepositoryTest : BaseTest
    {
        private ProductRepository _repository;
        public ProductRepositoryTest()
        {
            _repository = new ProductRepository(_context);
        }

        [Fact(DisplayName = "Crud Product With Valid State")]
        public async Task CrudProduct_WithValidParameters_ResultValidState()
        {
            var product = new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
            var produtoCriado = await _repository.CreateAsync(product);

            Assert.NotNull(produtoCriado);
            Assert.Equal("Product Name", produtoCriado.Name);

            // Update
            produtoCriado.Update("Product Name Updated", "Product Description Updated", 19.99m, 199, "product image updated", 1);
            var produtoUpdated = await _repository.UpdateAsync(produtoCriado);

            Assert.NotNull(produtoUpdated);
            Assert.Equal("Product Name Updated", produtoUpdated.Name);
            Assert.Equal("Product Description Updated", produtoUpdated.Description);
            Assert.Equal(19.99m, produtoUpdated.Price);
            Assert.Equal(199, produtoUpdated.Stock);
            Assert.Equal("product image updated", produtoUpdated.Image);

            // GetById
            var produtoById = await _repository.GetByIdAsync(produtoCriado.Id);
            Assert.NotNull(produtoById);
            Assert.Equal("Product Name Updated", produtoById.Name);
            Assert.Equal("Product Description Updated", produtoById.Description);
            Assert.Equal(19.99m, produtoById.Price);
            Assert.Equal(199, produtoById.Stock);
            Assert.Equal("product image updated", produtoById.Image);

            // GetProducts
            var produtos = await _repository.GetProductsAsync();
            Assert.NotNull(produtos);
            Assert.True(produtos.Count() > 0);

            // Remove
            await _repository.RemoveAsync(produtoCriado);
            var produtoRemovido = await _repository.GetByIdAsync(produtoCriado.Id);
            Assert.Null(produtoRemovido);
        }
    }
}
