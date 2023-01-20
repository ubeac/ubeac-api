namespace uBeac;

public interface IDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
