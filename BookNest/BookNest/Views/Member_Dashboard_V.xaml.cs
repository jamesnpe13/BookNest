using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Views
{
    public partial class Member_Dashboard_V : UserControl
    {

        public Member_Dashboard_V()
        {
            InitializeComponent();
        }

        private void SetCurrentView(PageView targetView)
        {
            if (DataContext is MainPage_VM vm) vm.SetCurrentView(targetView);
        }

        private void DashboardSearchField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchBooks();
            }
        }

        private void DashboardSearchButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SearchBooks();
        }

        public void SearchBooks()
        {
            if (!string.IsNullOrEmpty(DashboardSearchField.Text))
            {
                if (DataContext is MainPage_VM vm)
                {
                    vm.UpdateBookList(BookFilterKey.SEARCH, DashboardSearchField.Text);
                    DashboardSearchField.Text = string.Empty;
                    vm.SetCurrentView(PageView.Books);
                }
            }
        }
    }
}
