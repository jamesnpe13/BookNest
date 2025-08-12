using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Data;

partial class DatabaseService : ObservableObject
{
    void UpdateDbConnectionStatus()
    {
        DbConnectionStatus = "Database connection status: " + System.Data.ConnectionState.Open;
    }
}
