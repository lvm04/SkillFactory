
using System;

namespace SF.Module7
{
    abstract class Delivery
    {
        protected string Number;        // номер доставки
        protected int _orderID;
        protected int OrderID
        {
            set { _orderID = value; }
        }
        protected string _address;
        public string Address { get; }

        abstract public string RunDelivery(string clientName);

        public Delivery(int orderID, string address)
        {
            OrderID = orderID;
            _address = address;
        }

        protected string GenerateNumber()
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

    class HomeDelivery : Delivery
    {
        public HomeDelivery(int orderID, string address) : base(orderID, address)
        {
            Number = GenerateNumber();
        }

        public override string RunDelivery(string clientName)
        {
            SendOrderToTransportCompany(clientName, Address, Number);
            return Number;
        }
    }

    class PickPointDelivery : Delivery
    {
        private int pickPointID;
        private string pickPointInfo;

        public PickPointDelivery(int orderID, string address) : base(orderID, address)
        {
            Number = GenerateNumber();
            if (_address.StartsWith("__PICKPOINT"))
            {
                if (int.TryParse(address.Substring(12), out pickPointID))
                {
                    pickPointInfo = GetPickPointInfo(pickPointID);
                }
            }
        }
        private string GetPickPointInfo(int id)
        {
            return "Получаем информацию о пункте доставки";
        }

        public override string RunDelivery(string clientName)
        {
            SendOrderToTransportCompany(clientName, pickPointInfo, Number);
            return Number;
        }
    }

    class ShopDelivery : Delivery
    {
        private int shopID;
        private string shopInfo;
        public ShopDelivery(int orderID, string address) : base(orderID, address)
        {
            Number = GenerateNumber();
            if (_address.StartsWith("__SHOP"))
            {
                if (int.TryParse(address.Substring(7), out shopID))
                {
                    shopInfo = GetShopInfo(shopID);
                }
            }
        }

        private string GetShopInfo(int id)
        {
            return "Получаем информацию о магазине";
        }

        public override string RunDelivery(string clientName)
        {
            // отправляем товар в магазин
            SendOrderToOwnDeliveryService(clientName, shopInfo, Number);
            return Number;
        }

        private void SendOrderToOwnDeliveryService(string clientName, string shopInfo, string Number)
        {
            Console.WriteLine("Работа Интернет-магазина со своей службой доставки");
        }
    }

   


}

