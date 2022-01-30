using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Database;

public class AppDbContext:IdentityDbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<ToDoTask> ToDoTasks { get; set; }
}