using Microsoft.AspNet.Identity;
using RentMgt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;


namespace RentMgt.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {

        RentMgtEntities houseDB = new RentMgtEntities();
        // GET: House
        public ActionResult Index()
        {
            var items = houseDB.Items.ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult Index(string srch,int? page)
        {
            
            ViewData["GetHouseDetails"] = srch;
            
            var query = from x in houseDB.Items select x;
            if(!String.IsNullOrEmpty(srch))
            {
                query = query.Where(x => x.Title.Contains(srch) || x.Description.Contains(srch) ||
                x.City.Contains(srch) || x.Address.Contains(srch)).OrderBy(x => x.ItemId);
            }
            //return View(await query.AsNoTracking().ToListAsync());            
            return View(query.ToList());
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = houseDB.Categories.ToList();
            return PartialView(categories);

        }
        public ActionResult Browse(string category)
        {
            var categoryModel = houseDB.Categories.Include("Items").Single(c=>c.Name==category);
            return View(categoryModel);
        }
        public ActionResult Details(int id)
        {
            var Item = houseDB.Items.Find(id);
            return View(Item);
        }
        public ActionResult Booking()
        {
            string umail = User.Identity.GetUserName().ToString();
            var items = houseDB.BookDetails.Where(i => i.Email == umail);
            return View(items.ToList());
        }

        public ActionResult CancelBooking(int? id)
        {
            var x = (from rec in houseDB.BookDetails where rec.BookDetailId == id select rec).SingleOrDefault();
            houseDB.BookDetails.Remove(x);
            houseDB.SaveChanges();                        
            return View();
            
        }
        
    }
}