using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chamada.Database;
using Chamada.iOS.Database;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace Chamada.iOS.Database
{
    class SQLiteDb : ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}