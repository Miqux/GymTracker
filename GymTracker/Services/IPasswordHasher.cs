// GymTracker/Services/Security/IPasswordHasher.cs
using System;

namespace GymTracker.Services.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
