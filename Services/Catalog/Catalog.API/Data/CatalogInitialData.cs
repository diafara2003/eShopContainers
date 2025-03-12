using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
         using var session = store.LightweightSession();

        if(await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetPreconfiguredProducts());

        await session.SaveChangesAsync(cancellation);
    }

    public static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id=Guid.NewGuid(),
                Name = "Keyboard",
                Description = "Ergonomic keyboard",
                Price = 20,
                Category = new List<string> { "Electronics", "Computer" },
                ImageFile = "product-1.png"
            },
            new Product
            {
                Id=Guid.NewGuid(),
                Name = "Mouse",
                Description = "Wireless mouse",
                Price = 10,
                Category = new List<string> { "Electronics", "Computer" },
                ImageFile = "product-2.png"
            },
            new Product
            {
                Id=Guid.NewGuid(),
                Name = "Monitor",
                Description = "27-inch 4k monitor",
                Price = 200,
                Category = new List<string> { "Electronics", "Computer" },
                ImageFile = "product-3.png"
            },
            new Product
            {
                Id=Guid.NewGuid(),
                Name = "Desk",
                Description = "Wooden desk",
                Price = 100,
                Category = new List<string> { "Furniture", "Home" },
                ImageFile = "product-4.png"
            },
            new Product
            {
                Id=Guid.NewGuid(),
                Name = "Lamp",
                Description = "Desk lamp",
                Price = 5,
                Category = new List<string> { "Lighting", "Home" },
                ImageFile = "product-5.png"
            },
            new Product
            {
                Id=Guid.NewGuid(),
                Name = "Chair",
                Description = "Ergonomic chair",
                Price = 20,
                Category = new List<string> { "Furniture", "Home" },
                ImageFile = "product-6.png"
            }
        };
    }
}
