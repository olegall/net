using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static C__NET5.OOP;

namespace C__NET5
{
    //protected class A1 { }
    //private class A1 { }
    //class A1 { protected class A2 { } }
    class A3 { }
    public class A4 { }
    // модификаторы для делегатов, событий, свойств

    internal class OOP
    {
        class InheritanceCasting
        {
            class A { }
            
            class B : A { }
            
            public InheritanceCasting()
            {
                var a = (A)new B(); // upcasting - design time ok - runtime ok. B видит родителя А. B в курсе про A, потому что он его наследник
                //var b = (B)new A(); // downcasting - design time ok - runtime error. A пытается стать B. A не знает о потомках. Unable to cast object of type 'A' to type 'B'
                //a = b;
                //b = a;
                //(B)new A(); // без присваивания нельзя
            }
        }

        public interface InterfaceA {}
        
        public interface InterfaceB {}

        public class MyClass2 {}

        class Person
        {
            protected string a1 { get; set; }
            public virtual void CalculatePay()
            {
            }
        }

        class Manager : Person
        {
            public override void CalculatePay() // причина override - не в base
            {
                var a1 = base.a1 + " ";
            }
            
            public void CalculatePay2()
            {
                var a1 = base.a1 + " ";
            }
        }

        //public class MyClass : InterfaceA, MyClass2
        public class MyClass : MyClass2, InterfaceA, InterfaceB
        {
            public void Main()
            {
            }
        }

        interface IFoo { }
        class A1 : IFoo { }
        class A2 : IFoo { }

        public static void Main_()
        {
            Person personManager = new Manager();
            personManager.CalculatePay(); //Manager.CalculatePay()
            ((Manager)personManager).CalculatePay(); //Manager.CalculatePay()
            ((Person)personManager).CalculatePay(); //Manager.CalculatePay()

            Person person = new Person();
            person.CalculatePay();

            Manager manager = new Manager();
            manager.CalculatePay();

            Array a1 = new String[0]; // инициализируется, т.к. реализуют 1 и тот же интерфейс(ы)?
            Array a2 = new[] { "a" };
            Array a3 = new[] { 1 };
            //Array a4 = new String();
            //Array a5 = new Object(); // может  быть неизвестно какой объект. д.б. совместимость типов

            //A1 a6 = new A2(); // несмотря на то, что оба класса реализуют 1 и тот же интерфейс, между ними нет связи. надо наследовать один от другого
            //A1 a7 = (IFoo)(new A2());

            new InheritanceCasting();
        }
    }
}
