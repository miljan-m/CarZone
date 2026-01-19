using CarZone.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CarZone.Infrastructure.Persistance;
using System.Linq;

namespace CarZone.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController:ControllerBase
    {
        private readonly CarZoneDBContext _context;
        public UserController(CarZoneDBContext context)
        {
            _context=context;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser([FromRoute]int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound(); // eksplicitno 404
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users=_context.Users.ToList();
            return users;
        }


        [HttpDelete("{id})")]
        public IActionResult DeleteUser([FromRoute]int id)
        {
            var userToDelete=_context.Users.FirstOrDefault(u=>u.UserId==id);
            if(userToDelete==null) return NotFound();
            _context.Remove(userToDelete);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromRoute]int id,[FromBody]User user)
        {
            var userToUpdate=_context.Users.FirstOrDefault(u=>u.UserId==id);
            if(userToUpdate==null) return NotFound();
            userToUpdate.Name=user.Name;
            userToUpdate.LastName=user.LastName;
            userToUpdate.Email=user.Email;
            userToUpdate.Address=user.Address;
            userToUpdate.Phone=user.Phone;
            _context.SaveChanges();
            return Ok();
        }
    }
}