using System;

namespace SF.Module7
{
    abstract class Delivery<T> where T : Destination
    {
        protected string Number;        // номер доставки
        protected int OrderID { get; }
       
        public string Address { get; }
        public T Recipient { get; set; }

        abstract public string RunDelivery();

        public Delivery(int orderID, T recipient)
        {
            OrderID = orderID;
            Number = GenerateNumber();
            Recipient = recipient;
            Address = recipient.Address;
        }

        private string GenerateNumber()
        {
            return "some code";
        }

        protected virtual void SendOrderToTransportCompany(string clientName, string address, string deliveryNumber)
        {
            Console.WriteLine("Работа Интернет-магазина с транспортной компанией");
        }

        public override string ToString()
        {
            return Address;
        }
    }

    class ConcreteDelivery<T> : Delivery<T> where T : Destination
    {
        public ConcreteDelivery(int orderID, T recipient) : base(orderID, recipient)
        {
            
        }
        public override string RunDelivery()
        {
            if (Recipient.GetType() == typeof(Home))
            {
                SendOrderToTransportCompany(Recipient.RecipientName, Address, Number);
            }
            else if (Recipient.GetType() == typeof(PickPoint))
            {
                SendOrderToTransportCompany(Recipient.RecipientName, (Recipient as PickPoint).PickPointInfo, Number); ;
            }
            else if (Recipient.GetType() == typeof(Shop))
            {
                SendOrderToOwnDeliveryService(Recipient.RecipientName, (Recipient as Shop).ShopInfo, Number); ;
            }
            else
            {
                Console.WriteLine("Ошибка! Передан некорректный получатель");
                return "error";
            }

            return Number;
        }

        private void SendOrderToOwnDeliveryService(string clientName, string shopInfo, string Number)
        {
            Console.WriteLine("Работа Интернет-магазина со своей службой доставки");
        }

    }


    class Destination
    {
        public string Address { get; set; }
        public string RecipientName { get; set; }
        public string Phone { get; set; }

        //public Destination() { }
    }

    class Home : Destination
    {
        public string ContactName { get; set; }
    }

    class PickPoint : Destination
    {
        public int Id { get; set; }
        public string OpeningHours { get; set; }        // режим работы
        public string PickPointInfo 
        { 
            get 
            {
                return "Информация о пункте доставки найденная по Id";
            } 
        }
        public PickPoint(int id)
        {
            Id = id;
        }      
    }

    class Shop : Destination
    {
        public int Id { get; set; }
        public string OpeningHours { get; set; }
        public string Manager { get; set; }
        public string ShopInfo
        {
            get
            {
                return "Информация о магазине найденная по Id";
            }
        }
        public Shop(int id)
        {
            Id = id;
        }
    }
}