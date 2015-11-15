using System.Windows;

namespace DBMS_lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilmstoreRepository repo = App.Current.Repository;

        public MainWindow()
        {
            InitializeComponent();
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
    }
}
