using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;

public class BaseTests
{
    protected DbContextOptions<TaskManagerDbContext> DbOptions;
    public BaseTests()
    {
        DbOptions = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
}