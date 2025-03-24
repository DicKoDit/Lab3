using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс кофейни, управляющий заказами и курьерами.
    /// </summary>
    public class CoffeeHouse
    {
        public List<Food> Menu { get; set; } = new List<Food>();
        private List<Courier> Couriers = new List<Courier>();
        public event Action<CoffeeHouse, OrderEventArgs> FindCourierNotify;
        public event Action<CoffeeHouse, OrderEventArgs> CheckNotify;

        public void CourierRegister(Courier courier)
        {
            Couriers.Add(courier);
            FindCourierNotify += courier.Delivery;
            courier.DeliveryNotify += FindCourier;
        }

        // Выбор лучшего доступного курьера (с наивысшим рейтингом)
        private Courier FindBestCourier()
        {
            return Couriers
                .Where(c => c.IsAvailable && c.AssignedOrders.Count < 3)
                .OrderByDescending(c => c.Rating) // Сортируем по убыванию рейтинга
                .FirstOrDefault();
        }

        public void MakeOrder(Client client, List<Food> food, bool delivery)
        {
            Console.WriteLine($"Получен новый заказ: {string.Join(", ", food.Select(f => f.Name))}, от клиента {client.Name}");
            Order order = new Order { Foods = food, Delivery = delivery };
            // Проверка баланса перед обработкой заказа
            double totalPrice = food.Sum(f => Menu.FirstOrDefault(m => m.Name == f.Name)?.Price ?? 0);
            if (client.Balance < totalPrice)
            {
                Console.WriteLine("Недостаточно средств! Заказ не может быть оформлен.");
                return;
            }
            CheckNotify += client.PayCheck;
            OrderPrepare(client, order);
        }

        private void OrderPrepare(Client client, Order order)
        {
            double totalPrice = 0;
            List<Food> finalOrder = new List<Food>();
            Random rand = new Random();

            foreach (var food in order.Foods)
            {
                var menuItem = Menu.FirstOrDefault(f => f.Name == food.Name);
                if (menuItem == null)
                {
                    Console.WriteLine($"{food.Name} нет в меню!");
                    continue;
                }
                if (rand.Next(0, 5) == 0)
                {
                    Console.WriteLine($"{food.Name} не готово!");
                    continue;
                }
                Console.WriteLine($"{food.Name} {menuItem.Price} руб. готово!");
                finalOrder.Add(menuItem);
                totalPrice += menuItem.Price;
            }

            order.Foods = finalOrder;
            order.TotalPrice = totalPrice;

            if (order.Delivery && client.Balance < order.TotalPrice)
            {
                Console.WriteLine("Недостаточно средств для оплаты доставки! Заказ отменён.");
                return;
            }

            if (order.Delivery)
            {
                Console.WriteLine("Поиск курьера!");
                var courier = FindBestCourier(); // Теперь ищем лучшего курьера

                if (courier != null)
                {
                    Console.WriteLine($"Назначен курьер: {courier.Name} (Рейтинг: {courier.Rating})");
                    courier.AssignedOrders.Add(order); // Назначаем заказ курьеру
                    FindCourierNotify?.Invoke(this, new OrderEventArgs(order, $"Назначен курьер: {courier.Name} (Рейтинг: {courier.Rating})"));
                }
                else
                {
                    Console.WriteLine("Нет доступных курьеров для доставки!");
                }
            }
            else
            {
                CheckNotify?.Invoke(this, new OrderEventArgs(order, "Способ получения заказа: самовывоз!"));
            }
        }

        private void FindCourier(Courier sender)
        {
            Couriers.Add(sender);
        }
    }
}