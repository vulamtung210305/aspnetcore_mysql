using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Repositories;
using MvcMovie.Services;
using System.Text.Encodings.Web;
using X.PagedList;
namespace MvcMovie.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        private IProductService _service;

        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IProductService service)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _service = service;
        }

        // 
        // GET: /HelloWorld/

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var products = await _unitOfWork.Products.All();
            return View(products.ToPagedList(pageIndex, pageSize));
        }

        // POST: Product
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,ExpiryDate")] ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                // await _unitOfWork.Products.Add(product);
                // await _unitOfWork.CompleteAsync();
                var errors = await _service.Validate(productDto);
                if (errors.Count() == 0)
                {
                    bool result = await _service.Save(productDto);
                    if (result)
                    {
                        TempData["Message"] = "Saved";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                }
            }
            return View(productDto);
        }
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _service.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ExpiryDate")] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var errors = await _service.Validate(productDto);
                if (errors.Count() == 0)
                {
                    var result = await _service.Save(productDto);
                    if (result)
                    {
                        TempData["Message"] = "Saved";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                }
            }
            return View(productDto);
        }
    }
}