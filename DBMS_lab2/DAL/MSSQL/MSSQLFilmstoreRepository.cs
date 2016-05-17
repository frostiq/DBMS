using System.Data.SqlClient;

namespace DBMS_lab2.DAL.MSSQL
{
    public class MSSQLFilmstoreRepository : BaseFilmstoreRepository
    {
        private SqlConnection connection;

        public MSSQLFilmstoreRepository(string connectionString) : this(new SqlConnection(connectionString))
        {
        }

        private MSSQLFilmstoreRepository(SqlConnection connection) : base(connection)
        {
            this.connection = connection;
        }

        internal SqlDataAdapter GetDataAdapter(string tableName)
        {
            var adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
            return adapter;
        }
    }
}
