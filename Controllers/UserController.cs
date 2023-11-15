﻿using AutoMapper;
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
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll([FromQuery]string search)
        {
            var users = userService.GetAll(search);

            return Ok(users);
        }

        [HttpPost("register")]
        public ActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            userService.CreateUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = userService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get([FromRoute]int id)
        {
            var user=userService.GetById(id);

           return Ok(user);
        }


      /*  [HttpPut("{id}")]
        public ActionResult Update([FromBody] User user, [FromRoute]int id)
        {   
            userService.UpdateUser(id, user);

            return Ok();
        }*/

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            userService.DeleteUser(id);

            return NoContent();
        }   
        [HttpPut("assignrole/{roleid}/{userid}")]
        public ActionResult AssignRole([FromRoute]int  roleid, [FromRoute]int userid) 
        {
            userService.AssignRole(roleid, userid);
            return Ok();
        }

    }
}
