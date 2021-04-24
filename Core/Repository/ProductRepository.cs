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
    public class ProductRepository : IRepository<ProductPoco>
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public ProductRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public int AddOrUpdate(ProductPoco obj)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = @"UPDATE Product SET Name=@Name,Description=@Description,Price=@Price,DiscountPrice=@DiscountPrice,Quantitiy=@Quantitiy,BrandId=@BrandId,Status=1 Where Id= @Id";
                return connection.Execute(query, obj);
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var query = "UPDATE Product SET Status=0 Where Id = @Id";
                return connection.Execute(query, new { Id = id });
            }
        }

        public ProductPoco Get(Expression<Func<ProductPoco, bool>> expression)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<ProductPoco>("Select * from Product").AsQueryable().FirstOrDefault(expression);
            }
        }

        public IEnumerable<ProductPoco> GetAllValues()
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                IEnumerable<ProductPoco> products = connection.Query<ProductPoco>(
                    "SELECT p.Id, p.BrandId,b.Name as BrandName,p.Name,p.Description,p.Price,p.DiscountPrice,p.Quantitiy,p.Status " +
                    "FROM Product as p " +
                    "Left Join Brands as b on b.Id= p.BrandId"
                   ).AsEnumerable();
                return products;
            }
        }

        public ProductPoco GetById(int id)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<ProductPoco>("Select * from Product Where Id =" + id).FirstOrDefault();
            }
        }

        public IEnumerable<ProductPoco> GetMany(Expression<Func<ProductPoco, bool>> expression)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                return connection.Query<ProductPoco>("Select * from Product").AsQueryable().Where(expression);
            }
        }

        public int Insert(ProductPoco obj)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                var id = connection.QuerySingle<int>(@"INSERT INTO Product (Name,Description,Price,DiscountPrice,Quantitiy,Status,BrandId) 
                                                     VALUES (@Name,@Description,@Price,@DiscountPrice,@Quantitiy,@Status,@BrandId) ;
                                                     SELECT LAST_INSERT_ID();"
                         , obj);
                return id;
            }
        }
    }
}
