using BookNest.Components;
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                    NotificationService.Instance.AddNotificationItem(NotificationToastStyle.Success, "Created loan transaction");
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

    // Delete transaction
}
