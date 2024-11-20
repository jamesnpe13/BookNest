using BookNest.Components;
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows.Input;

namespace BookNest.Data;

partial class DatabaseService : ObservableObject
{
    //************************************
    //          HANDLE LOAN TRANSACTION QUERIES
    //************************************

    // create loan transacton
    public void AddLoanTransaction(LoanTransaction_M loanTransaction)
    {
        try
        {
            using (var connection = new SQLiteConnection(DB_STRING))
            {
                connection.Open();

                string addItemSql = @"
                INSERT INTO LoanTransactions (AccountID, BookID, LoanDate, DueDate, Status)
                VALUES (@AccountID, @BookID, @LoanDate, @DueDate, @Status)
                ";

                using (var command = new SQLiteCommand(addItemSql, connection))
                {
                    command.Parameters.AddWithValue("@AccountID", loanTransaction.AccountId);
                    command.Parameters.AddWithValue("@BookID", loanTransaction.BookId);
                    command.Parameters.AddWithValue("@LoanDate", loanTransaction.LoanDate);
                    command.Parameters.AddWithValue("@DueDate", loanTransaction.DueDate);
                    command.Parameters.AddWithValue("@Status", loanTransaction.Status.ToString());

                    // exception handling here...

                    command.ExecuteNonQuery();
                    NotificationService.Instance.AddNotificationItem(NotificationToastStyle.Success, "Entry created");
                }
            }
        }
        catch (Exception err)
        {
            NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Error, err.Message);
            throw new Exception(err.Message);
        }
    }

    // Read transaction

    public ObservableCollection<LoanTransaction_M> GetLoanTransaction(LoanTransactionFilterKey key = LoanTransactionFilterKey.ALL, string? value = null)
    {
        ObservableCollection<LoanTransaction_M> tempLoanTransactionList = new();

        using (var connection = new SQLiteConnection(DB_STRING))
        {
            connection.Open();
            string query = string.Empty;

            switch (key)
            {
                case LoanTransactionFilterKey.ALL:
                    query = @"
                    SELECT * FROM LoanTransactions
                    ";
                    break;
                case LoanTransactionFilterKey.BOOK_ID:
                    query = @"
                    SELECT * FROM LoanTransactions
                    WHERE BookID = @value
                    ";
                    break;
                case LoanTransactionFilterKey.ACCOUNT_ID:
                    query = @"
                    SELECT * FROM LoanTransactions
                    WHERE AccountID = @value
                    ";
                    break;
                case LoanTransactionFilterKey.STATUS:
                    query = @"
                    SELECT * FROM LoanTransactions
                    WHERE Status = @value
                    ";
                    break;
                case LoanTransactionFilterKey.TRANSACTION_ID:
                    query = @"
                    SELECT * FROM LoanTransactions
                    WHERE TransactionID = @value
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
                        LoanTransaction_M tempLoanTransaction = new();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            tempLoanTransaction.LoanTransactionId = reader.GetInt32(reader.GetOrdinal("TransactionID"));
                            tempLoanTransaction.AccountId = reader.GetInt32(reader.GetOrdinal("AccountID"));
                            tempLoanTransaction.BookId = reader.GetInt32(reader.GetOrdinal("BookID"));
                            tempLoanTransaction.LoanDate = DateOnly.ParseExact(reader["LoanDate"].ToString(), "dd/MM/yyyy");
                            tempLoanTransaction.DueDate = DateOnly.ParseExact(reader["DueDate"].ToString(), "dd/MM/yyyy");
                            tempLoanTransaction.Status = Enum.Parse<LoanStatus>(reader["Status"].ToString());
                        }

                        tempLoanTransactionList.Add(tempLoanTransaction);
                    }
                }
            }
        }

        return tempLoanTransactionList;
    }

    // Delete transaction
}
