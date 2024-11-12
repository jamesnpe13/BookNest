using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.SQLite;

namespace BookNest.Data;

partial class DatabaseService : ObservableObject
{
    //************************************
    //          HANDLE BOOK QUERIES
    //************************************

    // Add book
    public void AddBook(Book_M book)
    {
        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();

            string addItemSql = @"
            INSERT INTO Books (ISBN, Title, Genre, Author, YearOfPublication, Publisher, Likes, Status)
            VALUES (@ISBN, @Title, @Genre, @Author, @YearOfPublication, @Publisher, @Likes, @Status)
            ";

            using (var command = new SQLiteCommand(addItemSql, connection))
            {
                command.Parameters.AddWithValue("@ISBN", book.Isbn);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Genre", book.Genre.ToString());
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Status", book.Status.ToString());
                command.Parameters.AddWithValue("@YearOfPublication", book.YearOfPublication);
                command.Parameters.AddWithValue("@Publisher", book.Publisher);
                command.Parameters.AddWithValue("@Likes", book.Likes);

                command.ExecuteNonQuery();
            }
        }
    }

    // Update book
    public void UpdateBook(string targetBookId, Book_M updatedBook)
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
                        UPDATE Books
                        SET ISBN = @ISBN,
                            Title = @Title,
                            Genre = @Genre,
                            Author = @Author,
                            Status = @Status,
                            YearOfPublication = @YearOfPublication,
                            Publisher = @Publisher,
                            Likes = @Likes
                        WHERE BookID = @targetBookID
                        ";

                        command.Parameters.AddWithValue("@targetBookID", targetBookId);
                        command.Parameters.AddWithValue("@ISBN", updatedBook.Isbn);
                        command.Parameters.AddWithValue("@Title", updatedBook.Title);
                        command.Parameters.AddWithValue("@Genre", updatedBook.Genre.ToString());
                        command.Parameters.AddWithValue("@Author", updatedBook.Author);
                        command.Parameters.AddWithValue("@Status", updatedBook.Status.ToString());
                        command.Parameters.AddWithValue("@YearOfPublication", updatedBook.YearOfPublication);
                        command.Parameters.AddWithValue("@Publisher", updatedBook.Publisher);
                        command.Parameters.AddWithValue("@Likes", updatedBook.Likes);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine("Book update SUCCESS");
                }
                catch (Exception err)
                {
                    transaction.Rollback();
                    Console.WriteLine("Book update FAILED");
                    Console.WriteLine(err.Message);
                }
            }
        }
    }

    // Get book - single overload
    public Book_M GetBook(BookFilterKey key, string value, bool returnSingle)
    {
        Book_M tempBook = new();
        {
            if (returnSingle)
            {
                // filter by ID
                if (key == BookFilterKey.ID)
                {
                    using (var connection = new SQLiteConnection(DB_STRING))
                    {
                        connection.Open();
                        string query = @"
                            SELECT * FROM Books
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
                                    var statusString = reader["Status"].ToString() ?? BookStatus.Available.ToString();

                                    tempBook.BookId = reader.GetInt32(reader.GetOrdinal("BookID"));
                                    tempBook.Isbn = reader["ISBN"].ToString() ?? string.Empty;
                                    tempBook.Title = reader["Title"].ToString() ?? string.Empty;
                                    tempBook.Author = reader["Author"].ToString() ?? string.Empty;
                                    tempBook.Genre = Enum.Parse<BookGenre>(genreString);
                                    tempBook.Status = Enum.Parse<BookStatus>(statusString);
                                    tempBook.YearOfPublication = reader["YearOfPublication"].ToString() ?? string.Empty;
                                    tempBook.Publisher = reader["Publisher"].ToString() ?? string.Empty;
                                    tempBook.Likes = reader.GetInt32(reader.GetOrdinal("Likes"));

                                    //Console.WriteLine("Match found for " + key + " = " + value);
                                    //Console.WriteLine("BookID: " + tempBook.BookId);
                                    //Console.WriteLine("ISBN " + tempBook.Isbn);
                                    //Console.WriteLine("Title: " + tempBook.Title);
                                    //Console.WriteLine("Author: " + tempBook.Author);
                                    //Console.WriteLine("Genre: " + tempBook.Genre);
                                    //Console.WriteLine("Status: " + tempBook.Status);
                                    //Console.WriteLine("YearOfPublication: " + tempBook.YearOfPublication);
                                    //Console.WriteLine("Publisher: " + tempBook.Publisher);
                                    //Console.WriteLine("Likes: " + tempBook.Likes);
                                }
                            }
                        }
                    }
                }

            }
        }
        return tempBook;
    }

    // Get book - list overload
    public List<Book_M> GetBook(BookFilterKey key = BookFilterKey.ALL, string? value = null)
    {
        List<Book_M> tempBookList = new();

        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();
            string query = string.Empty;

            switch (key)
            {
                case BookFilterKey.ALL:
                    query = @"
                                SELECT * FROM Books
                                ";
                    break;
                case BookFilterKey.ID:
                    query = @"
                                SELECT * FROM Books
                                WHERE BookId = @value
                                ";
                    break;
                case BookFilterKey.ISBN:
                    query = @"
                                SELECT * FROM Books
                                WHERE ISBN = @value
                                ";
                    break;
                case BookFilterKey.TITLE:
                    query = @"
                                SELECT * FROM Books
                                WHERE Title = @value
                                ";
                    break;
                case BookFilterKey.GENRE:
                    query = @"
                                SELECT * FROM Books
                                WHERE Genre = @value
                                ";
                    break;
                case BookFilterKey.AUTHOR:
                    query = @"
                                SELECT * FROM Books
                                WHERE Author = @value
                                ";
                    break;
                case BookFilterKey.YEAR_OF_PUBLICATION:
                    query = @"
                                SELECT * FROM Books
                                WHERE YearOfPublication = @value
                                ";
                    break;
                case BookFilterKey.PUBLISHER:
                    query = @"
                                SELECT * FROM Books
                                WHERE Publisher = @value
                                ";
                    break;
                case BookFilterKey.STATUS:
                    query = @"
                                SELECT * FROM Books
                                WHERE Status = @value
                                ";
                    break;
                case BookFilterKey.LIKES:
                    query = @"
                                SELECT * FROM Books
                                WHERE Likes = @value
                                ";
                    break;
            }

            using (var command = new SQLiteCommand(query, connection))
            {
                // changes sql query string based on filter key and value sent as argument

                // pass value from argument to query param
                command.Parameters.AddWithValue("@value", value);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book_M tempBook = new();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {

                            var genreString = reader["Genre"].ToString() ?? BookGenre.Unassigned.ToString();
                            var statusString = reader["Status"].ToString() ?? BookStatus.Available.ToString();

                            tempBook.BookId = reader.GetInt32(reader.GetOrdinal("BookID"));
                            tempBook.Isbn = reader["ISBN"].ToString() ?? string.Empty;
                            tempBook.Title = reader["Title"].ToString() ?? string.Empty;
                            tempBook.Author = reader["Author"].ToString() ?? string.Empty;
                            tempBook.Genre = Enum.Parse<BookGenre>(genreString);
                            tempBook.Status = Enum.Parse<BookStatus>(statusString);
                            tempBook.YearOfPublication = reader["YearOfPublication"].ToString() ?? string.Empty;
                            tempBook.Publisher = reader["Publisher"].ToString() ?? string.Empty;
                            tempBook.Likes = reader.GetInt32(reader.GetOrdinal("Likes"));

                        }

                        tempBookList.Add(tempBook);
                        //Console.WriteLine("Match found for " + key + (!string.IsNullOrEmpty(value) ? (" = " + value) : ""));
                        //Console.WriteLine("BookID: " + tempBook.BookId);
                        //Console.WriteLine("ISBN " + tempBook.Isbn);
                        //Console.WriteLine("Title: " + tempBook.Title);
                        //Console.WriteLine("Author: " + tempBook.Author);
                        //Console.WriteLine("Genre: " + tempBook.Genre);
                        //Console.WriteLine("Status: " + tempBook.Status);
                        //Console.WriteLine("YearOfPublication: " + tempBook.YearOfPublication);
                        //Console.WriteLine("Publisher: " + tempBook.Publisher);
                        //Console.WriteLine("Likes: " + tempBook.Likes);

                    }
                }

            }

        }

        return tempBookList;
    }

    // Delete book
    public void DeleteBook(string targetId)
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
                        DELETE FROM Books
                        WHERE BookID = @targetID
                        ";

                        command.Parameters.AddWithValue("@targetID", targetId);

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    Console.WriteLine("Book deletion SUCCESS");
                }
                catch (Exception err)
                {
                    Console.WriteLine("Book deletion FAILED");
                    Console.WriteLine(err.Message);
                }
            }
        }
    }
}
