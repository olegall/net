namespace ConsoleApp1
{
    partial class Program
    {
        #region перегрузки см. вызывающий код
        public static void Foo(object a) => Console.WriteLine("object"); // TODO public везде из-за partial

        public static void Foo(object a, object b) => Console.WriteLine("object, object");

        public static void Foo(params object[] args) => Console.WriteLine("params object[]");

        public static void Foo<T>(params T[] args) => Console.WriteLine("params T[]");
        
        public class Bar { }
        #endregion

        public class Foo2
        {
            public virtual void Quux(int a)
            {
                Console.WriteLine("Foo.Quux(int)");
            }
        }

        public class Bar2 : Foo2
        {
            public override void Quux(int a)
            {
                Console.WriteLine("Bar.Quux(int)");
            }

            public void Quux(object a)
            {
                Console.WriteLine("Bar.Quux(object)");
            }
        }

        public class Baz : Bar2
        {
            public override void Quux(int a)
            {
                Console.WriteLine("Baz.Quux(int)");
            }

            public void Quux<T>(params T[] a)
            {
                Console.WriteLine("Baz.Quux(params T[])");
            }
        }



        public class Foo3
        {
            public Foo3()
            {
                Quux();
            }

            public virtual void Quux()
            {
                Console.WriteLine("Foo.Quux()");
            }
        }

        public class Bar3 : Foo3
        {
            protected string name;

            public Bar3()
            {
                name = "Bar";
            }

            public override void Quux()
            {
                Console.WriteLine("Bar.Quux(), " + name);
            }

            public void Quux(params object[] args) // 2 Quux. одинаковую сигнатуру нельзя
            {
                Console.WriteLine("Bar.Quux(params object[])");
            }
        }

        public class Baz3 : Bar3
        {
            int a1;
            public Baz3()
            {
                name = "Baz";
                Quux(); // TODO п1 вызовется Quux(params object[] args), если бы его не было - Quux(). почему?
                ((Foo3)this).Quux(); // this - текущий экземпляр Baz3. в дебаге показывает a1 и унаследованный name (как будто его)
            }
        }



        public class Base4
        {
            //class Quux // предупреждение, ошибка. internal - c# default class access modifier
            protected class Quux
            {
                public Quux()
                {
                    Console.WriteLine("Foo.Quux()");
                }
            }
        }

        public class Derived4 : Base4 // TODO OOP
        {
            // override class Quux override для класса нельзя, а new можно (для метода и override, и new можно)
            // class Quux предупреждение есть, protected class Quux предупреждения нет. связано с приватностью
            //protected /*new*/ class Quux // protected new vs protected
            new class Quux
            {
                public Quux()
                {
                    Console.WriteLine("Bar.Quux()");
                }
            }
        }

        public class Main4 : Derived4
        {
            public Main4()
            {
                // Derived4.Quux private/internal(без мод-ра) -> Base4.Quux() - игнорируется в иерархии
                // Derived4.Quux public/protected -> Base4.Quux() - ближайший доступный в иерархии
                // Derived4.Quux protected new -> Base4.Quux() - ближайший доступный в иерархии, хоть и new
                // можно не запускать - в дизайн тайме переходит куда надо по F12 по new Quux() здесь
                new Quux();
            }
        }



        public static string GetString(string s)
        {
            //Console.WriteLine("GetString: " + s);
            return s;
        }

        public static IEnumerable<string> GetStringEnumerable()
        {
            yield return GetString("Foo");
            yield return GetString("Bar");
        }

        public static string[] EnumerableToArray()
        {
            var strings = GetStringEnumerable();

            foreach (var s in strings)
                Console.WriteLine("EnumerableToArray: " + s);

            return strings.ToArray();
        }

        public static IEnumerable<string> Foo6() // класс называть так нельзя
        {
            yield return "Bar";
            Console.WriteLine("Baz");
        }

        public static int Inc(int x)
        {
            //Console.WriteLine("Inc: " + x);
            return x + 1;
        }

        public static IEnumerable<int> GetSmallNumbers()
        {
            yield return 1;
            throw new Exception();
            yield return 2;
        }

        public static IEnumerable<int> GetSmallNumbers2()
        {
            try
            {
                yield return 1;
                yield return 2;
            }
            finally
            {
                Console.WriteLine("Foo");
            }
        }

        public static Action<int, int> print = (a, b) => Console.WriteLine("{0,2} = {1,2} * {2,3} + {3,3} ", a, b, a / b, a % b);

        public struct Foo7
        {
            int value;
            public override string ToString()
            {
                if (value == 2)
                    return "Baz";
                return (value++ == 0) ? "Foo" : "Bar";
            }
        }


        public struct Struct1
        //public class Struct1
        {
            public int value;
            public void Change(int newValue)
            {
                value = newValue;
            }
        }

        public class Bar5
        {
            public Struct1 Struct1 { get; set; } // 0 для структуры / 5 для класса
            //public Struct1 Struct1; // всегда 5 для структуры / класса
        }

        public struct Foo9
        {
            public byte Byte1;
            public int Int1;
        }
        public struct Bar6
        {
            public byte Byte1;
            public byte Byte2;
            public byte Byte3;
            public byte Byte4;
            public int Int1;
        }

        [ThreadStatic]
        public static readonly int Foo10 = 42;
    }
}