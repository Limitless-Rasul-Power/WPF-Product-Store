using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Product.Static_Classes
{
    public static class Configuration
    {
        public static List<Product> GetProducts()
        {
                    return new List<Product>
                {
                    new Product
                    {
                        Name = "Freedom",
                        ImagePath = @"/img/Freedom.jpg"
                    },

                    new Product
                    {
                        Name = "Purpose",
                        ImagePath = @"/img/bugatti.PNG"
                    },

                    new Product
                    {
                        Name = "Victory",
                        ImagePath = @"/img/Victory.jpg"
                    }

                };
        }
    }
}