using AutoMapper;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.ImageDTOs;
using LibraryService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("library/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ImagesController(IImageRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<ImageDto>>> GetAllImages()
        {
            return await _repo.GetImagesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> GetImageById(Guid id)
        {
            var image = await _repo.GetImageByIdAsync(id);
            
            if (image == null) return NotFound();

            return image;
        }

        [HttpPost]
        public async Task<ActionResult<ImageDto>> CreateImage(CreateImageDto createImageDto)
        {
            var image = _mapper.Map<Image>(createImageDto);

            if (image.Book.Id != Guid.Empty)
            {
                var book = await _repo.GetBookEntityByIdAsync(image.Book.Id);
                if (book != null) image.Book = book;
                else return BadRequest("Book not found");
            }
            
            _repo.AddImage(image);

            var newImage = _mapper.Map<ImageDto>(image);

            // await _publishEndpoint.Publish(_mapper.Map<ImageCreated>(newImage));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while creating Image");

            return CreatedAtAction(nameof(GetImageById), new { id = image.Id }, _mapper.Map<ImageDto>(image));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ImageDto>> UpdateImage(Guid id, UpdateImageDto updateImageDto)
        {
            var image = await _repo.GetImageEntityByIdAsync(id);

            if (image == null) return NotFound();

            image.ImageUrl = updateImageDto.ImageUrl ?? image.ImageUrl;

            // await _publishEndpoint.Publish(_mapper.Map<ImageUpdated>(image));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while updating Image");

            return _mapper.Map<ImageDto>(image);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            var image = await _repo.GetImageEntityByIdAsync(id);

            if (image == null) return NotFound();

            _repo.RemoveImage(image);

            // await _publishEndpoint.Publish(_mapper.Map<ImageDeleted>(Image));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while deleting Image");

            return NoContent();
        }
    }
}
