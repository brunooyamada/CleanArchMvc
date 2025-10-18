using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using FluentAssertions;
using Moq;

namespace CleanArchMvc.Application.Tests
{
    public class CategoryServiceTest
    {
        private ICategoryService _categoryService;
        private Mock<ICategoryService> _serviceMock;
        private List<CategoryDTO> _categories;

        public CategoryServiceTest()
        {
            _categories = new List<CategoryDTO>();
            _categories.Add(new CategoryDTO { Id = 1, Name = Faker.Name.FullName() });
            _categories.Add(new CategoryDTO { Id = 2, Name = Faker.Name.FullName() });
        }

        [Fact]
        public async Task CreateCategory_WithValidParameters_ResultOk()
        {
            var category = new CategoryDTO
            {
                Id = 1,
                Name = Faker.Name.FullName(),
            };
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.Add(category)).ReturnsAsync(category);
            _categoryService = _serviceMock.Object;

            Func<Task> action = async () => await _categoryService.Add(category);
            action.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetCategory_WithValidParameters_ResultOk()
        {
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.GetCategories()).ReturnsAsync(_categories);
            _categoryService = _serviceMock.Object;

            var result = await _categoryService.GetCategories();
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task GetCategoryById_WithValidParameters_ResultOk()
        {
            var category = new CategoryDTO
            {
                Id = 1,
                Name = Faker.Name.FullName(),
            };

            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.GetById(1)).ReturnsAsync(category);

            _categoryService = _serviceMock.Object;

            var result = await _categoryService.GetById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCategory_WithValidParameters_ResultOk()
        {
            var category = new CategoryDTO
            {
                Id = 1,
                Name = Faker.Name.FullName(),
            };
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.Add(category)).ReturnsAsync(category);
            _categoryService = _serviceMock.Object;

            var categoryCreate = await _categoryService.Add(category);
            Assert.NotNull(categoryCreate);
            Assert.Equal(category.Id, categoryCreate.Id);
            Assert.Equal(category.Name, categoryCreate.Name);

            var categoryUpdate = categoryCreate;
            categoryUpdate.Name = Faker.Name.FullName();

            _serviceMock.Setup(m => m.Update(categoryUpdate)).ReturnsAsync(categoryUpdate);
            _categoryService = _serviceMock.Object;

            var result = await _categoryService.Update(categoryUpdate);
            
            Assert.Equal(categoryUpdate.Id, result.Id);
            Assert.Equal(categoryUpdate.Name, result.Name);
        }

        [Fact]
        public async Task DeleteCategory_WithValidParameters_ResultOk()
        {
            var idCategory = 1;
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.Remove(idCategory)).ReturnsAsync(true);
            _categoryService = _serviceMock.Object;

            var result = await _categoryService.Remove(idCategory);
            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCategory_WithInvalidParameters_ResultException()
        {
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.Remove(It.IsAny<int>())).ReturnsAsync(false);
            _categoryService = _serviceMock.Object;

            var result = await _categoryService.Remove(0);
            Assert.NotNull(result);
            Assert.False(result);
        }
    }
}