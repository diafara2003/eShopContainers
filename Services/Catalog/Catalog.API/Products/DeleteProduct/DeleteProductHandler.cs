namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");

        }
    }

    public class DeleteProductHandler
        (IDocumentSession sesion)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
        
            var product = await sesion.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
                throw new Exception();

            sesion.Delete(product);

            await sesion.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }

}
