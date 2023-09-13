using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03DesignPattern
{
    public class Document
    {

    }

    //IMachine과 같이 인터페이스에 많은 걸 구현하면 해당 인터페이스를 참조하는 클래스는
    //자신과 관계없는 메소드를 구현해야하거나 영향을 받게된다
    //그래서 인터페이스는 통합이 아니라 개별로 선언하는 것이 인터페이스 분리 원칙이다.
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }
   
    //에러 상황으로 이렇게 IMachine 인터페이스를 상속받지만
    //Print 메소드만 사용하고 Fax나 Scan은 사용하지 못하는데 구현해야한다는 단점이 존재
    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }
    //그래서 통합된 인터페이스보다 분리해서 개별로 선언하는 것이 좋다 -> 인터페이스 분리 법칙
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }
    //많은 인터페이스를 사용하는 경우에는 이렇게 나열하는 방식도 가능하다
    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }
    //혹은 인터페이스가 상속함으로써 더 큰 인터페이스를 선언하는 것도 가능하다
    public interface IMultiFunctionDeivce : IScanner, IPrinter //..
    {

    }

    public class MultiFunctionMachine : IMultiFunctionDeivce
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            if(printer == null) throw new ArgumentNullException(nameof(printer));
            if(scanner == null) throw new ArgumentNullException(nameof(scanner));

            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }//decorator pattern
    }

    internal class _01SOLID_04InterfaceSegregationPrinciple
    {
    }
}
