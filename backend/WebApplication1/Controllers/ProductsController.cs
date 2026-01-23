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
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });
        
        return Ok(new { message = "Product found", product });
    }

    [HttpGet("most-viewed")]
    public async Task<IActionResult> GetMostViewedProducts()
    {
        var products = await _context.Products.OrderByDescending(p => p.Views).Take(12).ToListAsync();
        return Ok(products);
    }

    [HttpGet("discounts")]
    public async Task<IActionResult> GetProductsWithDiscounts()
    {
        var products = await _context.Products.Where(p => p.Discount != null && p.Discount != 0).Take(12).ToListAsync();
        return Ok(products);
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
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Product added sucessfully", product });
    }

}
