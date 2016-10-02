using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.NET.Mapper;

namespace Toolkit.NET
{
    public class Program
    {
        public static void Main(string[] paArgs)
        {
            Mapper.Mapper.Instance.CreateContract<Car, Bus>(
                new MapperSpectialContract<Car, Bus>(
                    nameof(Car.Name),
                    nameof(Bus.Name),
                    (car1, bus1) =>
                    {
                        bus1.Name = car1.Name + " bus type";
                    }));

            var car = new Car()
            {
                Name = "Toyota",
                Color = "Red",
                NameOfDriver = "John",
                Wheels = 4.ToString()
            };
            var bus = new Bus();

            Console.WriteLine("=========================================");
            Console.WriteLine(car);
            Console.WriteLine(bus);

            Console.WriteLine("=========================================");
            Console.WriteLine("Mapping objects");
            Mapper.Mapper.Instance.Map(ref car, ref bus);
            Console.WriteLine("=========================================");
            Console.WriteLine(car);
            Console.WriteLine(bus);
            Console.ReadKey();
        }
    }

    internal class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Wheels { get; set; }
        public string NameOfDriver { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Color: {Color}, Wheels: {Wheels}, NameOfDriver: {NameOfDriver}";
        }
    }

    internal class Bus
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Wheels { get; set; }
        public string NameOfDriver { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Color: {Color}, Wheels: {Wheels}, NameOfDriver: {NameOfDriver}";
        }
    }
}
