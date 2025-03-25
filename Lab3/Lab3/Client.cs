using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс, клиента. Его Имя, баланс и что назаказывал.
    /// pharam 
    /// </summary>
    public class Client
    {
        public string Name { get; set; } 
        public double Balance { get; set; }
        private List<Order> Orders = new List<Order>(); // Чек заказа

        // Событие для уведомлений
        public event Action<string> OnOrderProcessed;

        //Проверка на оплату заказа
        public void PayCheck(CoffeeHouse sender, OrderEventArgs e) // Вызываем событие
        {
            string message;

            Balance -= e.Order.TotalPrice;
            Orders.Add(e.Order);//Добавил заказ в Чек
            message = $"{Name}, ваш заказ готов! Оплачено: {e.Order.TotalPrice}.";

            // Cообщение через событие
            OnOrderProcessed?.Invoke(message);
        }
    }
}
