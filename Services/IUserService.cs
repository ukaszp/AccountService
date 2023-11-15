using AccountApi.Entities;
using AccountApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Services
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto dto);
        public ActionResult<IEnumerable<User>> GetAll([FromQuery] string search);
        User GetById(int id);
        void DeleteUser(int id);
        /*void UpdateUser(int id, User user);*/
        public void AssignRole(int userId, int roleId);
        public string GenerateJwt(LoginDto dto);

    }
}