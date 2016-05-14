using System.Configuration;
using System.Windows;

namespace DBMS_lab2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["MSSQLDb"].ConnectionString;

        public ITableService TableService { get; } =  new MSSQLTableService(new MSSQLFilmstoreRepository(connString));

        public static new App Current => Application.Current as App;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            TableService.Dispose();
        }
    }
}
