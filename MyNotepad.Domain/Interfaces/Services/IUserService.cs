﻿using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Requests;

namespace MyNotepad.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public UserDTO ValidateAndSignUpUser(UserRegisterRequest user);
        public Dictionary<string, string> Login(LoginRequest login);
        public UserDTO GetUserData(int id);
        public UserDTO Update(UserDTO user);
        public UserDTO Delete(string password, int id);
    }
}
