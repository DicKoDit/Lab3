using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static void Main()
        {
            CoffeeHouse coffeeHouse = new CoffeeHouse();
            coffeeHouse.Menu.Add(new Food { Name = "Латте", Price = 350 });
            coffeeHouse.Menu.Add(new Food { Name = "Панкейки", Price = 400 });
            coffeeHouse.Menu.Add(new Food { Name = "Капучино", Price = 250 });
            coffeeHouse.Menu.Add(new Food { Name = "Чипсы", Price = 124 });
            coffeeHouse.Menu.Add(new Food { Name = "Вода", Price = 50 });
            coffeeHouse.Menu.Add(new Food { Name = "ТочноНеДети", Price = 1000000 });

            Console.Write("Введите имя клиента: ");
            string clientName = Console.ReadLine();

            Console.Write("Введите баланс клиента: ");
            double balance = double.Parse(Console.ReadLine());

            Client client = new Client { Name = clientName, Balance = balance };

            // Подписка на события, чтобы получать сообщения от клиента
            client.OnOrderProcessed += message => Console.WriteLine(message);

            // Регистрируем несколько курьеров с разными рейтингами
            Courier courier1 = new Courier { Name = "HarryNoWizard", Rating = 1 };
            Courier courier2 = new Courier { Name = "Ктони Стар", Rating = 2 };
            Courier courier3 = new Courier { Name = "Billy Bons", Rating = 4 };
            Courier courier4 = new Courier { Name = "AlicaFrame", Rating = 5 };

            coffeeHouse.CourierRegister(courier1);
            coffeeHouse.CourierRegister(courier2);
            coffeeHouse.CourierRegister(courier3);
            coffeeHouse.CourierRegister(courier4);

            List<Food> orderItems = new List<Food>();

            Console.WriteLine("Меню:");
            foreach (var food in coffeeHouse.Menu)
            {
                Console.WriteLine($"{food.Name} - {food.Price} руб.");
            }

            //Заказ ---------------------
            Console.WriteLine("Введите названия блюд через запятую:");
            string[] selectedDishes = Console.ReadLine().Split(',');

            foreach (string dishName in selectedDishes)
            {
                var foodItem = coffeeHouse.Menu.FirstOrDefault(f => f.Name.Equals(dishName.Trim(), StringComparison.OrdinalIgnoreCase));
                if (foodItem != null)
                {
                    orderItems.Add(foodItem);
                }
                else
                {
                    Console.WriteLine($"Блюдо {dishName.Trim()} не найдено в меню.");
                }
            }

            Console.Write("Вы хотите доставку? (да/нет): ");
            bool delivery = Console.ReadLine().ToLower() == "да";

            coffeeHouse.MakeOrder(client, orderItems, delivery);
            Console.ReadKey();
        }

    }
}
