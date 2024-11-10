using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Documents;
using System.Xml;
using BookNest.Models;

namespace BookNest.Data;

public partial class DatabaseService : ObservableObject
{
    private static readonly object _lock = new object();

    readonly string DB_STRING = @"Data Source=booknest.db";

    [ObservableProperty]
    private SQLiteConnection connection;

    [ObservableProperty]
    private SQLiteTransaction transaction;

    [ObservableProperty]
    private string dbConnectionStatus;

    public DatabaseService()
    {
        TableInit();

        //AddTestAccount();
    }

    void AddTestAccount()
    {
        var TestAccount = new Account_M()
        {
            FirstName = "James",
            LastName = "Elazegui",
            Username = "jamesnpe13",
            Email = "jameselazegui21@gmail.com",
            AccountType = "Admin",
        };

        AddAccount(TestAccount);
    }

    void UpdateDbConnectionStatus()
    {
        DbConnectionStatus = "Database connection status: " + System.Data.ConnectionState.Open;
    }

    // initialize tables
    void TableInit()
    {
        Connection = new SQLiteConnection(DB_STRING);
        Connection.Open();
        UpdateDbConnectionStatus();

        // CREATE TABLE FOR ACCOUNTS
        var createAccountsTable =
            @"
                CREATE TABLE IF NOT EXISTS Accounts(
                UserID    INTEGER NOT NULL UNIQUE,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                Username TEXT NOT NULL UNIQUE,
                PasswordHash TEXT NOT NULL DEFAULT 'password hash not set',
                Salt TEXT NOT NULL,
                Email TEXT,
                AccountType TEXT NOT NULL,
                PRIMARY KEY(UserID AUTOINCREMENT))
            ";

        using var cmdCreateAccountsTable = new SQLiteCommand(createAccountsTable, Connection, Transaction);

        // CREATE TABLE FOR BOOKS
        var createBooksTable =
            @"
                CREATE TABLE IF NOT EXISTS Books(
                BookID    INTEGER NOT NULL UNIQUE,
                ISBN  TEXT NOT NULL DEFAULT 'No ISBN' UNIQUE,
                Title TEXT NOT NULL DEFAULT 'No Title',
                Author    TEXT NOT NULL DEFAULT 'Author unknown',
                YearOfPublication TEXT NOT NULL DEFAULT 'Year of publication unknown',
                Publisher TEXT NOT NULL DEFAULT 'Publisher unknown',
                PRIMARY KEY(BookID AUTOINCREMENT))
            ";

        using var cmdCreateBooksTable = new SQLiteCommand(createBooksTable, Connection, Transaction);

        // CREATE TABLE FOR LOAN TRANSACTIONS
        var createLoanTransactionsTable =
            @"
                CREATE TABLE IF NOT EXISTS LoanTransactions(
                TransactionID    INTEGER NOT NULL UNIQUE,
                MemberId  INTEGER NOT NULL,
                BookId    INTEGER NOT NULL,
                LoanDate  TEXT NOT NULL,
                DueDate   TEXT NOT NULL,
                ReturnDate    INTEGER,
                Status    TEXT,
                PRIMARY KEY(TransactionID AUTOINCREMENT))
            ";

        using (var cmdCreateLoanTransactionsTable = new SQLiteCommand(createLoanTransactionsTable, Connection, Transaction))
        {
            cmdCreateAccountsTable.ExecuteNonQuery();
            cmdCreateBooksTable.ExecuteNonQuery();
            cmdCreateLoanTransactionsTable.ExecuteNonQuery();
        };
        UpdateDbConnectionStatus();
    }

    /***********************************
     *      ACCOUNTS
    ***********************************/

    // Add account
    public void AddAccount(Account_M account)
    {
        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();

            string addItemSql =
                @"
                    INSERT INTO Accounts (FirstName, LastName, Username, PasswordHash, Salt, Email, AccountType) 
                    VALUES (@FirstName, @LastName, @Username, @PasswordHash, @Salt, @Email, @AccountType)
                ";

            using (var command = new SQLiteCommand(addItemSql, connection))
            {
                command.Parameters.AddWithValue("@FirstName", account.FirstName);
                command.Parameters.AddWithValue("@LastName", account.LastName);
                command.Parameters.AddWithValue("@Username", account.Username);
                command.Parameters.AddWithValue("@PasswordHash", account.PasswordHash);
                command.Parameters.AddWithValue("@Salt", account.Salt);
                command.Parameters.AddWithValue("@Email", account.Email);
                command.Parameters.AddWithValue("@AccountType", account.AccountType);

                command.ExecuteNonQuery();
            }
        }
    }

    // Update account

    // Read account
    public List<Account_M> GetAccount()
    {
        List<Account_M> tempAccounts = new();

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
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["Lastname"].ToString(),
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Salt = reader["Salt"].ToString(),
                                AccountType = reader["AccountType"].ToString(),
                            };

                            tempAccounts.Add(tempAccount);

                            Console.WriteLine($"{reader.GetName(i)}: {reader.GetValue(i)}");

                        }
                    }
                }
            }
        }

        return tempAccounts;
    }

    // Delete account

    /***********************************
     *      BOOKS
    ***********************************/

    // Add book

    // Update book

    // Read book

    // Delete book

    /***********************************
     *      lOAN TRANSACTIONS
    ***********************************/

    // Add loan transacton

    // Read transaction

    // Delete transaction

}
