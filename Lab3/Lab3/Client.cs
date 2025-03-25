using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс, клиента. Его Имя, баланс и что назаказывал.
    /// </summary>
    public class Client
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        private List<Order> Orders = new List<Order>();

        // Событие для уведомлений
        public event Action<string> OnOrderProcessed;

        public void PayCheck(CoffeeHouse sender, OrderEventArgs e)
        {
            string message;

            Balance -= e.Order.TotalPrice;
            Orders.Add(e.Order);
            message = $"{Name}, ваш заказ готов! Оплачено: {e.Order.TotalPrice}.";

            // Cообщение через событие
            OnOrderProcessed?.Invoke(message);
        }
    }
}
