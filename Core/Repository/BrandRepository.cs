using Core.Helper;
using Core.Poco;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class BrandRepository : IRepository<BrandPoco>
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public BrandRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public int AddOrUpdate(BrandPoco obj)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = @"UPDATE Brands SET Name=@Name,Status=1 Where Id= @Id";
                return connection.Execute(query, obj);
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = "UPDATE Brands SET Status=0 Where Id = @Id";
                return connection.Execute(query, new { Id = id });
            }
        }

        public BrandPoco Get(Expression<Func<BrandPoco, bool>> expression)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<BrandPoco>("Select * from Brands").AsQueryable().FirstOrDefault(expression);
            }
        }

        public IEnumerable<BrandPoco> GetAllValues()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                IEnumerable<BrandPoco> brands = connection.Query<BrandPoco>(
                    "SELECT * FROM Brands").AsEnumerable();
                return brands;
            }
        }

        public BrandPoco GetById(int id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<BrandPoco>("Select * from Brands Where Id =" + id).FirstOrDefault();
            }
        }

        public IEnumerable<BrandPoco> GetMany(Expression<Func<BrandPoco, bool>> expression)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<BrandPoco>("Select * from Brands").AsQueryable().Where(expression);
            }
        }

        public int Insert(BrandPoco obj)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = @"INSERT INTO Brands (Name,Status) VALUES (@Name,@Status)";
                return connection.Execute(query, obj);
            }
        }
    }
}
