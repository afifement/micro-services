
namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync()) return;

        session.Store(GetPreconfigureProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfigureProducts() => new List<Product>
    {
        new()
        {
            Id= Guid.NewGuid(),
            Name="Initial test name 1",
            Description = "Initial description 1",
            ImageFile = "Image File Init 1",
            Price= 1,
            Category = new List<string>{"1"}
        },
         new()
        {
            Id= Guid.NewGuid(),
            Name="Initial test name 2",
            Description = "Initial description 2",
            ImageFile = "Image File Init 2",
            Price= 1,
            Category = new List<string>{"2"}
        }
    };
}
