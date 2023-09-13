using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _03DesignPattern
{
    public enum Relationship
    {
        Parent, Child, Sibling 
    }

    public class Person
    {
        public string Name;
        //public DateTime DateOfBirth;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    //low-level
    public class Relationships : IRelationshipBrowser
    {
        private List <(Person, Relationship, Person)> relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
                x => x.Item1.Name == name &&
                x.Item2 == Relationship.Parent
                ).Select(r => r.Item3);
        }

        //public List <(Person, Relationship, Person)> Relations => relations;
    }

    public class _01SOLID_05DependencyInversionPrinciple
    {

        //public _01solid_05dependencyinversionprinciple(relationships relationships)
        //{
        //    var relations = relationships.relations;

        //    foreach (var r in relations.where(
        //        x => x.item1.name == "john" &&
        //        x.item2 == relationship.parent
        //        ))
        //    {
        //        console.writeline($"john has a child called {r.item3.name}");
        //    }
        //}

        public _01SOLID_05DependencyInversionPrinciple(IRelationshipBrowser browser)
        {
            foreach(var p in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"john has a child called {p.Name}");
            }
        }
        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();

            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new _01SOLID_05DependencyInversionPrinciple(relationships);        }
        }

}
