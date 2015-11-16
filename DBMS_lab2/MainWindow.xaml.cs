using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DBMS_lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilmstoreRepository repo = App.Current.Repository;

        private TableService tableService;

        public MainWindow()
        {
            InitializeComponent();
            tableService = new TableService(repo);
            metaTables.ItemsSource = repo.GetTables();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            var genre = genreTextBox.Text;
            var level = levelUpDown.Value ?? 0;
            var data = await repo.FindByGenreAssociationLevel(genre, level);
            dataGrid.ItemsSource = data.DefaultView;
            tabControl.SelectedIndex = 1;
        }

        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            var yearsCount = yearsUpDown.Value ?? 0;
            var data = await repo.FindLastFilms(yearsCount);
            dataGrid.ItemsSource = data.DefaultView;
            tabControl.SelectedIndex = 1;
        }

        private async void button3_Click(object sender, RoutedEventArgs e)
        {
            var data = await repo.FindActorAndDirector();
            dataGrid.ItemsSource = data.DefaultView;
            tabControl.SelectedIndex = 1;
        }

        private void metaTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;
            var tableName = (string)listBox.SelectedItem;
            tableDataGrid.DataContext = tableService.GetDataTable(tableName);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            tableService.Update((DataTable)tableDataGrid.DataContext);
        }
    }
}
