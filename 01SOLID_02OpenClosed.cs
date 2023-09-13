using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _03DesignPattern
{

    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color; 
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if(name == null) throw new ArgumentNullException(paramName: nameof(name));

            Name = name;
            Color = color;
            Size = size;    
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach(var p in products)
            {
                if(p.Size == size) yield return p;
            }
        }
        //Open-Closed Principle을 지키지 않으면 하나씩 메소드 추가해야한다
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color) yield return p;
            }
        }

        //추가적인 예시로 필터의 경우 And 조건은 모든 조합을 고려해서 하나씩 추가해야 한다...!
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
            {
                if (p.Size == size && p.Color == color) yield return p;
            }
        }
    }

    //Open-Closed Principle은 상속과 확장을 이용하는 방법 -> 추상 클래스와 인터페이스 사용!
    //
    //1) 먼저 조건과 필터에 대한 인터페이스 작성
    //2) 이후 Color, Size에 대한 조건은 1)에서 작성한 조건 인터페이스를 상속해서 구체 클래스 구현
    //3) 필터에 대한 구체 클래스 구현
    //4) 추가로 And 조건을 넣을 경우 추상 인터페이스를 매개 인자로 넣음으로써 2)에서 생성한 클래스들이 모두 매개인자로 들어올 수 있게끔 구현 가능
    //5) 필터 조건으로 추가할 경우에도 추상 인터페이스를 매개 인자로 넣음으로써 별도 클래스나 메소드 구현할 필요가 없다!
    //
    //즉, 개방-확장에는 열려있고, 폐쇄-변경은 어렵게 클래스를 구현하는 것이 Open-Closed Principle이다....!
    public interface ISpecification<T>
    {        
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color== color;          
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
           return t.Size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first?? throw new ArgumentNullException(paramName:nameof(first));
            this.second = second?? throw new ArgumentNullException(paramName: nameof(first));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }


    public class BetterFilter:IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach(var i in items)
            {
                if(spec.IsSatisfied(i)) yield return i;
            }
        }
    }

    public class _01SOLID_02OpenClosed
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = {apple, tree, house};

            var pf = new ProductFilter();
            WriteLine("Green products (old): ");
            foreach(Product p in pf.FilterByColor(products, Color.Green))
            {
                WriteLine($" - {p.Name} is green");
            }

            WriteLine();
            var bf = new BetterFilter();
            WriteLine("Green products (new): ");
            foreach(var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($" - {p.Name} is green");
            }

            WriteLine("Large blue products (new): ");
            foreach (var p in bf.Filter(products, 
                new AndSpecification<Product>(new ColorSpecification(Color.Blue), 
                new SizeSpecification(Size.Large))))
            {
                WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}
