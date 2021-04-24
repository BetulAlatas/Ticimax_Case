using Core.Helper;
using Dapper;
using Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper.Extensions.Linq.Builder;
using Core.Poco;

namespace Core.Repository
{
    public class CategoryRepository : IRepository<CategoryPoco>
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public CategoryRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public int AddOrUpdate(CategoryPoco obj)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = @"UPDATE Categories SET ParentId=@ParentId,Name=@Name,SortOrder=@SortOrder,Status=1 Where Id= @Id";
                return connection.Execute(query, obj);
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = "UPDATE Categories SET Status=0 Where Id = @Id";
                return connection.Execute(query, new { Id = id });
            }
        }

        public CategoryPoco Get(Expression<Func<CategoryPoco, bool>> expression)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<CategoryPoco>("Select * from Categories").AsQueryable().FirstOrDefault(expression);
            }
        }

        public IEnumerable<CategoryPoco> GetAllValues()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                IEnumerable<CategoryPoco> categories = connection.Query<CategoryPoco>(
                    "SELECT c.Id, c.ParentId,c.Name, c2.Name as ParentName,c.SortOrder,c.Status " +
                    "FROM betulalatas.Categories as c " +
                    "CROSS JOIN betulalatas.Categories as c2 " +
                    "WHERE c.ParentId = c2.Id " +
                    "UNION " +
                    "SELECT c.Id, c.ParentId,c.Name, '' as ParentName,c.SortOrder,c.Status " +
                    "FROM betulalatas.Categories as c where c.ParentId IS NULL ").AsEnumerable();
                return categories;
            }
        }

        public CategoryPoco GetById(int id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<CategoryPoco>("Select * from Categories Where Id =" + id).FirstOrDefault();
            }
        }

        public IEnumerable<CategoryPoco> GetMany(Expression<Func<CategoryPoco, bool>> expression)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<CategoryPoco>("Select * from Categories").AsQueryable().Where(expression);
            }
        }

        public int Insert(CategoryPoco obj)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = @"INSERT INTO Categories (ParentId,Name,SortOrder,Status) VALUES (@ParentId,@Name,@SortOrder,@Status)";
                return connection.Execute(query, obj);
            }
        }
    }
}
