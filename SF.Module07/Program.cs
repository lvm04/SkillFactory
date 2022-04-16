using System;

namespace SF.Module7
{
    class Program
    {
        static void Main(string[] args)
        {
            // Персонал
            Employee emp1 = new Employee(1, "Ирина Соколова", "Бухгалтер", new DateTime(1982, 4, 17), 60000);
            emp1.Address = "г. Васюки, ул. Попова, д.2";
            emp1.Phone = "33-22-11";
            Employee emp2 = new Employee(2, "Петр Иванов", "Менеджер", new DateTime(1993, 7, 21),
                     "г.Воронеж, ул. ленина, д.45, кв.14", "55-66-77", 50000);
            Employee emp3 = new Employee(3, "Сергей Шпак", "Продавец", new DateTime(1996, 10, 14), 40000)
            {
                Address = "г.Самара, ул. Пивная, д.7, кв.108",
                Phone = "123-54-76"
            };
            emp3.Display();

            // Клиенты
            Customer cust1 = new Customer(1, "Эдуард Сидоров")
            {
                Address = "не известен",
                Phone = "431-56-09",
                ContactName = "Эдик"
            };
            Customer cust2 = new Customer(2, "Борис Шнеерсон")
            {
                Address = "г.Москва, ул. Лубянка, д.1",
                PostalCode = "112234",
                Phone = "987-01-77",
                ContactName = "Борис"
            };
            cust2.Display();

            // Категории товаров
            Console.WriteLine();
            Categories.Get.Display();
            Console.WriteLine(Categories.Get[1].Name);
            Console.WriteLine(Categories.Get["Смартфон"].Id);
            Console.WriteLine(Categories.Get["Кофеварка"].Name);

            // Товары
            Catalog products = Catalog.Get;
            Console.WriteLine();
            products[2].Display();
            products.Display();

            // Создаем ордера
            int orderID = 1;
            Product[] ordProd1 = new Product[] { products[0], products[3] };
            int[] qnt1 = new[] { 1, 2 };
            HomeDelivery delivery1 = new HomeDelivery(orderID, "110056, г.Краснодар, ул. Ударная, д.4 кв.32");

            Order<HomeDelivery, int> order1 = new Order<HomeDelivery, int>(orderID, new DateTime(2021, 10, 27),
                    cust1, delivery1, ordProd1, qnt1)
            {
                Seller = emp3,
                Manager = emp2,
                Description = "Оплачено картой VISA",
                Bonus = 1000
            };
            string deliveryNumber1 = delivery1.RunDelivery(cust1.Name);   // номер доставки отдать клиенту 

            orderID = 2;
            Product[] ordProd2 = new Product[] { products[1], products[4] };
            int[] qnt2 = new[] { 2, 3 };
            // Пункт выдачи товара пользователь выбирает из списка. Его код передаем в конструктор
            ShopDelivery delivery2 = new ShopDelivery(orderID, "__PICKPOINT#24");

            Order<ShopDelivery, int> order2 = new Order<ShopDelivery, int>(orderID, new DateTime(2021, 10, 28),
                    cust2, delivery2, ordProd2, qnt2)
            {
                Seller = emp3,
                Manager = emp2,
                Description = "Оплата при получении",
                Bonus = 2000
            };
            string deliveryNumber2 = delivery2.RunDelivery(cust2.Name);   // номер доставки отдать клиенту

            // Другая реализация класса Delivery
            orderID = 3;
            Product[] ordProd3 = new Product[] { products[2], products[3], products[5] };
            int[] qnt3 = new[] { 1, 2, 1 };
            Shop destination1 = new Shop(6) 
            { 
                Address = cust2.Address,
                RecipientName = cust2.Name,
                Phone = cust2.Phone
            };
            ConcreteDelivery<Shop> delivery3 = new ConcreteDelivery<Shop>(orderID, destination1);

            Order<ConcreteDelivery<Shop>, int> order3 = new Order<ConcreteDelivery<Shop>, int>(orderID, new DateTime(2021, 10, 29),
                    cust2, delivery3, ordProd3, qnt3)
            {
                Seller = emp3,
                Manager = emp2,
                Description = "Оплата при получении",
                Bonus = 2400
            };
            order3.DisplayAddress();
            string deliveryNumber3 = delivery3.RunDelivery();


            // Перегрузка оператора
            Console.WriteLine("Общая сумма заказов: {0:f2}", order1.Details + order2.Details);

            // Метод расширения
            // emp1.SaveXMLFile("employee1.xml");

        }
    }
}
