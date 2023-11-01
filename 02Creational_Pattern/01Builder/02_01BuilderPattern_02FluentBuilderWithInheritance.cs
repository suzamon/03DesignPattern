using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _03DesignPattern._02_01BuilderPattern_02FluentBuilderWithInheritance;
using static System.Console;

namespace _03DesignPattern
{
    public class _02_01BuilderPattern_02FluentBuilderWithInheritance
    {
        public class Person
        {
            public string Name;

            public string Position;
            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }

            public DateTime DateOfBirth;

            //Builder 생성 시 <SELF> 를 추가해서 객체 생성 불가
            //Class 안에 별도 Builer 클래스 추가해서 구현
            public class Builder : PersonBirthDateBuilder<Builder>
            {
                internal Builder() { }
            }

            public static Builder New => new Builder();

        }

        public abstract class PersonBuilder
        {
            protected Person person = new Person();

            public Person Build()
            {
                return person;
            }
        }

        //SELF -> 자기 참조 & Where을 사용해서 자기참조 제한하는 조건 추가
        public class PersonInfoBuilder<SELF> : PersonBuilder
            where SELF: PersonInfoBuilder<SELF>
        {
            public SELF Called(string name)
            {
                person.Name = name;
                return (SELF)this;
            }
        }

        public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
          where SELF : PersonJobBuilder<SELF>
        {
            public SELF WorksAsA(string position)
            {
                person.Position = position;
                return (SELF)this;
            }
        }

        // here's another inheritance level
        // note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

        public class PersonBirthDateBuilder<SELF>
          : PersonJobBuilder<PersonBirthDateBuilder<SELF>>
          where SELF : PersonBirthDateBuilder<SELF>
        {
            public SELF Born(DateTime dateOfBirth)
            {
                person.DateOfBirth = dateOfBirth;
                return (SELF)this;
            }
        }

        class SomeBuilder : PersonBirthDateBuilder<SomeBuilder>
        {

        }

        public static void Main(string[] args)
        {
            var me = Person.New
              .Called("Dmitri")
              .WorksAsA("Quant")
              .Born(DateTime.UtcNow)
              .Build();

            Console.WriteLine(me);


            var you = Person.New
                .Called("Sumin")
                .Build();

            Console.WriteLine(you);
        }
    }
}
