#region
var a = new A(); a.Foo(); // A
a = new B(); a.Foo(); // B
a = new C(); a.Foo(); // B последний по стеку оверриднутый метод. new не переопределяет. был бы в C override - он вызвался бы
var b = new B(); b = new C(); b.Foo(); // B последний по стеку оверриднутый метод. new не переопределяет
//a = new D(); a.Foo(); // B
var c = new D(); c.Foo(); // D
//new B().Foo(); // B очевидно
//new C().Foo(); // C очевидно
//new A() = new B(); // нельзя

class A
{
    public virtual void Foo()
    {
    }
}

class B : A
{
    public override void Foo()
    {
    }
}

class C : B // ломает иерархию. теперь начальный - этот метод. методы выше [наследниками, объявленные классами выше, и инстанцированные объектами на иерархии ниже] этого класса никогда не будут вызваны
{
    public new void Foo()
    //public override void Foo()
    //public new virtual void Foo()
    {
    }
}

class D : C
{
    //public override void Foo()
    //{
    //}
}
#endregion

#region
//var a = new C(); a.Foo(); // B ближайший по иерархии

//class A
//{
//    public virtual void Foo()
//    {
//    }
//}

//class B : A
//{
//    public override void Foo()
//    {
//    }
//}

//class C : B // ломает иерархию. теперь начальный - этот метод. методы выше [наследниками, объявленные классами выше, и инстанцированные объектами на иерархии ниже] этого класса никогда не будут вызваны
//{
//}
#endregion