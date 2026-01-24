
public class ProductDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int? Discount { get; set; }
    public int Quantity { get; set; }
    public List<string> Images { get; set; }

}

