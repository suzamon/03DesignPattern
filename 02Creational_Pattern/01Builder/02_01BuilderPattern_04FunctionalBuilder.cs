using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace _03DesignPattern
{
    public class Person
    {
        public string Name, Position;
    }

    //sealed 키워드 -> 상속불가를 선언하는 키워드
    public sealed class PersonBuilder
    {
        //Action 대리자 사용 -> Person을 인자로 가지는 메소드
        public readonly List<Action<Person>> Actions
          = new List<Action<Person>>();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    //상속이 안되는 객체에서 Open-Closed Principle 적용 방법
    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAsA
          (this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }

    public static class _02_01BuilderPattern_04FunctionalBuilder
    {
        public static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Dmitri").WorksAsA("Programmer").Build();
        }
    }
}
