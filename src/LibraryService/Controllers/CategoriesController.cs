using AutoMapper;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.CategoryDTOs;
using LibraryService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("library/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CategoriesController(ICategoryRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            return await _repo.GetCategoriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _repo.GetCategoryByIdAsync(id);
            
            if (category == null) return NotFound();

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            
            _repo.AddCategory(category);

            var newCategory = _mapper.Map<CategoryDto>(category);

            // await _publishEndpoint.Publish(_mapper.Map<CategoryCreated>(newCategory));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while creating Category");

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, _mapper.Map<CategoryDto>(category));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _repo.GetCategoryEntityByIdAsync(id);

            if (category == null) return NotFound();

            category.Name = updateCategoryDto.Name ?? category.Name;
            category.Description = updateCategoryDto.Description ?? category.Description;

            // await _publishEndpoint.Publish(_mapper.Map<CategoryUpdated>(category));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while updating Category");

            return _mapper.Map<CategoryDto>(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var category = await _repo.GetCategoryEntityByIdAsync(id);

            if (category == null) return NotFound();

            _repo.RemoveCategory(category);

            // await _publishEndpoint.Publish(_mapper.Map<CategoryDeleted>(category));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while deleting Category");

            return NoContent();
        }
    }
}
