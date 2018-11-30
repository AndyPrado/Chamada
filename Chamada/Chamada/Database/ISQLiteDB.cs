using SQLite;

namespace Chamada.Database
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}