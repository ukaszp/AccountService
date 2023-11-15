using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AccountApi.Entities;
using AccountApi.Models;
using AccountApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AccountApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        [HttpPost("register")]
        public ActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            _userService.CreateUser(dto);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get([FromRoute]int id)
        {
            var user=_userService.GetById(id);

           return Ok(user);
        }


        [HttpPut("{id}")]
        public ActionResult Update([FromBody] User user, [FromRoute]int id)
        {   
            _userService.UpdateUser(id, user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _userService.DeleteUser(id);

            return NoContent();
        }   
        [HttpPut("assignrole/{roleid}/{userid}")]
        public ActionResult AssignRole([FromRoute]int  roleid, [FromRoute]int userid) 
        {
            _userService.AssignRole(roleid, userid);
            return Ok();
        }

    }
}
