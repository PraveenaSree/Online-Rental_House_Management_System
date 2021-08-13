using RentMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace RentMgt.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        RentMgtEntities houseDB = new RentMgtEntities();
        

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddressAndPaymentAsync(FormCollection values)
        {
            var book = new Book();
            var item=new Item();
            TryUpdateModel(book);

            try
            {
                book.Username = User.Identity.Name;
                book.BookDate = DateTime.Now;
                //book.ItemId = item.ItemId;               
                

                houseDB.Books.Add(book);
                houseDB.SaveChanges();

                //book.ItemId = (from x in houseDB.BookDetails where x.BookId == book.BookId select x.ItemId).Last();
                //houseDB.SaveChanges();
                
                var cart = BookingCart.GetCart(this.HttpContext);
                cart.CreateOrder(book);

                

                return RedirectToAction("Complete",new { id = book.BookId });
               
            }
            catch
            {
                return View(book);
            }
        }

        public ActionResult Complete(int? id)
        {

            bool isValid = houseDB.Books.Any(
                o => o.BookId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}