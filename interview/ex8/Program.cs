#region
// TODO как использовать на практике? Построение архитектур
// TODO virtual и new ведут себя одинаково - нужен кейс, чтобы показать различия
// #new #virtual #override
#endregion
#region
// Так же
Base bd1 = new Derived1(); bd1.Foo(); // Derived
Base bd2 = new Derived2(); bd2.Foo(); // Base. new ломает иерархию
Base bd3 = new Derived3(); bd3.Foo(); // Base. ошибки нет, хотя у Derived3 нет Foo
Base bd4 = new Derived4(); bd4.Foo(); // Base - вариантов нет, только Foo в иерархии наследования
//b4.Foo2(); // ошибка
#endregion
#region использовать это, остальное сложно (выпилить)
//Derived1 dd1 = new Derived1(); dd1.Foo();  // Derived
//Derived2 dd2 = new Derived2(); dd2.Foo();  // Base TODO почему? как сделать Derived?
//Derived3 dd3 = new Derived3(); dd3.Foo();  // Base
//Derived4 dd4 = new Derived4(); dd4.Foo2(); // Derived - вариантов нет, только Foo2 в иерархии наследования
//var d1_var = new Derived1(); // TODO компилятор в design time типизирует var типом создаваемого объекта. статическая типизация? 
Console.WriteLine();
#endregion
#region
//Derived1 db1 = (Derived1)new Base(); db1.Foo();  // в каком случае будет ошибка в рантайме?
//Derived2 db2 = (Derived2)new Base(); db2.Foo();
//Derived3 db3 = (Derived3)new Base(); db3.Foo();
//Derived4 db4 = (Derived4)new Base(); db4.Foo2();
Console.WriteLine();
#endregion

Console.ReadLine();

class Base { public virtual void Foo() => Console.WriteLine("Base"); }

class Derived1 : Base { public override void Foo() => Console.WriteLine("Derived"); }
/// <summary>
/// TODO без public - так же. в любом случае base, new ломает иерархию
/// без new результат тот же, просто предупреждение
/// </summary>
class Derived2 : Base { public new void Foo() => Console.WriteLine("Derived"); } // new vs override https://stackoverflow.com/questions/1399127/difference-between-new-and-override

class Derived3 : Base { }

class Derived4 : Base { public new void Foo2() => Console.WriteLine("Derived"); }     
