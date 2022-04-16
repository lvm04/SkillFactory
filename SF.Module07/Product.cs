using System;

namespace SF.Module7
{
    // Товар
    class Product
    {
        public int Id;
        public string Name;
        public decimal UnitPrice;
        public ProductCategory Category;
        public string Description;

        public Product(int id, string name, ProductCategory prodCategory, decimal price, string descr = "")
        {
            Id = id;
            Name = name;
            UnitPrice = price;
            Category = prodCategory;
            Description = descr;
        }

        public void Display()
        {
            Console.WriteLine("ID         : {0}", Id);
            Console.WriteLine("Название   : {0}", Name);
            Console.WriteLine("Категория  : {0}", Category.Name);
            Console.WriteLine("Подробности: {0}", Description);
        }
    }

    class Catalog
    {
        private static Product[] productList;
        private static Catalog _catalog = new Catalog();
        public static Catalog Get
        {
            get { return _catalog; }
        }
        private Catalog()
        {
            productList = new Product[]
            {
                new Product(1, "REALME 8 Pro 6/128Gb", Categories.Get["Смартфон"], 18000, "синий"),
                new Product(2, "HONOR 10X Lite 4/128Gb", Categories.Get["Смартфон"], 25000, "черный"),
                new Product(3, "SAMSUNG Galaxy Tab A SM-T295", Categories.Get["Планшет"], 35000, "2GB, 32GB, 4G черный"),
                new Product(4, "LENOVO Tab M10 Plus TB-X606F", Categories.Get["Планшет"], 27000, "4GB, 128GB серый"),
                new Product(5, "PANASONIC RP-TCM115GC", Categories.Get["Наушники"], 1850, "3.5 мм, вкладыши, белый"),
                new Product(6, "SONY WI-C400", Categories.Get["Наушники"], 2250, "Bluetooth, вкладыши, белый")
            };
        }

        public Product this[int index]
        {
            get
            {
                if (index < 0 || index > productList.Length - 1)
                    return new Product(-1, "Неизвестный товар", null, 0);
                else
                    return productList[index];
            }
        }

        public void Display()
        {
            foreach (var item in productList)
            {
                Console.WriteLine($"[{item.Id}] - {item.Category.Name} {item.Name} ({item.Description})");
            }
        }

    }

}