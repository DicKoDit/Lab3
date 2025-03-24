using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Класс, представляющий параметры заказа.
    /// </summary>
    public class OrderEventArgs : EventArgs
    {
        public Order Order { get; }
        public string Message { get; }
        public OrderEventArgs(Order order, string message)
        {
            Order = order;
            Message = message;
        }
    }
}
