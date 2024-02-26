namespace N5.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using N5.Core.Entities;
    using N5.Infrastructure.Common.Constants;
    using N5.Infrastructure.Common.Dtos;
    using N5.Infrastructure.Extensions;

    public partial class N5Context : DbContext
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private DataBaseDto? _dataBaseDto;
        #endregion

        public N5Context(DbContextOptions<N5Context> options, IConfiguration configuration) : base(options) => _configuration = configuration;

        #region Entities
        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            DataBaseDto instance = _dataBaseDto = new DataBaseDto();
            _configuration.Bind(DataBaseConstant.DataBase, instance);
            var connectionString = _dataBaseDto.ConnectionString;
            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
            modelBuilder.ApplyAllConfigurations();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}