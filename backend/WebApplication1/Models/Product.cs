
public class Product
{
    public Product() { }

    public Product(ProductDto dto)
    {
        Name = dto.Name;
        Description = dto.Description;
        BasePrice = dto.BasePrice;
        Discount = dto.Discount;
        Quantity = dto.Quantity;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int? Discount { get; set; }
    public decimal FinalPrice { get; set; }
    public int Quantity { get; set; }
    public int PublisherId { get; set; }
    public int Views { get; set; }
    public int UnitsSold { get; set; }

}
