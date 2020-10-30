﻿
using MiniShop.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniShop.Infrastructure
{
    public interface IUnitOfWork
    {
        //MediaContext dbContext { get; }
        int SaveChanges();

        Task<int> SaveChangesAsync();

        int ExecQueryCommand(string sqlQuery, params object[] param);

        Task<int> ExecQueryCommandAsync(string sqlQuery, params object[] param);

        #region Implement Repository
        IRepositoryBase<Category> CategoryRepository { get; }
        DbSet<Category> Categories { get; }
        IRepositoryBase<Product> ProductRepository { get; }
        DbSet<Product> Products { get; }

        #endregion

        #region Implement reponsitory not entity
        string GetDatabaseName();
        IEnumerable<dynamic> GetDynamicResult(string commandText, params SqlParameter[] parameters);
        #endregion
    }
}
