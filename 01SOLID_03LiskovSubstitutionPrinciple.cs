using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _03DesignPattern
{
    public class Rectangle
    {
        //프로퍼티 혹은 메소드를 virtual 키워드를 사용해서 선언
        //-> 이후 상속한 자식 클래스에서 override 키워드를 사용해서 재정의를 지원하게끔 구현
        //=> 이 방법으로 리스코프 교체 원칙을 지원하게 한다!
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public Rectangle()
        {

        }
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";

        }
    }

    public class Square : Rectangle
    {
        public override int Width {
            set { base.Width = base.Height = value; }
        }
        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    public class _01SOLID_03LiskovSubstitutionPrinciple
    {
        static int Area(Rectangle r) => r.Width* r.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2,3);
            WriteLine($"{rc} has area {Area(rc)}");

            //리스코프 교체 원리를 지키면 아래와 같이 부모 클래스로 선언된 값을 하위 클래스 생성자로 선언 가능하다
            Rectangle sq = new Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {Area(sq)}");


        }
    }
}
