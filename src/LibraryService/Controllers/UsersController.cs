using AutoMapper;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.UserDTOs;
using LibraryService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UsersController(IUserRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return await _repo.GetUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            
            if (user == null) return NotFound();

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            
            _repo.AddUser(user);

            var newUser = _mapper.Map<UserDto>(user);

            // await _publishEndpoint.Publish(_mapper.Map<UserCreated>(newUser));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while creating user");

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, _mapper.Map<UserDto>(user));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await _repo.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            user.Name = updateUserDto.Name ?? user.Name;
            user.BirthDate = updateUserDto.BirthDate ?? user.BirthDate;
            user.Email = updateUserDto.Email ?? user.Email;
            user.Password = updateUserDto.Password ?? user.Password;

            // await _publishEndpoint.Publish(_mapper.Map<UserUpdated>(user));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while updating user");

            return _mapper.Map<UserDto>(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _repo.GetUserEntityByIdAsync(id);

            if (user == null) return NotFound();

            _repo.RemoveUser(user);

            // await _publishEndpoint.Publish(_mapper.Map<UserDeleted>(user));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while deleting user");

            return NoContent();
        }
    }
}
