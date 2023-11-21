using AccountApi.Entities;
using AccountApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Services
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto dto);
        public IEnumerable<User> GetAll(string search);
        User GetById(int id);
        void DeleteUser(int id);
        /*void UpdateUser(int id, User user);*/
        public void AssignRole(int userId, int roleId);
        public string GenerateJwt(LoginDto dto);
        public User GetUserLogin(LoginDto dto);

    }
}