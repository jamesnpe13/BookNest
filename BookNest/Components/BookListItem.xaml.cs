using BookNest.Pages;
using BookNest.Services;
using BookNest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookNest.Components;

public partial class BookListItem : UserControl
{
    private readonly MainPage_VM vm;

    public BookListItem()
    {
        InitializeComponent();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
    }

    public int BookID
    {
        get { return (int)GetValue(BookIDProperty); }
        set { SetValue(BookIDProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BookID.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BookIDProperty =
        DependencyProperty.Register("BookID", typeof(int), typeof(BookListItem), new PropertyMetadata(null));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    public string ISBN
    {
        get { return (string)GetValue(ISBNProperty); }
        set { SetValue(ISBNProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ISBN.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ISBNProperty =
        DependencyProperty.Register("ISBN", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    public string Status
    {
        get { return (string)GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register("Status", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    public string Author
    {
        get { return (string)GetValue(AuthorProperty); }
        set { SetValue(AuthorProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Author.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AuthorProperty =
        DependencyProperty.Register("Author", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    public string Genre
    {
        get { return (string)GetValue(GenreProperty); }
        set { SetValue(GenreProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Genre.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GenreProperty =
        DependencyProperty.Register("Genre", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    public string YearOfPublication
    {
        get { return (string)GetValue(YearOfPublicationProperty); }
        set { SetValue(YearOfPublicationProperty, value); }
    }

    // Using a DependencyProperty as the backing store for YearOfPublication.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty YearOfPublicationProperty =
        DependencyProperty.Register("YearOfPublication", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    public string Publisher
    {
        get { return (string)GetValue(PublisherProperty); }
        set { SetValue(PublisherProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Publisher.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PublisherProperty =
        DependencyProperty.Register("Publisher", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    public string Likes
    {
        get { return (string)GetValue(LikesProperty); }
        set { SetValue(LikesProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Likes.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LikesProperty =
        DependencyProperty.Register("Likes", typeof(string), typeof(BookListItem), new PropertyMetadata(string.Empty));

    private void Item_MouseDown(object sender, MouseButtonEventArgs e)
    {
        vm.SetCurrentBook(BookID);
    }
}
