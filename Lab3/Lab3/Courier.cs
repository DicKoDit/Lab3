using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс с курьером: Имя, Рейтинг, Может/Нет принять заказ и Делигат с оповищением о взятии заказа
    /// </summary>
    public class Courier
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public bool IsAvailable { get; set; } = true; 
        public List<Order> AssignedOrders = new List<Order>();

        // Событие для уведомления что Курьер взял заказ
        public event Action<string> OnCourierUpdate;
        public event Action<Courier> DeliveryNotify;

        public void Delivery(CoffeeHouse sender, OrderEventArgs e)
        {
            if (IsAvailable && AssignedOrders.Count < 3)
            {
                AssignedOrders.Add(e.Order);

                // Передача сообщения через событие
                OnCourierUpdate?.Invoke($"Курьер {Name} взял заказ!");

                DeliveryNotify?.Invoke(this);
            }
            else
            {
                OnCourierUpdate?.Invoke($"Курьер {Name} не доступен для доставки.");
            }
        }
    }
}
