
namespace Ordering.Application.Orders.Queries.GetOrderByName
{
   public record GetOrderByNameQuery(string Name):
        IQuery<GetOrderByNameResult>;


    public record GetOrderByNameResult(IEnumerable<OrderDTO> orders);

    public class GetOrderByNameQueryValidator : AbstractValidator<GetOrderByNameQuery>
    {
        public GetOrderByNameQueryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
