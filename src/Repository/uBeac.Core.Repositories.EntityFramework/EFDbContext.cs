using Microsoft.EntityFrameworkCore;

namespace uBeac.Repositories.EntityFramework;

public class EFDbContext : DbContext
{
    public EFDbContext(DbContextOptions options) : base(options)
    {        
    }
}