using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс, представляющий заказ.
    /// </summary>
    public class Order
    {
        public List<Food> Foods { get; set; }
        public double TotalPrice { get; set; }
        public bool Delivery { get; set; }
    }
}
