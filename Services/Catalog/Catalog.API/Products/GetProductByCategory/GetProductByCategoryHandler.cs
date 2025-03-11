
namespace Catalog.API.Products.GetProductByCategory;

public record class GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record class GetProductByCategoryResult(IEnumerable<Product> Products);
public class GetProductByCategoryHandler
    (IDocumentSession sesion, ILogger<GetProductByCategoryHandler> loger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle
        (GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        loger.LogInformation("GetProductByCategoryHandler {@query}", request);
        var products = await sesion.Query<Product>()
            .Where(p => p.Category.Contains(request.Category))
            .ToListAsync(cancellationToken);     

        return new GetProductByCategoryResult(products);
    }
}
