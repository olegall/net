namespace C__NET5;

//protected class A1 { }
//private class A1 { }
class A1 { protected class A2 { } }
// модификаторы для делегатов, событий, свойств

internal class OOP
{
    class InheritanceCasting
    {
        class B { }
        
        class D : B { }
        
        public InheritanceCasting()
        {
            var a = (B)new D(); // upcasting - design time ok - runtime ok. от частного к общему В - подмножество D [B видит родителя А. B в курсе про A, потому что он его наследник]
            var b = (D)new B(); // downcasting - design time ok - runtime error. от общего к частному. B НЕ подмножество D. [A пытается стать B. A не знает о потомках. [Unable to cast object of type 'A' to type 'B']]
            //a = b;
            //b = a;
            //(B)new D(); // без присваивания нельзя
        }
    }

    public interface InterfaceA {}
    public interface InterfaceB {}
    public class MyClass2 {}
    //public class MyClass : InterfaceA, MyClass2
    public class MyClass : MyClass2, InterfaceA, InterfaceB
    {
    }

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

    public static void Main_()
    {
        #region не смотреть, всё очевидно
        Person personManager = new Manager();
        personManager.CalculatePay(); //Manager.CalculatePay()
        ((Manager)personManager).CalculatePay(); //Manager.CalculatePay()
        ((Person)personManager).CalculatePay(); //Manager.CalculatePay()

        Person person = new Person();
        person.CalculatePay();

        Manager manager = new Manager();
        manager.CalculatePay();
        #endregion

        //Array a5 = new Object(); // может  быть неизвестно какой объект. д.б. совместимость типов. наоборот можно

        //A1 a6 = new A2(); // несмотря на то, что оба класса реализуют 1 и тот же интерфейс, между ними нет связи. надо наследовать один от другого
        //A1 a7 = (IFoo)(new A2());

        new InheritanceCasting();
    }
}
