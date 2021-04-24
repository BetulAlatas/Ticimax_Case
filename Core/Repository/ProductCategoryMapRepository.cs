using Core.Helper;
using Core.Poco;
using Dapper;
using Data.Model;
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
    public class ProductCategoryMapRepository : IRepository<ProductCategoryMapPoco>
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public ProductCategoryMapRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public IEnumerable<string> GetByProductId(int productId)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<string>(
                      "SELECT c.Name as CategoryName " +
                      "FROM betulalatas.ProductCategoryMap as p " +
                      "Left Join betulalatas.Categories as c on c.Id= p.CategoryId " +
                      "Where p.ProductId= " + productId).ToList();
            }
        }

        public int AddOrUpdate(ProductCategoryMapPoco obj)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = @"DELETE FROM ProductCategoryMap where Id=" + id;
                return connection.Execute(query);
            }
        }

        public ProductCategoryMapPoco Get(Expression<Func<ProductCategoryMapPoco, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductCategoryMapPoco> GetAllValues()
        {
            throw new NotImplementedException();
        }

        public ProductCategoryMapPoco GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(ProductCategoryMapPoco obj)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = @"INSERT INTO ProductCategoryMap (CategoryId,ProductId) VALUES (@CategoryId,@ProductId)";
                return connection.Execute(query, obj);
            }
        }

        public IEnumerable<ProductCategoryMapPoco> GetMany(Expression<Func<ProductCategoryMapPoco, bool>> expression)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<ProductCategoryMapPoco>("Select * from ProductCategoryMap").AsQueryable().Where(expression);
            }
        }
    }
}
