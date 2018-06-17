using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Controllers
{
    public class RemotesController : Controller
    {
        [HttpGet]
        public JsonResult OrderDateNotInFuture(DateTime orderDate)  //The parametter type must be the same as the fields you want to validate
        {
            //test if the date value is in the past return true and do any othe manipulations want want
            if (orderDate <= DateTime.Now)
            {
                return Json(true);
            }
            else //else, return an error message
                return Json("Order date cannot be in the future");
        }

        //Apply this method to validate multiple fields in OrderMetaData
        [HttpGet]   //need this to create the relation with the annotation, client side
        public JsonResult CheckUserName(string userName, string firstName, string lastName)
        {
            userName = userName.ToLower();

            if (userName.Contains(firstName.ToLower()) || userName.Contains(lastName.ToLower()))
                return Json("Username can't derive from first name and last name");
            else
                return Json(true);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}