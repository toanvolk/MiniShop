using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MiniShop.Infrastructure
{
    public interface IRepositoryBase
    {
        string GetDatabaseName();
        IEnumerable<dynamic> GetDynamicResult(string commandText, params SqlParameter[] parameters);
    }
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        int Count { get; }

        bool Any(object primaryKey);

        TEntity FindById(Guid id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate, string condition = "");

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> condition);
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> condition, string navigationPropertyPath);
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> navigationPropertyPath);
        IQueryable<TEntity> GetAllData(Expression<Func<TEntity, bool>> condition, int currentPage, int pageSize, Expression<Func<TEntity, string>> orderby);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity toUpdate);
        /// <summary>
        /// Điều hướng xử lý với các property cụ thể.
        /// </summary>
        /// <param name="toUpdate">Entity</param>
        /// <param name="accessMode">Chế độ cho phép (hoặc không) update property.</param>
        /// <param name="props">PropertyName</param>
        void Update(TEntity toUpdate, AccessPropertyMode accessMode, params string[] propertyNames);
        void Delete(Guid id);

        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> condition);
        void DeleteRange(IEnumerable<TEntity> entities);

        bool HasRows(Expression<Func<TEntity, bool>> condition);
         
        IQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> condition);

        IQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> condition);
    }    
   
    public interface IRepositoryBase<TEntity1, TEntity2> where TEntity1 : class where TEntity2 : class
    {
        void Add(TEntity1 toAdd, TEntity2 ignoreAdd);
    }
}
