using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;

namespace DBMS_lab2
{
    public class FilmstoreRepository: IDisposable
    {
        private readonly SqlConnection connection;

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

        public IReadOnlyList<string> GetTables()
        {
            List<string> tables = new List<string>();
            DataTable dt = connection.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
            return tables;
        }

        public SqlDataAdapter GetDataAdapter(string tableName)
        {
            var adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
            return adapter;
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
