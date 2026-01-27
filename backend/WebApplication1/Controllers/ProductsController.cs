using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    // GET /api/products
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        var response = await _productService.BuildProductResponseList(products);
        return Ok(response);
    }

    // GET /api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        var response = await _productService.BuildProductResponse(product);
        return Ok(new { message = "Product found", product = response });
    }

    // GET /api/products/most-viewed
    [HttpGet("most-viewed")]
    public async Task<IActionResult> GetMostViewed()
    {
        var products = await _productService.GetMostViewedAsync();
        var response = await _productService.BuildProductResponseList(products);
        return Ok(response);
    }

    // GET /api/products/best-sellers
    [HttpGet("best-sellers")]
    public async Task<IActionResult> GetBestSellers()
    {
        var products = await _productService.GetBestSellersAsync();
        var response = await _productService.BuildProductResponseList(products);
        return Ok(response);
    }

    // GET /api/products/discounts
    [HttpGet("discounts")]
    public async Task<IActionResult> GetDiscountedProducts()
    {
        var discounted = await _productService.GetDiscountedProductsAsync();
        var response = await _productService.BuildProductResponseList(discounted);
        return Ok(response);
    }

    // GET /api/products?categoryId
    [HttpGet("get")]
    public async Task<IActionResult> GetProductsByCategory([FromQuery] int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }

    // POST /api/products
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostProduct(ProductPostDto dto)
    {
        var publisherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var product = await _productService.CreateProductAsync(dto, publisherId);

        var response = await _productService.BuildProductResponse(product);

        return Ok(new { message = "Product added successfully", product = response });
    }
}
