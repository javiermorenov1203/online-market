using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly PurchaseService _purchaseService;

    public PurchaseController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [Authorize]
    [HttpGet("cart-items")]
    public async Task<IActionResult> GetCartItems()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var products = await _purchaseService.GetCartItemsAsync(userId);
        return Ok(products);
    }

    [Authorize]
    [HttpPost("add-to-cart")]
    public async Task<IActionResult> AddCartItem([FromQuery] int productId, [FromQuery] int quantity)
    {
        var isStockAvailable = await _purchaseService.CheckStockAsync(productId, quantity);

        if (!isStockAvailable)
            return UnprocessableEntity(new { message = "Requested quantity is not in stock" });

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var item = await _purchaseService.AddItemToCartAsync(productId, userId, quantity);

        return Ok(new {message = "Item added successfully", item});
    }

}

