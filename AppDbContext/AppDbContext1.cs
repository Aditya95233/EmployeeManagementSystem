using EmployeeManagementSystem.Entitys;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.AppDbContext
{
    public class AppDbContext1:DbContext
    {
        public AppDbContext1(DbContextOptions<AppDbContext1> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");
                entity.HasKey(e => e.Id);
                entity.HasOne(x => x.department)
                .WithMany(d => d.employees)
                .HasForeignKey(e => e.DepartmentId);
                entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
                entity.Property(e => e.Salary)
                .HasPrecision(18, 2)
                .IsRequired();
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(e => e.Email)
                .IsRequired();
                entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .IsRequired();
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");
                
                entity.HasKey(x => x.Id);
               
                entity.HasIndex(d => d.Name)
                .IsUnique();

                entity.Property(d => d.Name)
                .IsRequired();

            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(x => x.IsActive).HasDefaultValue(true);
                entity.HasOne(u => u.Employee).WithOne(e=>e.User).HasForeignKey<User>(e=>e.EmployeeId);
                entity.HasIndex(u => u.EmployeeId).IsUnique();
            });
        }
    }
}
