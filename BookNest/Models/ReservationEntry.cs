using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Models;

public partial class ReservationEntry : ObservableObject
{
    [ObservableProperty] int accountId;
    [ObservableProperty] int bookId;
    [ObservableProperty] bool isReserved;

}
