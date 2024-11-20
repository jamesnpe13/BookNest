using BookNest.Data;
using BookNest.Models;
using BookNest.ViewModels;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SharpVectors.Dom.Css;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.Modules;

public enum BookDynaButtonMode
{
    AddToBag,
    RemoveFromBag,
    ReserveBook,
    UnreserveBook,

}

public partial class BookSingleMember : UserControl
{
    private readonly MainPage_VM vm;
    private readonly DatabaseService ds;

    public BookDynaButtonMode ButtonMode { get; set; }

    public BookSingleMember()
    {
        InitializeComponent();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        ds = ((App)Application.Current).ServiceProvider.GetRequiredService<DatabaseService>();
        this.IsVisibleChanged += OnCurrentBookChanged;
        CreateComboboxItems();
        SetDynaButton();
    }

    private void CreateComboboxItems()
    {
        GenreDropdown.DropdownCombobox.Items.Add(new ComboBoxItem() { Content = "All" }); // add ALL BOOKS category

        var genreList = Enum.GetValues(typeof(BookGenre));

        foreach (var genre in genreList)
        {
            ComboBoxItem tempCBItem = new() { Content = genre.ToString() };
            GenreDropdown.DropdownCombobox.Items.Add(tempCBItem);
        }
    }

    private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => NavigateBack();

    private void backButtonUtility_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => NavigateBack();

    private void NavigateBack()
    {
        vm.IsEditing = false;
        vm.CurrentBook = null;
    }

    private void OnCurrentBookChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        SetDynaButton();
    }

    private void DynamicButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        switch (ButtonMode)
        {
            case BookDynaButtonMode.AddToBag:

                AddBookToBag();
                break;
            case BookDynaButtonMode.RemoveFromBag:
                RemoveFromBag();
                break;
            case BookDynaButtonMode.ReserveBook:
                ReserveBook();
                break;
            case BookDynaButtonMode.UnreserveBook:
                UnreserveBook();
                break;
        }

    }

    private void UnreserveBook()
    {
        throw new NotImplementedException();
    }

    private void ReserveBook()
    {
        throw new NotImplementedException();
    }

    private void RemoveFromBag()
    {
        vm.BookBag.Remove(vm.CurrentBook.BookId);
        SetDynaButton();
        UpdateNewBookList();
    }

    private void AddBookToBag()
    {
        vm.BookBag.Add(vm.CurrentBook.BookId);
        SetDynaButton();
        UpdateNewBookList();
    }

    private void UpdateNewBookList()
    {

        if (vm.CurrentView.GetType() == typeof(Member_Bag_V))
        {
            vm.TempBookList = new();
            foreach (var item in vm.BookBag)
            {
                vm.TempBookList.Add(ds.GetBook(BookFilterKey.ID, item.ToString())[0]);
            }
            vm.RefreshBookList();
        }
    }

    private void SetDynaButton()
    {
        if (vm.CurrentBook != null)
        {
            if (vm.CurrentBook.Status == BookStatus.Available)
            {
                bool foundMatch = false;

                foreach (var book in vm.BookBag)
                {

                    if (book == vm.CurrentBook.BookId)
                    {
                        foundMatch = true;
                        break;
                    }
                }

                if (foundMatch) ButtonMode = BookDynaButtonMode.RemoveFromBag;
                else if (!foundMatch) ButtonMode = BookDynaButtonMode.AddToBag;
            }
            else if (vm.CurrentBook.Status == BookStatus.Unavailable)
            {
                ButtonMode = BookDynaButtonMode.ReserveBook;

            }
        }

        switch (ButtonMode)
        {
            case BookDynaButtonMode.AddToBag:
                DynamicButton.ButtonText = "Add to bag";
                DynamicButton.ButtonStyle = "Primary";

                break;
            case BookDynaButtonMode.RemoveFromBag:
                DynamicButton.ButtonText = "Remove from bag";
                DynamicButton.ButtonStyle = "Destructive";

                break;
            case BookDynaButtonMode.ReserveBook:
                DynamicButton.ButtonText = "Reserve";
                DynamicButton.ButtonStyle = "Primary";

                break;
            case BookDynaButtonMode.UnreserveBook:
                DynamicButton.ButtonText = "Unreserve";
                DynamicButton.ButtonStyle = "Destructive";
                break;
        }
    }

}
