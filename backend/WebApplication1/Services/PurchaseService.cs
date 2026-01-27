
using Microsoft.EntityFrameworkCore;

public class PurchaseService
{
    private readonly AppDbContext _context;
    private readonly ProductService _productService;

    public PurchaseService(AppDbContext context, ProductService productService)
    {
        _context = context;
        _productService = productService;
    }

    public async Task<List<ProductResponseDto>> GetCartItemsAsync(int userId)
    {
        var productIds = await _context.CartItems.Where(i => i.UserId == userId).Select(i => i.ProductId).ToListAsync();
        var productList = new List<ProductResponseDto>();

        foreach (var productId in productIds)
        {
            var product = await _productService.GetByIdAsync(productId);
            var productResponse = await _productService.BuildProductResponse(product);
            productList.Add(productResponse);
        }
        return productList;
    }

    public async Task<CartItem> AddItemToCartAsync(int productId, int userId, int quantity)
    {
        CartItem item = new CartItem() { ProductId = productId, UserId = userId, Quantity = quantity };
        _context.CartItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> CheckStockAsync(int productId, int quantity)
    {
        var product = await _productService.GetByIdAsync(productId);
        var StockQuantity = product?.Quantity;

        return StockQuantity > quantity;
    }
}

