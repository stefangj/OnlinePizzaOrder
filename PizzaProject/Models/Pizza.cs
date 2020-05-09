using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaProject.Models
{
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

    }
}