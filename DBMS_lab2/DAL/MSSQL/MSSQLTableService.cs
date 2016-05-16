using System.Data;
using System.Data.SqlClient;

namespace DBMS_lab2.DAL.MSSQL
{
    public class MSSQLTableService : ITableService
    {
        private MSSQLFilmstoreRepository repo;

        public IFilmstoreRepository Repo => repo;

        private SqlDataAdapter currentAdapter;

        public MSSQLTableService(string connectionString)
        {
            repo = new MSSQLFilmstoreRepository(connectionString);
        }

        public DataTable GetDataTable(string tableName)
        {
            currentAdapter = repo.GetDataAdapter(tableName);
            var dataTable = new DataTable();
            currentAdapter.Fill(dataTable);
            new SqlCommandBuilder(currentAdapter);

            return dataTable;
        }

        public void Update(DataTable dataTable)
        {
            currentAdapter.Update(dataTable);
        }

        public void Dispose()
        {
            currentAdapter.Dispose();
            repo.Dispose();
        }
    }
}
