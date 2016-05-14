using System;
using System.Data;

namespace DBMS_lab2
{
    public interface ITableService : IDisposable
    {
        IFilmstoreRepository Repo { get; }
        DataTable GetDataTable(string tableName);
        void Update(DataTable dataTable);
    }
}