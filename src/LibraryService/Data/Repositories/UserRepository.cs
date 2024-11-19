using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.UserDTOs;
using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void AddUser(User User)
    {
        _context.Users.Add(User);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetUserEntityByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public void RemoveUser(User User)
    {
        _context.Users.Remove(User);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
