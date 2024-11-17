using BookNest.Data;  // Import the correct namespace
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows;
using System.Collections.ObjectModel;

namespace BookNest
{
    public partial class Member_Books_V : UserControl
    {
        // Connection string to your SQLite database
        private const string ConnectionString = "Data Source=booknest.db;Version=3;";

        // ObservableCollection to store the books
        public ObservableCollection<Book_M> Books { get; set; } = new ObservableCollection<Book_M>();

        public Member_Books_V()
        {
            InitializeComponent();

            // Load the books from the SQLite database
            LoadBooks();

            // Bind the DataContext to the current instance
            DataContext = this;
        }

        // Method to load books from the SQLite database
        private void LoadBooks()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Books";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            int rowCount = 0; // Counter to check if rows are being added
                            while (reader.Read())
                            {
                                Books.Add(new Book_M
                                {
                                    BookId = reader.IsDBNull(0) ? null : reader.GetInt32(0),
                                    Isbn = reader.GetString(1),
                                    Title = reader.GetString(2),
                                    Status = Enum.TryParse(reader.GetString(3), out BookStatus status) ? status : BookStatus.Available,
                                    Author = reader.GetString(4),
                                    Genre = Enum.TryParse(reader.GetString(5), out BookGenre genre) ? genre : BookGenre.Unassigned,
                                    YearOfPublication = reader.GetString(6),
                                    Publisher = reader.GetString(7),
                                    Likes = reader.IsDBNull(8) ? 0 : reader.GetInt32(8)
                                });
                                rowCount++;
                            }

                            if (rowCount == 0)
                            {
                                MessageBox.Show("No books found in the database.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}");
            }
        }

    }
}
