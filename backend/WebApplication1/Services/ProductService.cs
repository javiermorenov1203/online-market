using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetMostViewedAsync(int limit = 8)
    {
        return await _context.Products
            .OrderByDescending(p => p.Views)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<Product>> GetBestSellersAsync(int limit = 8)
    {
        return await _context.Products
            .OrderByDescending(p => p.UnitsSold)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<Product>> GetDiscountedProductsAsync(int limit = 8)
    {
        var discounted = await  _context.Products
            .Where(p => p.Discount != null && p.Discount != 0)
            .ToListAsync();

        var bestSellers = await GetBestSellersAsync();
        var bestSellerIds = bestSellers.Select(p => p.Id).ToHashSet();

        // exclude best sellers
        var filteredDiscounts = discounted
            .Where(p => !bestSellerIds.Contains(p.Id))
            .Take(limit)
            .ToList();

        return filteredDiscounts;
    }

    public async Task<List<ProductResponseDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return await BuildProductResponseList(products);
    }

    public async Task<Product> CreateProductAsync(ProductPostDto dto, int publisherId)
    {
        var product = new Product(dto);

        product.PublisherId = publisherId;
        var discount = product.Discount ?? 0;
        product.FinalPrice = product.BasePrice - (product.BasePrice * (discount / 100m));
        product.Views = 0;
        product.UnitsSold = 0;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Save images
        foreach (var img in dto.Images)
        {
            var pi = new ProductImage { ProductId = product.Id, Image = img };
            _context.ProductImages.Add(pi);
        }

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<ProductResponseDto> BuildProductResponse(Product product)
    {
        var images = await _context.ProductImages
            .Where(i => i.ProductId == product.Id)
            .ToListAsync();

        return new ProductResponseDto(product, images);
    }

    public async Task<List<ProductResponseDto>> BuildProductResponseList(List<Product> products)
    {
        var list = new List<ProductResponseDto>();

        foreach (var product in products)
            list.Add(await BuildProductResponse(product));

        return list;
    }
}
