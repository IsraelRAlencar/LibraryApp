using System.ComponentModel.DataAnnotations;

namespace LibraryService.DTOs.UserDTOs;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
