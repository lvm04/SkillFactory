using System;

namespace SF.Module7
{
    public abstract class Person
    {
        protected int Id { get; set; }
        protected string _name;
        public string Name 
        {
            get 
            {
                return _name;
            }
            
            set
            {
                if (string.IsNullOrEmpty(value))
                    _name = "none";
                else
                    _name = value;
            }
        }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Person()
        {
            
        }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Person(int id, string name, string address, string phone) : this(id, name)
        {
            Address = address;
            Phone = phone;
        }

        public virtual void Display()
        {
            Console.WriteLine("ID        : {0}", Id);
            Console.WriteLine("Имя       : {0}", Name);
            Console.WriteLine("Адрес     : {0}", Address);
            Console.WriteLine("Телефон   : {0}", Phone);
        }
    }

}