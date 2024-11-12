using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
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
            }
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

                            tempAccount.FirstName = reader["FirstName"].ToString() ?? string.Empty;
                            tempAccount.LastName = reader["Lastname"].ToString() ?? string.Empty;
                            tempAccount.Username = reader["Username"].ToString() ?? string.Empty;
                            tempAccount.Email = reader["Email"].ToString() ?? string.Empty;
                            tempAccount.Password = reader["Password"].ToString() ?? string.Empty;
                            tempAccount.PasswordHash = reader["PasswordHash"].ToString() ?? string.Empty;
                            tempAccount.Salt = reader["Salt"].ToString() ?? string.Empty;
                            tempAccount.AccountType = reader["AccountType"].ToString() ?? string.Empty;
                        }
                    }
                }
            }
        }
        return tempAccount;
    }

    public List<Account_M> GetAccount(bool returnSingle)
    {
        List<Account_M> tempAccounts = new();

        if (!returnSingle)
        {

            using (var connection = new SQLiteConnection(DB_STRING))
            {
                connection.Open();
                string query = @"SELECT * FROM Accounts";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Account_M tempAccount = new Account_M
                                {
                                    FirstName = reader["FirstName"].ToString() ?? string.Empty,
                                    LastName = reader["Lastname"].ToString() ?? string.Empty,
                                    Username = reader["Username"].ToString() ?? string.Empty,
                                    PasswordHash = reader["PasswordHash"].ToString() ?? string.Empty,
                                    Salt = reader["Salt"].ToString() ?? string.Empty,
                                    AccountType = reader["AccountType"].ToString() ?? string.Empty,
                                };
                                tempAccounts.Add(tempAccount);
                            }
                        }
                    }
                }
            }
        }
        return tempAccounts;
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
