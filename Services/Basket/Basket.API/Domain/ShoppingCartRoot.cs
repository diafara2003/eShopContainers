namespace Basket.API.Models;

public class ShoppingCartRoot
{
    public string UserName { get; set; }= string.Empty;
    public List<ShoppingCarItem> Items { get; set; }
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCartRoot(string userName)
    {
        this.UserName= userName;
    }

    public ShoppingCartRoot()
    {
        
    }
}
