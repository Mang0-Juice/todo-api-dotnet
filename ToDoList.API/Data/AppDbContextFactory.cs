using Microsoft.EntityFrameworkCore.Design;

namespace ToDoList.API.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{

    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=todo.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}