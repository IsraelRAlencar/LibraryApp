using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.ImageDTOs;
using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public ImageRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void AddImage(Image Image)
    {
        _context.Images.Add(Image);
    }

    public async Task<ImageDto> GetImageByIdAsync(Guid id)
    {
        return await _context.Images.ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Image> GetImageEntityByIdAsync(Guid id)
    {
        return await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<ImageDto>> GetImagesAsync()
    {
        return await _context.Images.ProjectTo<ImageDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public void RemoveImage(Image Image)
    {
        _context.Images.Remove(Image);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
