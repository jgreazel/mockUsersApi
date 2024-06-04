using Microsoft.AspNetCore.Mvc;
using UsersApi.Data;
using UsersApi.Models;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            var newUser = _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            var updatedUser = _userRepository.UpdateUser(id, user);
            return updatedUser != null ? Ok(updatedUser) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var success = _userRepository.DeleteUser(id);
            return success ? Ok() : NotFound();
        }
    }
}