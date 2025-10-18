using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Add(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Domain.Entities.Category>(categoryDto);
            var result = await _categoryRepository.Create(categoryEntity);
            return _mapper.Map<CategoryDTO>(result);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var categoryEntity = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _categoryRepository.GetCategories();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<bool> Remove(int? id)
        {
            var categoryEntity = _categoryRepository.GetById(id).Result;
            if (categoryEntity == null)
            {
                return false;
            }
            await _categoryRepository.Remove(categoryEntity);
            return true;
        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Domain.Entities.Category>(categoryDto);
            var result = await _categoryRepository.Update(categoryEntity);
            return _mapper.Map<CategoryDTO>(result);
        }
    }
}
