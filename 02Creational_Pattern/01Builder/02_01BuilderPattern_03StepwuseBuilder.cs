using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace _03DesignPattern
{
    public enum CarType
    {
        Sedan,
        Crossover
    };
    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        Car Build();
    }

    public class CarBuilder
    {
        public static ISpecifyCarType Create()
        {
            return new Impl();
        }

        private class Impl :
            ISpecifyCarType,
            ISpecifyWheelSize,
            IBuildCar
        {
            private Car car = new Car();

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
                }
                car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                return car;
            }
        }
    }
 
        public class _02_01BuilderPattern_03StepwuseBuilder
        {

        public static void Main(string[] args)
        {

            //순서대로 인터페이스 반환, 정해진 순서에 맞게끔 해야한다 -> StepwiseBuilder
            var car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheels(18)
                .Build();
            
            Console.WriteLine(car);

        }
    }
}
