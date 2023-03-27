using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace SimpleWebApi
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Todo>().HasData(new Todo() { Id = 1, Name = "Test data", IsComplete = true});
        }

        public DbSet<Todo> Todos => Set<Todo>();

    }
}
