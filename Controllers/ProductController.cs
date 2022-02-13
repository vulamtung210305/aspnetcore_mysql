using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext context;

        public ProductController(DataContext context)
        {
            this.context = context;
        }

        // 
        // GET: /HelloWorld/

        public async Task<IActionResult> Index()
        {
            var products = await context.Products.ToListAsync();
            return View(products);
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}