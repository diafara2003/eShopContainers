namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);


    public class DeleteProductHandler
        (IDocumentSession sesion, ILogger<DeleteProductHandler> Logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            Logger.LogInformation("DeleteProductHandler {@query}", command);

            var product = await sesion.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException();

            sesion.Delete(product);

            await sesion.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }

}
