using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaProject.Models
{
    public class Cart
    {
        public Pizza Pizza { get; set; }
        public int Quantity { get; set; }

        public Cart(Pizza pizza, int quantity) {
            Pizza = pizza;
            Quantity = quantity;
        }
    }
}