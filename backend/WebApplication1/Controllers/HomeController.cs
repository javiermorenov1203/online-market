using Microsoft.AspNetCore.Mvc;

public abstract class HomeController : ControllerBase
{
    protected readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }
}

