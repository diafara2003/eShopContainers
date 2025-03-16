﻿namespace Basket.API.Models;

public class ShoppingCarItem
{
    public int Quantity { get; set; }= 0;
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = string.Empty;
}
