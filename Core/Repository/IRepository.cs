using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    internal interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all values from entity
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllValues();

        /// <summary>
        /// Get values from expression
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get entity from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Get entity from expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> expression);

        /// <summary> 
        /// Insert new item
        /// </summary>
        /// <param name="obj"></param>
        int Insert(T obj);

        /// <summary>
        /// If object exist update entity else insert new entity
        /// </summary>
        /// <param name="obj"></param>
        int AddOrUpdate(T obj);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id"></param>
        int Delete(int id);
    }
}
