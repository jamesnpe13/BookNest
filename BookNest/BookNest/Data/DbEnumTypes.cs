using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Data;

partial class DatabaseService : ObservableObject
{

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
        ALL,
        ID,
        ISBN,
        TITLE,
        GENRE,
        AUTHOR,
        YEAR_OF_PUBLICATION,
        PUBLISHER,
        STATUS,
        LIKES
    }

    public enum LoanTransactionFilterKey
    {
    }
}
