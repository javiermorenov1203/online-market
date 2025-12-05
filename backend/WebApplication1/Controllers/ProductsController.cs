using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;
using WebApplication1.Data;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : HomeController
{
    public ProductsController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetProductos()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }
}
