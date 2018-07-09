using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaibaReduxAPI.Models
{
    public class Priceline
        // represents a quantity-price combination for a specific item
        // Ex: Ribs-
        //          full rack $20
        //          half rack $12
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Position { get; set; }

        public Priceline(int id, string description, float price, float position)
        {
            Id = id;
            Description = description;
            Price = price;
            Position = position;
        }
    }
}
