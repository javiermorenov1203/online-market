
public class ProductResponseDto
{
    public ProductResponseDto(Product p, List<ProductImage> productImages)
    {
        Id = p.Id;
        Name = p.Name;
        Description = p.Description;
        BasePrice = p.BasePrice;
        Discount = p.Discount;
        FinalPrice = p.FinalPrice;
        Quantity = p.Quantity;
        PublisherId = p.PublisherId;
        Views = p.Views;
        UnitsSold = p.UnitsSold;
        CategoryId = p.CategoryId;
        Images = productImages.Select(p => p.Image).ToList();
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
    public int CategoryId { get; set; }
    public List<string> Images { get; set; }

}
