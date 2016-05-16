using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DBMS_lab2.DAL.Oracle
{
    public class OracleTableService : ITableService
    {
        private OracleFilmstoreRepository repo;

        public IFilmstoreRepository Repo => repo;

        private OracleDataAdapter currentAdapter;

        public OracleTableService(string connectionString)
        {
            repo = new OracleFilmstoreRepository(connectionString);
        }

        public DataTable GetDataTable(string tableName)
        {
            currentAdapter = repo.GetDataAdapter(tableName);
            var dataTable = new DataTable();
            currentAdapter.Fill(dataTable);
            new OracleCommandBuilder(currentAdapter);

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
