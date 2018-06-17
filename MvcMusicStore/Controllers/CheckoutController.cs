using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMusicStore.Controllers
{
    public class CheckoutController : Controller
    {
        //Add the context
        private readonly MvcMusicStoreContext _context;

        public CheckoutController(MvcMusicStoreContext context)
        {
            _context = context;
        }

        //defaut get: initializing something: **** Turn on Create as a partial view when you create a view for this controller
        public IActionResult AddressAndPayment()
        {
            //Create a new instance of order
            Order order = new Models.Order()
            {
                OrderDate = DateTime.Now
            };

            //Create a list
            ViewBag.CountryCode = new SelectList(_context.Country.OrderBy(a => a.Name), "CountryCode", "Name");
            ViewBag.ProvinceCode = new SelectList(_context.Province.OrderBy(a => a.Name), "ProvinceCode", "Name");

            return View(order);
        }

        //default post method: to write to the database
        [HttpPost]
        public async Task<IActionResult> AddressAndPayment(Order order)
        {
            try
            {
                //first, test if the model is in a stable state
                if (ModelState.IsValid)
                {
                    _context.Add(order);
                    await _context.SaveChangesAsync();  //save the changes in model
                    return RedirectToAction("AddressAndPayment");
                }
            }
            catch (Exception ex)
            {
                //Show the error message, and flag an error on the model state
                ModelState.AddModelError("", $"Error inserting new order:ex.Message); {ex.GetBaseException().Message}");
            }

            AddressAndPayment();
            return View(order);
        }

    }
}