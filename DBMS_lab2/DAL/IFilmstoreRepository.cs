using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DBMS_lab2
{
    public interface IFilmstoreRepository : IDisposable
    {
        Task<DataTable> FindActorAndDirector();
        Task<DataTable> FindByGenreAssociationLevel(string genre, int level);
        Task<DataTable> FindLastFilms(int yearsCount);
        IReadOnlyList<string> GetTables();
    }
}