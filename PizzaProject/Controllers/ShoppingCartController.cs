using PizzaProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PizzaProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private PizzaProjectContext db = new PizzaProjectContext();
        private string strCart = "Cart";

        // GET: ShoppingCart
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Order(int? id) {

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Cart> lsCart = new List<Cart> {
                    new Cart(db.Pizzas.Find(id),1)
                };
                Session[strCart] = lsCart;
            }
            else {
                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = IsExistingCheck(id);
                if (check == -1)
                {
                    lsCart.Add(new Cart(db.Pizzas.Find(id), 1));
                }
                else
                {
                    lsCart[check].Quantity++;
                }
                Session[strCart] = lsCart;
            }
            return View("Index");
        }

        private int IsExistingCheck(int? id) {
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < lsCart.Count; i++) {
                if (lsCart[i].Pizza.PizzaId == id) return i;
            }
            return -1;
        }

        [AllowAnonymous]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.RemoveAt(check);
            return View("Index");
        }

        [Authorize]
        public ActionResult Checkout(FormCollection frc) {

            return View("Checkout");
        }

        [Authorize]
        public ActionResult ProcessOrder(FormCollection frc)
        {
            List<Cart> lstCart = (List<Cart>)Session[strCart];

            Order order = new Order()
            {
                FirstName = frc["cusName"],
                LastName = frc["cusLast"],
                PhoneNumber = frc["cusPhone"],
                Email = frc["cusEmail"],
                OrderDate = DateTime.Now
            };

            db.Orders.Add(order);
            db.SaveChanges();

            foreach(Cart cart in lstCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    PizzaId = cart.Pizza.PizzaId,
                    Quantity = cart.Quantity,
                    Price = cart.Pizza.Price
                };
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            Session.Remove(strCart);

            return View("OrderSuccess");
        }
    }
}