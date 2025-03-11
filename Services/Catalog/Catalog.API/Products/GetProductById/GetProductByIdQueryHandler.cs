


using BuildingBlocks.Exceptions;

namespace Catalog.API.Products.GetProductById;


public record GetProductByIdQuery(Guid Id) : IQuery<GetPtoductResult>;
public record GetPtoductResult(Product Troduct);
internal class GetProductByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetPtoductResult>
{
    public async Task<GetPtoductResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException("Product", request.Id);


        return new GetPtoductResult(product);
    }
}
