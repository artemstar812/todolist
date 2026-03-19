using Microsoft.EntityFrameworkCore;
using ToDo_List.Models;

namespace ToDo_List.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
