using CommunityToolkit.Mvvm.ComponentModel;

namespace BookNest.Services;

partial class NavigateToPage_Message : ObservableObject
{
    [ObservableProperty] private string targetPage;

    public NavigateToPage_Message(string targetPage) => TargetPage = targetPage;
}
