using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public abstract class HomeController : ControllerBase
    {
        protected readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
    }
}
