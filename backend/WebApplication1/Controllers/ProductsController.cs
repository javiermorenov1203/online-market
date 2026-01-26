using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    // GET /api/products
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _service.GetAllAsync();
        var response = await _service.BuildProductResponseList(products);
        return Ok(response);
    }

    // GET /api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        var response = await _service.BuildProductResponse(product);
        return Ok(new { message = "Product found", product = response });
    }

    // GET /api/products/most-viewed
    [HttpGet("most-viewed")]
    public async Task<IActionResult> GetMostViewed()
    {
        var products = await _service.GetMostViewedAsync();
        var response = await _service.BuildProductResponseList(products);
        return Ok(response);
    }

    // GET /api/products/best-sellers
    [HttpGet("best-sellers")]
    public async Task<IActionResult> GetBestSellers()
    {
        var products = await _service.GetBestSellersAsync();
        var response = await _service.BuildProductResponseList(products);
        return Ok(response);
    }

    // GET /api/products/discounts
    [HttpGet("discounts")]
    public async Task<IActionResult> GetDiscountedProducts()
    {
        var discounted = await _service.GetDiscountedProductsAsync();
        var response = await _service.BuildProductResponseList(discounted);
        return Ok(response);
    }

    // POST /api/products
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostProduct(ProductPostDto dto)
    {
        var publisherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var product = await _service.CreateProductAsync(dto, publisherId);

        var response = await _service.BuildProductResponse(product);

        return Ok(new { message = "Product added successfully", product = response });
    }
}
