using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс с заказом: Список заказанного, Финальная цена заказа и Нужна ли доставка.
    /// </summary>
    public class Order
    {
        public List<Food> Foods { get; set; }
        public double TotalPrice { get; set; }
        public bool Delivery { get; set; }
    }
}
