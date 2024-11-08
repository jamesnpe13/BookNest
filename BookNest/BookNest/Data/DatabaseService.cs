using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        DbInit();
        TableInit();
        DbConnectionStatus = "Database connected status: " + System.Data.ConnectionState.Open;
    }

    // initialize database
    void DbInit()
    {
        Connection = new SQLiteConnection(DB_STRING);
        Connection.Open();

        if (Connection.State == System.Data.ConnectionState.Open)
        {
            Console.WriteLine(Connection.State == System.Data.ConnectionState.Open);
        }
    }

    // initialize tables
    void TableInit()
    {
        var createTableQuery = @"
            CREATE TABLE IF NOT EXISTS ToDoItems(
            Id    INTEGER NOT NULL UNIQUE,
            FirstName TEXT NOT NULL,
            LastName TEXT NOT NULL,
            Username TEXT NOT NULL UNIQUE,            
            PRIMARY KEY(Id AUTOINCREMENT))"
            ;

        using var cmdCreateTable = new SQLiteCommand(createTableQuery, Connection, Transaction);
        cmdCreateTable.ExecuteNonQuery();
        Console.WriteLine("Table 'ToDoItems' created successfully.");

    }

    public void AddItem(string title, string description)
    {
        var sql =
        "INSERT INTO ToDoItems(title, description)" +
        " VALUES(@title, @description)";

        using var cmdInsertBook = new SQLiteCommand(sql, Connection, Transaction);

        cmdInsertBook.Parameters.AddWithValue("@title", title);
        cmdInsertBook.Parameters.AddWithValue("@description", description);

        cmdInsertBook.ExecuteNonQuery();
    }

    public void RemoveItem(int id)
    {

    }

}
