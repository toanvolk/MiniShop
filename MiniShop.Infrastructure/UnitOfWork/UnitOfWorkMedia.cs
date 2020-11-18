
using MiniShop.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MiniShop.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        //  var context = services.GetRequiredService<MiniShopContext>();
        //  public MiniShopContext dbContext =>  new MiniShopContext;
        private MiniShopContext _dbContext;
        private readonly IRepositoryBase _repositoryBase;
        public UnitOfWork(MiniShopContext dbContext, IRepositoryBase repositoryBase)
        {
            _dbContext = dbContext;
            _repositoryBase = repositoryBase;
        }

        #region method
        public int SaveChanges()
        {
            CheckIsDisposed();
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            CheckIsDisposed();
            return _dbContext.SaveChangesAsync();
        }

        private bool _disposed = false;
        private void CheckIsDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        [Obsolete]
        public int ExecQueryCommand(string sqlQuery, params object[] param) => _dbContext.Database.ExecuteSqlCommand(sqlQuery, param); //"procedureName @p0, @p1", parameters: new[] { "Bill", "Gates" }

        [Obsolete]
        public Task<int> ExecQueryCommandAsync(string sqlQuery, params object[] param) => _dbContext.Database.ExecuteSqlCommandAsync(sqlQuery, param); //"CreateTable @p0, @p1", parameters: new[]

        public string GetDatabaseName()=> _repositoryBase.GetDatabaseName();

        public System.Collections.Generic.IEnumerable<dynamic> GetDynamicResult(string commandText, params SqlParameter[] parameters)=> _repositoryBase.GetDynamicResult(commandText, parameters);

        #endregion end method

        #region register reponsitory
        private IRepositoryBase<Category> _categoryRepository;
        public IRepositoryBase<Category> CategoryRepository => _categoryRepository ?? (_categoryRepository = new RepositoryBase<Category>(_dbContext));
        public DbSet<Category> Categories => _dbContext.Categorys;

        private IRepositoryBase<Product> _productRepository;
        public IRepositoryBase<Product> ProductRepository => _productRepository ?? (_productRepository = new RepositoryBase<Product>(_dbContext));
        public DbSet<Product> Products => _dbContext.Products;

        private IRepositoryBase<Area> _areaRepository;
        public IRepositoryBase<Area> AreaRepository => _areaRepository ?? (_areaRepository = new RepositoryBase<Area>(_dbContext));
        public DbSet<Area> Areas => _dbContext.Areas;

        private IRepositoryBase<TouchHistory> _touchHistoryRepository;
        public IRepositoryBase<TouchHistory> TouchHistoryRepository => _touchHistoryRepository ?? (_touchHistoryRepository = new RepositoryBase<TouchHistory>(_dbContext));

        public DbSet<TouchHistory> TouchHistorys => _dbContext.TouchHistorys;
        public DbSet<ProductCategory> ProductCategories => _dbContext.ProductCategories;
        //Not table on database

        #endregion end register reponsitory
    }
  
}
