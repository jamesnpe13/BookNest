using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services;

public partial class PasswordManager : ObservableObject
{
    public PasswordManager()
    {
    }

    // generate random salt
    public byte[] GenerateSalt(int size = 16)
    {
        var salt = new byte[size];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    // hash the password with the salt
    public string HashPassword(string password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combineBytes = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, combineBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, combineBytes, 0, salt.Length);

            byte[] hashBytes = sha256.ComputeHash(combineBytes);

            return Convert.ToBase64String(hashBytes);
        }

    }

    // verify password
    //public bool VerifyPassword(string passwordInput, byte[] storedSalt, string storedHash)
    //{
    //    Console.WriteLine("Verifying password");
    //    string hashOfPasswordInput = HashPassword(passwordInput, storedSalt);
    //    return hashOfPasswordInput == storedHash;
    //}

    // verify password (string)
    public bool VerifyPassword(string passwordInput, Account_M targetAccount)
    {
        return passwordInput == targetAccount.Password;
    }

}
