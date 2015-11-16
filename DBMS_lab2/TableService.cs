using System.Data;
using System.Data.SqlClient;

namespace DBMS_lab2
{
    public class TableService
    {
        private FilmstoreRepository repo;

        private SqlDataAdapter currentAdapter;

        public TableService(FilmstoreRepository _repo)
        {
            repo = _repo;
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
    }
}
