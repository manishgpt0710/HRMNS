using HRMS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Data;

public class HRMSDbContext : DbContext
{
    public HRMSDbContext(DbContextOptions<HRMSDbContext> options)
    : base(options)
    {

    }

    public virtual DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}