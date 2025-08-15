using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoCal.Db;
using Microsoft.EntityFrameworkCore;
public class LocalSqlite:DbContext
{
    public DbSet<TodoList>  TodoLists { get; set; }
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=todocal.db");
    }
}

public class TodoList
{
    public ulong Id { get; set; }
    public string Name { get; set; } = "";
    public List<Todo> Tasks { get; set; } = new();
    public bool IsDeleted { get; set; }
}

public class Todo
{
    public ulong Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsComplete { get; set; }
    public bool IsDeleted { get; set; }
    public string Description { get; set; } = "";
    public bool Starred { get; set; }
    public Todo? Parent { get; set; }
}