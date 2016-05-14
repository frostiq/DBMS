using Oracle.ManagedDataAccess.Client;

namespace DBMS_lab2.DAL.Oracle
{
    public class OracleFilmstoreRepository : BaseFilmstoreRepository
    {
        private OracleConnection connection;

        public OracleFilmstoreRepository(string connectionString) : this(new OracleConnection(connectionString))
        {
        }

        private OracleFilmstoreRepository(OracleConnection connection) : base(connection)
        {
            this.connection = connection;
        }

        internal OracleDataAdapter GetDataAdapter(string tableName)
        {
            var adapter = new OracleDataAdapter($"SELECT * FROM {tableName}", connection);
            return adapter;
        }

    }
}
