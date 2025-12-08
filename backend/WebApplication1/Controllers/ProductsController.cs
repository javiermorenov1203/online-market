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
    public async Task<IActionResult> GetProducts(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });
        
        return Ok(new { message = "Product found", product });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostProduct(Product product)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        product.PublisherId = int.Parse(userId);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Product added sucessfully", product });
    }

}
