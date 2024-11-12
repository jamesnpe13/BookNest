using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.SQLite;

namespace BookNest.Data;

public partial class DatabaseService : ObservableObject
{
    private readonly AppData ad;
    private readonly string DB_STRING = string.Empty;

    [ObservableProperty]
    private SQLiteConnection? connection;

    [ObservableProperty]
    private SQLiteTransaction? transaction;

    [ObservableProperty]
    private string dbConnectionStatus = string.Empty;

    public enum AccountFilterKey
    {
        ID,
        FIRST_NAME,
        LAST_NAME,
        USERNAME,
        EMAIL,
        ACCOUNT_TYPE
    }

    public enum BookFilterKey
    {
        ID,
        ISBN,
        TITLE,
        GENRE,
        AUTHOR,
        YEAR_OF_PUBLICATION,
        PUBLISHER,
        LIKES
    }

    public enum LoanTransactionFilterKey
    {
    }

    public DatabaseService(AppData _ad)
    {
        ad = _ad;
        DB_STRING = ad.DB_STRING;
        TableInit();
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
                Genre TEXT NOT NULL DEFAULT 'Unassigned',
                Author    TEXT NOT NULL DEFAULT 'Author unknown',
                YearOfPublication TEXT NOT NULL DEFAULT 'Year of publication unknown',
                Publisher TEXT NOT NULL DEFAULT 'Publisher unknown',
                Likes INTEGER NOT NULL DEFAULT 0,
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
                    Console.WriteLine("Account deletion SUCCESS");
                    Console.WriteLine(err.Message);
                }
            }
        }

    }

    /***********************************
    *      BOOKS
   ***********************************/

    // Add book
    public void AddBook(Book_M book)
    {
        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();

            string addItemSql = @"
            INSERT INTO Books (ISBN, Title, Genre, Author, YearOfPublication, Publisher, Likes)
            VALUES (@ISBN, @Title, @Genre, @Author, @YearOfPublication, @Publisher, @Likes)
            ";

            using (var command = new SQLiteCommand(addItemSql, connection))
            {
                command.Parameters.AddWithValue("@ISBN", book.Isbn);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Genre", book.Genre.ToString());
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@YearOfPublication", book.YearOfPublication);
                command.Parameters.AddWithValue("@Publisher", book.Publisher);
                command.Parameters.AddWithValue("@Likes", book.Likes);

                command.ExecuteNonQuery();
            }
        }
    }

    // Update book
    public void UpdateBook(int targetBookId, Book_M updateBook)
    {

    }

    // Get book - single overload
    public Book_M GetBook(BookFilterKey key, string value, bool returnSingle)
    {
        // parse string back to enum type
        Book_M tempBook = new();
        {
            // filter by ID
            if (key == BookFilterKey.ID)
            {
                using (var connection = new SQLiteConnection(DB_STRING))
                {
                    connection.Open();
                    string query = @"SELECT * FROM Books
                            WHERE BookID = @bookId
                            ";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@bookId", value);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var genreString = reader["Genre"].ToString() ?? BookGenre.Unassigned.ToString();

                                tempBook.BookId = reader.GetInt32(reader.GetOrdinal("BookID"));
                                tempBook.Isbn = reader["ISBN"].ToString() ?? string.Empty;
                                tempBook.Title = reader["Title"].ToString() ?? string.Empty;
                                tempBook.Author = reader["Author"].ToString() ?? string.Empty;
                                tempBook.Genre = Enum.Parse<BookGenre>(genreString);
                                tempBook.YearOfPublication = reader["YearOfPublication"].ToString() ?? string.Empty;
                                tempBook.Publisher = reader["Publisher"].ToString() ?? string.Empty;
                                tempBook.Likes = reader.GetInt32(reader.GetOrdinal("Likes"));

                                Console.WriteLine("Match found for " + key + " = " + value);
                                Console.WriteLine("BookID: " + tempBook.BookId);
                                Console.WriteLine("ISBN " + tempBook.Isbn);
                                Console.WriteLine("Title: " + tempBook.Title);
                                Console.WriteLine("Author: " + tempBook.Author);
                                Console.WriteLine("Genre: " + tempBook.Genre);
                                Console.WriteLine("YearOfPublication: " + tempBook.YearOfPublication);
                                Console.WriteLine("Publisher: " + tempBook.Publisher);
                                Console.WriteLine("Likes: " + tempBook.Likes);
                            }
                        }
                    }
                }
            }
        }
        return tempBook;
    }

    // Get book - list overload

    // Delete book

    /***********************************
    *      lOAN TRANSACTIONS
    ***********************************/

    // Add loan transacton

    // Read transaction

    // Delete transaction

}
