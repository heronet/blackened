using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [Authorize]
    public class UsersController : DefaultController
    {
        private readonly UserManager<EntityUser> _userManager;
        public UsersController(UserManager<EntityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityUser>>> GetUsers()
        {
            IEnumerable<EntityUser> users = await _userManager.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet("{username}", Name = "GetUserByUsername")]
        public async Task<ActionResult<EntityUser>> GetUserByUsername(string username)
        {
            EntityUser user = await _userManager.FindByNameAsync(username);
            if (user != null)
                return user;
            return BadRequest("Invalid Username");
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<EntityUser>> GetUserById(string id)
        {
            EntityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return Ok(user);
            return BadRequest("Invalid UserID");
        }
    }
}