using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.SQLite;

namespace BookNest.Data;

public partial class DatabaseService : ObservableObject
{
    private readonly AppData ad;
    private readonly string DB_STRING;

    [ObservableProperty]
    private SQLiteConnection connection;

    [ObservableProperty]
    private SQLiteTransaction transaction;

    [ObservableProperty]
    private string dbConnectionStatus;

    public DatabaseService(AppData _ad)
    {
        ad = _ad;
        DB_STRING = ad.DB_STRING;
        TableInit();

        //AddTestAccount();
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
                Password TEXT NOT NULL,
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
                        while (reader.Read())
                        {
                            tempAccount.FirstName = reader["FirstName"].ToString();
                            tempAccount.LastName = reader["Lastname"].ToString();
                            tempAccount.Username = reader["Username"].ToString();
                            tempAccount.Email = reader["Email"].ToString();
                            tempAccount.Password = reader["Password"].ToString();
                            tempAccount.PasswordHash = reader["PasswordHash"].ToString();
                            tempAccount.Salt = reader["Salt"].ToString();
                            tempAccount.AccountType = reader["AccountType"].ToString();
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
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["Lastname"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    PasswordHash = reader["PasswordHash"].ToString(),
                                    Salt = reader["Salt"].ToString(),
                                    AccountType = reader["AccountType"].ToString(),
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
                    Console.WriteLine("Account deletion SUCCESS");
                    Console.WriteLine(err.Message);
                }
            }
        }

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
}
