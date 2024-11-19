using LibraryService.DTOs.UserDTOs;
using LibraryService.Entities;

namespace LibraryService.Data.Interfaces;

public interface IUserRepository
{
    Task<List<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<User> GetUserEntityByIdAsync(Guid id);
    void AddUser(User User);
    void RemoveUser(User User);
    Task<bool> SaveChangesAsync();
}
