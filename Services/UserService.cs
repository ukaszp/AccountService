﻿using AutoMapper;
using Azure.Identity;
using AccountApi.Authentication;
using AccountApi.Entities;
using AccountApi.Exceptions;
using AccountApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace AccountApi.Services
{
    public class UserService : IUserService
    {
        private readonly AccountDbContext dbContext;
        private readonly ILogger<UserService> logger;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        public UserService(AccountDbContext dbContext, ILogger<UserService> logger, IPasswordHasher passwordHasher, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
        }

        public User GetById(int id)
        {

            var user = dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            return user == null ? throw new NotFoundException("User not found") : user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = dbContext
                .Users
                .ToList();

            return users;
        }

        public void CreateUser(CreateUserDto dto)
        {
            var passwordHash=passwordHasher.Hash(dto.Password);
            var newUser = new User()
            { 
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                ContactNumber = dto.ContactNumber,
                PasswordHash = passwordHash,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            logger.LogWarning($"User with id: {id} DELETE action invoked");

            var user = dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            dbContext .Users.Remove(user);
            dbContext.SaveChanges ();
        }

        public void UpdateUser(int id, User user)
        {
            var userdb = dbContext
              .Users
              .FirstOrDefault(u => u.Id == id);

            if (userdb is null)
                throw new NotFoundException("User not found");

            userdb.Name=user.Name;
            userdb.Email=user.Email;
            userdb.ContactNumber = user.ContactNumber;
            userdb.DateOfBirth = user.DateOfBirth;
            userdb.Role = user.Role;

            dbContext.SaveChanges();
        }

        public void AssignRole(int userId, int roleId) 
        { 
            var userdb = dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);

            var roledb = dbContext
               .Roles
               .FirstOrDefault(u => u.Id == roleId);

            if (userdb is null)
                throw new NotFoundException("user not found");
            if (roledb is null)
                throw new NotFoundException("Role not found");

            userdb.RoleId = roleId; 
            userdb.Role = roledb;
            dbContext.SaveChanges();
        }

       
    }
}