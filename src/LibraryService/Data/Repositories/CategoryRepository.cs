using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.CategoryDTOs;
using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void AddCategory(Category Category)
    {
        _context.Categories.Add(Category);
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        return await _context.Categories.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
    {
        return await _context.Categories.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> GetCategoryEntityByIdAsync(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void RemoveCategory(Category Category)
    {
        _context.Categories.Remove(Category);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
