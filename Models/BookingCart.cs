using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentMgt.Models
{
    public class BookingCart
    {
        RentMgtEntities houseDB = new RentMgtEntities();
        string BookingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static BookingCart GetCart(HttpContextBase context)
        {
            var cart = new BookingCart();
            cart.BookingCartId = cart.GetCartId(context);
            return cart;
        }
        //helper method
        public static BookingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Item item)
        {
            var cartItem = houseDB.Carts.SingleOrDefault(
                c => c.CartId == BookingCartId
                && c.ItemId == item.ItemId);

            if(cartItem == null)
            {
                cartItem = new Cart
                {
                    ItemId = item.ItemId,
                    CartId = BookingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now,
                    Creator = item.Email,
                    Title = item.Title,
                    //Phone = houseDB.Registers.SingleOrDefault(p => p.Email == item.Email).ToString(),
                    Phone =(from Role in houseDB.Registers where Role.Email == item.Email select Role.Phone).SingleOrDefault()

            };
                houseDB.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            houseDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = houseDB.Carts.Single(
                cart => cart.CartId == BookingCartId
                && cart.RecordId == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if(cartItem.Count>1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    houseDB.Carts.Remove(cartItem);
                }
                houseDB.SaveChanges();
            }
            return itemCount;                
        }

        public void EmptyCart()
        {
            var cartItems = houseDB.Carts.Where(

                cart => cart.CartId == BookingCartId);
            foreach (var cartItem in cartItems)
            {
                houseDB.Carts.Remove(cartItem);
            }
            houseDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return houseDB.Carts.Where(
                cart => cart.CartId == BookingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in houseDB.Carts
                          where cartItems.CartId == BookingCartId
                          select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in houseDB.Carts
                              where cartItems.CartId == BookingCartId
                              select (int?)cartItems.Count *
                              cartItems.Item.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Book order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var bookDetail = new BookDetail
                {
                    ItemId = item.ItemId,
                    BookId = order.BookId,
                    UnitPrice = item.Item.Price,
                    Quantity = item.Count,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Phone = order.Phone,
                    Email = order.Email,
                    BookDate = order.BookDate,
                    Creator = item.Creator,
                    Title = item.Title
                                       
                    
                };
                

                orderTotal += (item.Count * item.Item.Price);

                houseDB.BookDetails.Add(bookDetail);
                

            }

            order.Total = orderTotal;


            houseDB.SaveChanges();

            EmptyCart();

            return order.BookId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {

                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        public void MigrateCart(string Email)
        {
            var bookingCart = houseDB.Carts.Where(
                c => c.CartId == BookingCartId);

            foreach (Cart item in bookingCart)
            {
                item.CartId = Email;
            }
            houseDB.SaveChanges();
        }

    }
}