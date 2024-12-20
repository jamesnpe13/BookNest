﻿using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Data;

partial class DatabaseService : ObservableObject
{

    //************************************
    //          HANDLE ACCOUNT QUERIES
    //************************************

    // Add account
    public void AddAccount(Account_M account)
    {
        try
        {

            using (var connection = new SQLiteConnection(DB_STRING))
            {
                connection.Open();

                string addItemSql =
                    @"
                    INSERT INTO Accounts (FirstName, LastName, Username, Password, PasswordHash, Salt, Email, AccountType) 
                    VALUES (@FirstName, @LastName, @Username, @Password, @PasswordHash, @Salt, @Email, @AccountType)
                ";

                using (var command = new SQLiteCommand(addItemSql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", account.FirstName);
                    command.Parameters.AddWithValue("@LastName", account.LastName);
                    command.Parameters.AddWithValue("@Username", account.Username);
                    command.Parameters.AddWithValue("@Password", account.Password);
                    command.Parameters.AddWithValue("@PasswordHash", account.PasswordHash);
                    command.Parameters.AddWithValue("@Salt", account.Salt);
                    command.Parameters.AddWithValue("@Email", account.Email);
                    command.Parameters.AddWithValue("@AccountType", account.AccountType);

                    command.ExecuteNonQuery();
                    NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Success, $"{account.AccountType} account successfully created.");
                }
            }
        }
        catch (Exception err)
        {
            if (err.Message == "constraint failed\r\nUNIQUE constraint failed: Accounts.Username")
                throw new Exception("Username already exists.");
        }
    }

    // Update account
    public void UpdateAccount(string targetUsername, string targetAccountType, Account_M updatedAccount)
    {
        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                        UPDATE Accounts
                        SET FirstName = @FirstName,
                            LastName = @LastName,
                            Username = @Username,
                            Username = @Password,
                            PasswordHash = @PasswordHash,
                            Salt = @Salt,
                            Email = @Email,
                            AccountType = @AccountType
                        WHERE Username = @targetUsername AND AccountType = @targetAccountType
                        
                        ";

                        command.Parameters.AddWithValue("@FirstName", updatedAccount.FirstName);
                        command.Parameters.AddWithValue("@LastName", updatedAccount.LastName);
                        command.Parameters.AddWithValue("@Username", updatedAccount.Username);
                        command.Parameters.AddWithValue("@Password", updatedAccount.Password);
                        command.Parameters.AddWithValue("@PasswordHash", updatedAccount.PasswordHash);
                        command.Parameters.AddWithValue("@Salt", updatedAccount.Salt);
                        command.Parameters.AddWithValue("@Email", updatedAccount.Email);
                        command.Parameters.AddWithValue("@AccountType", updatedAccount.AccountType);
                        command.Parameters.AddWithValue("@targetUsername", targetUsername);
                        command.Parameters.AddWithValue("@targetAccountType", targetAccountType);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine("Account update SUCCESS");
                }
                catch (Exception err)
                {
                    transaction.Rollback();
                    Console.WriteLine("Account update FAILED");
                    Console.WriteLine(err.Message);
                }
            }
        }
    }

    // Read account
    public Account_M GetAccount(string username, string accountType, bool returnSingle)
    {

        Account_M tempAccount = new();

        if (returnSingle)
        {
            using (var connection = new SQLiteConnection(DB_STRING))
            {
                connection.Open();
                string query = @"SELECT * FROM Accounts
                            WHERE Username = @Username
                            AND AccountType = @AccountType
                            ";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@AccountType", accountType);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.StepCount == 0) return null;

                        while (reader.Read())
                        {

                            tempAccount.AccountId = reader.GetInt32(reader.GetOrdinal("UserID"));
                            tempAccount.FirstName = reader["FirstName"].ToString() ?? string.Empty;
                            tempAccount.LastName = reader["Lastname"].ToString() ?? string.Empty;
                            tempAccount.Username = reader["Username"].ToString() ?? string.Empty;
                            tempAccount.Email = reader["Email"].ToString() ?? string.Empty;
                            tempAccount.Password = reader["Password"].ToString() ?? string.Empty;
                            tempAccount.AccountType = reader["AccountType"].ToString() ?? string.Empty;
                        }
                    }
                }
            }
        }
        return tempAccount;
    }

    public ObservableCollection<Account_M> GetAccount(AccountFilterKey key = AccountFilterKey.ALL, string? value = null)
    {
        ObservableCollection<Account_M> tempAccountList = new();

        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();
            string query = string.Empty;

            switch (key)
            {
                case AccountFilterKey.ALL:
                    query = @"
                    SELECT * FROM Accounts
                    ";
                    break;
                case AccountFilterKey.ID:
                    query = @"
                    SELECT * FROM Accounts
                    WHERE UserID = @value
                    ";
                    break;
                case AccountFilterKey.FIRST_NAME:
                    query = @"
                    SELECT * FROM Accounts
                    WHERE FirstName = @value
                    ";
                    break;
                case AccountFilterKey.LAST_NAME:
                    query = @"
                    SELECT * FROM Accounts
                    WHERE LastName = @value
                    ";
                    break;
                case AccountFilterKey.USERNAME:
                    query = @"
                    SELECT * FROM Accounts
                    WHERE Username = @value
                    ";
                    break;
                case AccountFilterKey.EMAIL:
                    query = @"
                    SELECT * FROM Accounts
                    WHERE Email = @value
                    ";
                    break;
                case AccountFilterKey.ACCOUNT_TYPE:
                    query = @"
                    SELECT * FROM Accounts
                    WHERE AccountType = @value
                    ";
                    break;
            }

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@value", value);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Account_M tempAccount = new();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            tempAccount.AccountId = reader.GetInt32(reader.GetOrdinal("UserID"));
                            tempAccount.FirstName = reader["FirstName"].ToString();
                            tempAccount.LastName = reader["LastName"].ToString();
                            tempAccount.Username = reader["Username"].ToString();
                            tempAccount.Email = reader["Email"].ToString();
                            tempAccount.AccountType = reader["AccountType"].ToString();

                        };
                        tempAccountList.Add(tempAccount);
                    }
                }
            }

        }

        return tempAccountList;
    }

    // Delete account
    public void DeleteAccount(string targetUsername, string targetAccountType)
    {

        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                        DELETE FROM Accounts
                        WHERE Username = @Username
                        AND AccountType = @AccountType
                        ";

                        command.Parameters.AddWithValue("@Username", targetUsername);
                        command.Parameters.AddWithValue("@AccountType", targetAccountType);

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    Console.WriteLine("Account deletion SUCCESS");
                }
                catch (Exception err)
                {
                    Console.WriteLine("Account deletion FAILED");
                    Console.WriteLine(err.Message);
                }
            }
        }

    }
}
