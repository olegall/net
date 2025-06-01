// TODO как использовать на практике? Построение архитектур
// TODO virtual и new ведут себя одинаково - нужен кейс, чтобы показать различия
#region
Derived1 dd1 = new Derived1();
var d1_var = new Derived1(); // тип Derived - это понятно
Derived2 dd2 = new Derived2();
Derived3 dd3 = new Derived3();
Derived4 dd4 = new Derived4();

//Derived1 db1 = (Derived1)new Base(); // в каком случае будет ошибка в рантайме?
//Derived2 db2 = (Derived2)new Base();
//Derived3 db3 = (Derived3)new Base();
//Derived4 db4 = (Derived4)new Base();

// Так же
Base bd1 = new Derived1();
Base bd2 = new Derived2();
Base bd3 = new Derived3();
Base bd4 = new Derived4();
#endregion

dd1.Foo();  // Derived
dd2.Foo();  // Base TODO почему? как сделать Derived?
dd3.Foo();  // Base
dd4.Foo2(); // Derived - вариантов нет, только Foo2 в иерархии наследования
Console.WriteLine();
//db1.Foo();  // 
//db2.Foo();  // 
//db3.Foo();  // 
//db4.Foo2(); // 
Console.WriteLine();
bd1.Foo(); // Derived
bd2.Foo(); // Base
bd3.Foo(); // Base
bd4.Foo(); // Base - вариантов нет, только Foo в иерархии наследования
//b4.Foo2(); // ошибка
Console.ReadLine();

class Base
{
    public virtual void Foo() => Console.WriteLine("Base");
}

class Derived1 : Base
{
    public override void Foo() => Console.WriteLine("Derived");
}

class Derived2 : Base
{
    // без public - base, этот метод не находит, но есть выше в иерархии наследования
    // без new результат тот же, просто предупреждение
    /*public */new void Foo() => Console.WriteLine("Derived"); 
}

class Derived3 : Base
{
}

class Derived4 : Base
{
    public new void Foo2() => Console.WriteLine("Derived");
}          
