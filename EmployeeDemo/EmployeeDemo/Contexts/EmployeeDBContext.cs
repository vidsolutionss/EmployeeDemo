using EmployeeDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDemo.Contexts
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
