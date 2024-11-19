using LibraryService.DTOs.CategoryDTOs;
using LibraryService.Entities;

namespace LibraryService.Data.Interfaces;

public interface ICategoryRepository
{
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(Guid id);
    Task<Category> GetCategoryEntityByIdAsync(Guid id);
    void AddCategory(Category Category);
    void RemoveCategory(Category Category);
    Task<bool> SaveChangesAsync();
}
