
public class Product
{
    public Product() { }

    public Product(ProductDto dto)
    {
        Name = dto.Name;
        Description = dto.Description;
        Price = dto.Price;
        Quantity = dto.Quantity;
        Image1 = dto.Image1;
        Image2 = dto.Image2;
        Image3 = dto.Image3;
        Image4 = dto.Image4;
        Image5 = dto.Image5;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int PublisherId { get; set; }
    public string Image1 { get; set; }
    public string? Image2 { get; set; }
    public string? Image3 { get; set; }
    public string? Image4 { get; set; }
    public string? Image5 { get; set; }

}
