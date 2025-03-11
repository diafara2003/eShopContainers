

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
    )
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductHandler
    (IDocumentSession session, ILogger<UpdateProductHandler> Logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Logger.LogInformation("UpdateProductHandler {@query}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if(product is null)
            throw new ProductNotFoundException();

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;

        session.Update(product);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }

  
}
