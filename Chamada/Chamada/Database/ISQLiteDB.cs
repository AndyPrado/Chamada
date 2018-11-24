using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Chamada.Database
{
   public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
