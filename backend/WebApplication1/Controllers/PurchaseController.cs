using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : HomeController
{
    private readonly ProductService _productService;
    public PurchaseController(AppDbContext context, ProductService productService) : base(context)
    {
        _productService = productService;
    }

    [HttpGet("cart-items")]
    public async Task<IActionResult> GetCartItems([FromQuery] int userId)
    {
        var cartItems = await _context.CartItems.Where(i => i.UserId == userId).ToListAsync();
        return Ok(cartItems);
    }

    [Authorize]
    [HttpPost("add-to-cart")]
    public async Task<IActionResult> AddCartItem([FromQuery] int productId, [FromQuery] int quantity)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var product = await _productService.GetByIdAsync(productId);
        var productQuantiy = product?.Quantity;

        if (quantity > productQuantiy)
        {
            return UnprocessableEntity(new { message = "Requested quantity is not in stock" });
        }

        CartItem item = new CartItem() { ProductId = productId, UserId = userId, Quantity = quantity };
        _context.CartItems.Add(item);
        _context.SaveChanges();

        return Ok(new {message = "Item added successfully", item});
    }

}

