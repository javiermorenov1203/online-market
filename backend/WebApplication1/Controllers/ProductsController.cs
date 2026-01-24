using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : HomeController
{
    public ProductsController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        var productResponseList = await createProductResponseList(products);
        return Ok(productResponseList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        var productResponse = await createProductResponse(product);

        return Ok(new { message = "Product found", product = productResponse });
    }

    [HttpGet("most-viewed")]
    public async Task<IActionResult> GetMostViewedProducts()
    {
        var products = await _context.Products.OrderByDescending(p => p.Views).Take(12).ToListAsync();
        var productResponseList = await createProductResponseList(products);
        return Ok(productResponseList);
    }

    [HttpGet("discounts")]
    public async Task<IActionResult> GetProductsWithDiscounts()
    {
        var products = await _context.Products.Where(p => p.Discount != null && p.Discount != 0).Take(12).ToListAsync();
        var productResponseList = await createProductResponseList(products);
        return Ok(productResponseList);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostProduct(ProductDto productDto)
    {
        Product product = new Product(productDto);
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        product.PublisherId = int.Parse(userId);
        var discount = product.Discount ?? 0;
        product.FinalPrice = product.BasePrice - (product.BasePrice * (discount / 100m));
        product.Views = 0;
        product.UnitsSold = 0;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        foreach (var img in productDto.Images)
        {
            ProductImage productImg = new ProductImage { ProductId = product.Id, Image = img };
            _context.ProductImages.Add(productImg);
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = "Product added sucessfully", product = await createProductResponse(product) });
    }

    private async Task<ProductResponseDto> createProductResponse(Product product)
    {
        var productImages = await _context.ProductImages.Where(img => img.ProductId == product.Id).ToListAsync();
        return new ProductResponseDto(product, productImages);
    }

    private async Task<List<ProductResponseDto>> createProductResponseList(List<Product> products)
    {
        List<ProductResponseDto> productResponseList = new List<ProductResponseDto>();
        foreach (var p in products)
        {
            var productResponse = await createProductResponse(p);
            productResponseList.Add(productResponse);
        }
        return productResponseList;
    }
}
