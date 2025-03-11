

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
    )
    : ICommand<CreatePrpoductResult>;

public record CreatePrpoductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreatePrpoductResult>
{
    public async Task<CreatePrpoductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //business logic create product
        //save to database
        //return result
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            ImageFile = command.ImageFile
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreatePrpoductResult(product.Id);
    }
}


