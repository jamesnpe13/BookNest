using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.SQLite;

namespace BookNest.Data;

public partial class DatabaseService : ObservableObject
{
    // THIS DB SERVICE CLASS IS SPLIT INTO MULTIPLE PARTIAL FILES UNDER 'Data' directory

    private readonly AppData ad;
    private readonly string DB_STRING = string.Empty;

    [ObservableProperty]
    private SQLiteConnection? connection;

    [ObservableProperty]
    private SQLiteTransaction? transaction;

    [ObservableProperty]
    private string dbConnectionStatus = string.Empty;

    public DatabaseService(AppData _ad)
    {
        ad = _ad;
        DB_STRING = ad.DB_STRING;
        TableInit();
    }

}
