using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace DBMS_lab2
{
    public class FilmstoreRepository: IDisposable
    {
        private readonly IDbConnection connection;

        public FilmstoreRepository(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public Task<DataTable> FindByGenreAssociationLevel(string genre, int level)
        {
            return GetDataTable(connection.ExecuteReaderAsync(Resource1.FilmsByGenreQuery, new { genre, level }));
        }

        public Task<DataTable> FindLastFilms(int yearsCount)
        {
            return GetDataTable(connection.ExecuteReaderAsync(Resource1.LastYearsFilmsQuery, new { years = yearsCount }));
        }

        public Task<DataTable> FindActorAndDirector()
        {
            return GetDataTable(connection.ExecuteReaderAsync(Resource1.ActorAndDirectorQuery));
        }

        public void Dispose()
        {
            connection.Close();
            connection.Dispose();
        }

        private Task<DataTable> GetDataTable(Task<IDataReader> dataReaderTask)
        {
            return dataReaderTask.ContinueWith(task =>
            {
                var reader = task.Result;
                var ret = new DataTable();
                ret.Load(task.Result);
                reader.Close();
                reader.Dispose();
                return ret;
            });
        }
    }
}
