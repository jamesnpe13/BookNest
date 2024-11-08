using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookNest.ViewModels;

public partial class RegistrationPage_VM : ObservableObject
{
    private readonly PageNavigationService ns;
    public RegistrationPage_VM(PageNavigationService _ns)
    {
        ns = _ns;
    }

    public void SubmitForm()
    {
        ns.SetCurrentPage("MainPage");

    }
}
