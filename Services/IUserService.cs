using AccountApi.Entities;
using AccountApi.Models;

namespace AccountApi.Services
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto dto);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void DeleteUser(int id);
        void UpdateUser(int id, User user);
        public void AssignRole(int userId, int roleId);

    }
}