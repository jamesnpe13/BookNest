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
    //          HANDLE DB AND TABLE INIT
    //************************************

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
                Status TEXT NOT NULL DEFAULT 'Available',
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
}
