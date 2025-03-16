


using BuildingBlocks.Exceptions;

namespace Catalog.API.Products.GetProductById;


public record GetProductByIdQuery(Guid Id) : IQuery<GetProductResult>;
public record GetProductResult(Product Troduct);
internal class GetProductByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException("Product", request.Id);


        return new GetProductResult(product);
    }
}
