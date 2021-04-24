using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class InMemoryDatabase
    {
        private readonly OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(":memory:", SqliteOrmLiteDialectProvider.Instance);

        public IDbConnection OpenConnection() => this.dbFactory.OpenDbConnection();

        public void Insert<T>(IEnumerable<T> items)
        {
            using (var db = this.OpenConnection())
            {
                db.CreateTableIfNotExists<T>();
                foreach (var item in items)
                {
                    db.Insert(item);
                }
            }
        }

        //public void Insert<T>(T item)
        //{
        //    using (var db = this.OpenConnection())
        //    {
        //        db.CreateTableIfNotExists<T>();
        //        db.Insert(item);

        //        if (db.TableExists("Product"))
        //        {
        //            var c = 1;
        //        }
        //    }
        //}
    }
}
