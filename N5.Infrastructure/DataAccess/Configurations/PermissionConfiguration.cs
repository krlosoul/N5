namespace N5.Infrastructure.DataAccess.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using N5.Core.Entities;

    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", "dbo");

            builder.HasKey(e => e.Id).HasName("Pk_Permissions_Id");

            builder.Property(e => e.EmployeeForename)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.EmployeeSurname)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.PermissionDate).HasColumnType("date");

            builder.HasOne(d => d.PermissionTypeNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PermissionTypes_PermissionType");
        }
    }
}