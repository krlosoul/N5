namespace N5.Business.Interfaces.DataAccess
{
    using N5.Core.Entities;

    public interface IUnitOfWork
    {
        #region Transactions
        /// <summary>
        /// starts a new transaction asynchronous.
        /// </summary>
        public Task BeginTransactionAsync();

        /// <summary>
        /// Commits all changes made to the database in the current transaction asynchronously.
        /// </summary>
        public Task CommitTransactionAsync();

        /// <summary>
        /// releasing, or resetting unmanaged resources asynchronously.
        /// </summary>
        public Task CloseTransactionAsync();

        /// <summary>
        /// Discards all changes made to the database in the current transaction asynchronously.
        /// </summary>
        public Task RollbackTransactionAsync();
        #endregion

        #region Repositories
        /// <summary>
        /// PermissionType repository
        /// </summary>
        public IRepository<PermissionType> PermissionTypeRepository { get; }

        /// <summary>
        /// Permission repository
        /// </summary>
        public IRepository<Permission> PermissionRepository { get; }
        #endregion
    }
}

