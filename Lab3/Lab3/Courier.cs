using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс, представляющий курьера.
    /// </summary>
    public class Courier
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<Order> AssignedOrders = new List<Order>();
        public event Action<Courier> DeliveryNotify;

        public void Delivery(CoffeeHouse sender, OrderEventArgs e)
        {
            if (IsAvailable && AssignedOrders.Count < 3)
            {
                AssignedOrders.Add(e.Order);
                Console.WriteLine($"Курьер: {Name} может принять заказ!");
                //   Сообщаем подписчикам, что курьер принял заказ
                DeliveryNotify?.Invoke(this);
            }
        }
    }
}
