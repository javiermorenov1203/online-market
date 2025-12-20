
public class Product
{
    public Product() { }

    public Product(ProductDto dto)
    {
        Name = dto.Name;
        Description = dto.Description;
        Price = dto.Price;
        Quantity = dto.Quantity;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int PublisherId { get; set; }

}
