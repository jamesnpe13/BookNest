using System.Collections.ObjectModel;
using System.Linq;
using System.Data.SQLite;
using BookNest.Models;

namespace BookNest.ViewModels
{
    public class BookViewModel
    {
        public ObservableCollection<Book_M> Books { get; set; }

        public BookViewModel()
        {
            Books = new ObservableCollection<Book_M>();
            LoadBooks();
        }

        private void LoadBooks()
        {
            using (var connection = new SQLiteConnection("Data Source=booknest.db"))
            {
                connection.Open();
                var query = "SELECT * FROM Books";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new Book_M
                        {
                            BookId = reader.GetInt32(reader.GetOrdinal("BookID")),
                            Isbn = reader["ISBN"].ToString(),
                            Title = reader["Title"].ToString(),
                            Author = reader["Author"].ToString(),
                            YearOfPublication = reader["YearOfPublication"].ToString(),
                            Publisher = reader["Publisher"].ToString(),
                            Likes = reader.GetInt32(reader.GetOrdinal("Likes"))
                        };
                        Books.Add(book);
                    }
                }
            }
        }
    }
}
