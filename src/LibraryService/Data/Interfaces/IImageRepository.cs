using LibraryService.DTOs.ImageDTOs;
using LibraryService.Entities;

namespace LibraryService.Data.Interfaces;

public interface IImageRepository
{
    Task<List<ImageDto>> GetImagesAsync();
    Task<ImageDto> GetImageByIdAsync(Guid id);
    Task<Image> GetImageEntityByIdAsync(Guid id);
    Task<Book> GetBookEntityByIdAsync(Guid id);
    void AddImage(Image Image);
    void RemoveImage(Image Image);
    Task<bool> SaveChangesAsync();
}
