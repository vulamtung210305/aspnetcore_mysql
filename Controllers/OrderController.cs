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
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MvcMovie.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderController> _logger;
        private IOrderService _service;

        public OrderController(IUnitOfWork unitOfWork, ILogger<OrderController> logger, IOrderService service)
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
            var orders = await _unitOfWork.Orders.All();
            return View(orders.ToPagedList(pageIndex, pageSize));
        }

        // POST: Product
        public async Task<IActionResult> Create()
        {
            var products = await _unitOfWork.Products.All();
            var orderDto = OrderDto.Create();
            orderDto.Products.AddRange(products.Select(m => new SelectListItem()
            {
                Value = $"{m.Id}",
                Text = m.Name
            }).ToList());
            return View(orderDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] OrderDto orderDto)
        {

            if (ModelState.IsValid)
            {
                var errors = await _service.Validate(orderDto);
                if (errors.Count() == 0)
                {
                    bool result = await _service.Save(orderDto);
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
            return View(orderDto);
        }
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _service.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ExpiryDate")] OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var errors = await _service.Validate(orderDto);
                if (errors.Count() == 0)
                {
                    var result = await _service.Save(orderDto);
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
            return View(orderDto);
        }
    }
}