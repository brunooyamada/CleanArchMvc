using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetById(int? id);
        Task<CategoryDTO> Add(CategoryDTO categoryDto);
        Task<CategoryDTO> Update(CategoryDTO categoryDto);
        Task<bool> Remove(int? id);
    }
}
