


namespace Catalog.API.Products.GetProductById; 


public record GetProductByIdQuery(Guid Id) : IQuery<GetPtoductResult>;
public record GetPtoductResult(Product Troduct);
internal class GetProductByIdQueryHandler
    (IDocumentSession session, ILogger<GetProductByIdQueryHandler> Logger)
    : IQueryHandler<GetProductByIdQuery, GetPtoductResult>
{
    public async Task<GetPtoductResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("GetProductByIdQueryHandler {@query}", request);
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if(product is null)        
            throw new ProductNotFoundException();
        

        return new GetPtoductResult(product);
    }
}
