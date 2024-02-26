namespace N5.Infrastructure.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using N5.Business.Interfaces.DataAccess;
    using N5.Core.Entities;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private DbContext DbContext { get; set; }
        private IDbContextTransaction? _transaction;
        private IRepository<PermissionType>? _permissionTypeRepository;
        private IRepository<Permission>? _permissionRepository;
        #endregion

        public UnitOfWork(N5Context dbContext)
        {
            DbContext = dbContext;
        }

        #region Transactions
        public async Task BeginTransactionAsync()
        {
            _transaction ??= await DbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task CloseTransactionAsync()
        {
            if (_transaction != null) await _transaction.DisposeAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null) await _transaction.RollbackAsync();
        }
        #endregion

        #region Repositories
        public IRepository<PermissionType> PermissionTypeRepository
        {
            get
            {
                return _permissionTypeRepository ??= new Repository<PermissionType>(DbContext);
            }
        }

        public IRepository<Permission> PermissionRepository
        {
            get
            {
                return _permissionRepository ??= new Repository<Permission>(DbContext);
            }
        }
        #endregion
    }
}