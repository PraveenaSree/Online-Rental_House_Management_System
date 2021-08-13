using RentMgt.Models;
using RentMgt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentMgt.Controllers
{
    public class BookingCartController : Controller
    {
        RentMgtEntities houseDB = new RentMgtEntities();

        public ActionResult Index()
        {
            var cart = BookingCart.GetCart(this.HttpContext);


            var viewModel = new BookingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            var addedItem = houseDB.Items
                .Single(item => item.ItemId == id);


            var cart = BookingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedItem);


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {

            var cart = BookingCart.GetCart(this.HttpContext);


            string itemName = houseDB.Carts
                .Single(item => item.RecordId == id).Item.Title;


            int itemCount = cart.RemoveFromCart(id);


            var results = new BookingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = BookingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}