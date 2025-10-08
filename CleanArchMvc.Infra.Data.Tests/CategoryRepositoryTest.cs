using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.Infra.Data.Tests
{
    public class CategoryRepositoryTest : BaseTest
    {
        
        private CategoryRepository _repository; 

        public CategoryRepositoryTest()
        {
            _repository = new CategoryRepository(_context);
        }

        [Fact(DisplayName = "Category Create With Valid State")]
        public async Task CrudCategory_WithValidParameters_ResultValidState()
        {
            // Create
            var categoria = await _repository.Create(new Category(1, "Category Name"));
            Assert.NotNull(categoria);
            Assert.Equal("Category Name", categoria.Name);

            // Update
            categoria.Update("Category Name Updated");

            var categoriaUpdated = await _repository.Update(categoria);

            Assert.NotNull(categoriaUpdated);
            Assert.Equal("Category Name Updated", categoriaUpdated.Name);

            // GetById
            var categoriaById = await _repository.GetById(categoria.Id);

            Assert.NotNull(categoriaById);
            Assert.Equal("Category Name Updated", categoriaById.Name);

            // GetCategories
            var categorias = await _repository.GetCategories();

            Assert.NotNull(categorias);
            Assert.True(categorias.Count() > 0);

            // Remove
            await _repository.Remove(categoria);

            var categoriaRemovida = await _repository.GetById(categoria.Id);
            Assert.Null(categoriaRemovida);
        }

    }
}