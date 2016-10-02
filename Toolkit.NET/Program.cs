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
            // frist create contract between specified types such as Car and Bus
            Mapper.Mapper.Instance.CreateContract<Car, Bus>(
                new MapperSpectialContract<Car, Bus>( // if you need you can specify special contract between custom properties
                    nameof(Car.Name), // Let's say that we have Car with property Name
                    nameof(Bus.Name), // and we have Bus with the same name but we want to create special behavior with mapping
                    (car1, bus1) => // after specifying two properties you must specify what must happen with them
                    {
                        bus1.Name = car1.Name + " bus type"; // we want to append to car name ' bus type' to see that bus got our special value on Name property
                    }));

            // now create new instance of car filled with some values
            var car = new Car()
            {
                Name = "Toyota",
                Color = "Red",
                NameOfDriver = "John",
                Wheels = 4.ToString()
            };

            // now we want to map this values to bus with the same properties but with different value types
            var bus = new Bus();

            Console.WriteLine("=========================================");
            Console.WriteLine(car); // first we will see data in car
            Console.WriteLine(bus); // then we will see data in empty bus

            Console.WriteLine("=========================================");
            Console.WriteLine("Mapping objects");
            Mapper.Mapper.Instance.Map(ref car, ref bus); // now we will mapp data from existing instance of car to existing instance of bus
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
