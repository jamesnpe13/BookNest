using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services
{
    partial class NavigateToPage_Message : ObservableObject
    {
        [ObservableProperty]
        private string targetPage;

        public NavigateToPage_Message(string targetPage)
        {
            TargetPage = targetPage;
        }
    }
}
