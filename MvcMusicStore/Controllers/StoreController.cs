using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        // Index is the default action for this controller
        // in the Store controller, this will list genres
        // of music to select from
        public string Index()
        {
            return "Hello from store Index()";
        }

        // Action to browse all albums available for the selected genre
        // - allows users to select albums to see their details
        public string Browse(string product)
        {
            //return "hello from store Browse()";
            return "Looking for this product: " + product;
        }

        // action to display the details of a selected album
        public string Details(Int32 id, string prodName)
        {
            return "Details for Product: " + id.ToString() + " " + prodName;
        }

        // sample action for View examples
        public IActionResult Sample()
        {
            ViewBag.message = "this is the ViewBag property 'message'";
            return View("Sample");
        }

        //// return a collection of generic album titles
        //public IActionResult List()
        //{
        //    List<Album> albums = new List<Album>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        albums.Add(new Album { Title = "Generic Album " + i });
        //    }
        //    ViewBag.Albums = albums; // create new entry in ViewData
        //    return View();           // render ~/Views/Store/List.cshtml
        //}

        //Week-3: Create an Action: Register
        public string Register(string prodName, string color)
        {
            string text = "Product information: ";

            return text + System.Environment.NewLine + prodName.ToString() + " " + color;
        }
        // return a collection of generic album titles
        public IActionResult List()
        {
            List<Album> albums = new List<Album>();
            for (int i = 0; i < 10; i++)
            {
                albums.Add(new Album { Title = "Generic Album " + i });
            }
            return View(albums);    // default to ~/Views/Store/List.cshtml
        }



    }
}
