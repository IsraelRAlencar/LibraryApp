namespace LibraryService.DTOs.UserDTOs;

public class UpdateUserDto
{
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
