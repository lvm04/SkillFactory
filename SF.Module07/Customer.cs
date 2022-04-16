using System;

namespace SF.Module7
{
    // Клиент
    class Customer : Person
    {
        public string ContactName { get; set; }
        public string PostalCode { get; set; }
        public Customer(int id, string name) : base(id, name)
        {
            
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Контактное имя : {0}", ContactName);
            Console.WriteLine("Почтовый индекс: {0}", PostalCode);
        }

    }
}