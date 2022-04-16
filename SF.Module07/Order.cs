using System;

namespace SF.Module7
{
    class Order<TDelivery, TStruct> where TDelivery : class where TStruct : struct
    {
        public int OrderID;             // Number
        public string Description;
        public DateTime OrderDate;
        public TDelivery Delivery;      // информация о доставке

        public OrderDetails Details;    // детализация заказа

        public Customer Client;         
        public Employee Seller;         // продавец, который оформил ордер
        public Employee Manager;        // менеджер, который завизировал
        public TStruct Bonus;

        public Order(int orderID, DateTime date, Customer customer, TDelivery delivery,
                    Product[] products, int[] quantity)
        {
            OrderID = orderID;
            OrderDate = date;
            Client = customer;
            Delivery = delivery;
            Details = new OrderDetails(orderID, products, quantity);    // Композиция
        }

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery);
        }        
    }

    class OrderDetails
    {
        public int OrderID;

        public Product[] Products;
        public int[] Quantity;
        public decimal TotalCost        // полная стоимость заказа
        {
            get
            {
                if (Products != null)
                {
                    decimal total = 0.0M;

                    for (int i = 0; i < Products.Length; i++)
                    {
                        total += Products[i].UnitPrice * Quantity[i];
                    }
                    return total;
                }
                else
                    return 0.0M;
            }
        }
        public OrderDetails(int orederID, Product[] products, int[] quantity)
        {
            if (products.Length == quantity.Length)
            {
                OrderID = orederID;
                Products = products;
                Quantity = quantity;
            }
            else
                Console.WriteLine("Ошибка в деталях заказа. Проверьте количество товара");
        }

        public static decimal operator +(OrderDetails order1, OrderDetails order2)
        {
            return order1.TotalCost + order2.TotalCost;
        }
    }
}
