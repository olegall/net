namespace C__NET5;

public class Foo
{
    public int X { get; set; }
}

public class Foo1
{
    public int X { get; set; }

    public override bool Equals(object obj)
    {
        return this.X == ((Foo2)obj).X;
    }
}

public class Foo2
{
    public int X { get; set; }
}


//using System;

//namespace C__NET5
//{
//    public class Foo
//    {
//        public int X { get; set; }
//    }

//    public class Foo2
//    {
//    }

//    public static class Object_
//    {
//        // метод расширения Object, сравнивающий объекты по значению
//        static bool Equals(this object obj, object o1, object o2)
//        {
//            return ((Foo)o1).X == ((Foo)o2).X;
//        }

//        // перекрыть (override) Equals, чтобы сравнить по значению (изначальный - по ссылке)

//        public static void Main_() // если Main - проект не запускается. перекликается с Main Program.cs
//        {
//            var a1 = new object().Equals(new Foo { X = 1 }, new Foo { X = 1 });
//            var a2 = new object().Equals(new Foo { X = 1 }, new Foo { X = 2 });
//        }
//    }

//    public class Object2
//    {
//        public bool Equals(Object? obj) { return true; }
//        // TODO new vs override? оба перекрывают. различаются по присваиванию объектов в иерархии классов
//        //public new bool Equals(Object? obj) { return true; }ncz 
//        //public override bool Equals(Object? obj) { return true; }
//    }
//}