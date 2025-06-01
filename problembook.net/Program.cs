using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{



    class Program
    {

        static void Foo(object a)
        {
            Console.WriteLine("object");
        }
        static void Foo(object a, object b)
        {
            Console.WriteLine("object, object");
        }
        static void Foo(params object[] args)
        {
            Console.WriteLine("params object[]");
        }
        static void Foo<T>(params T[] args)
        {
            Console.WriteLine("params T[]");
        }
        class Bar { }



        class Foo2
        {
            public virtual void Quux(int a)
            {
                Console.WriteLine("Foo.Quux(int)");
            }
        }
        class Bar2 : Foo2
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
        class Baz : Bar2
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

        class Foo3
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
        class Bar3 : Foo3
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
            public void Quux(params object[] args)
            {
                Console.WriteLine("Bar.Quux(params object[])");
            }
        }
        class Baz3 : Bar3
        {
            public Baz3()
            {
                name = "Baz";
                Quux();
                ((Foo3)this).Quux();
            }
        }



        class Foo4
        {
            protected class Quux
            {
                public Quux()
                {
                    Console.WriteLine("Foo.Quux()");
                }
            }
        }
        class Bar4 : Foo4
        {
            new class Quux
            {
                public Quux()
                {
                    Console.WriteLine("Bar.Quux()");
                }
            }
        }
        class Baz4 : Bar4
        {
            public Baz4()
            {
                new Quux();
            }
        }

        class Foo5<T>
        {
            public static int Bar;
        }


        public static string GetString(string s)
        {
            Console.WriteLine("GetString: " + s);
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

        static IEnumerable<string> Foo6()
        {
            yield return "Bar";
            Console.WriteLine("Baz");
        }

        static int Inc(int x)
        {
            Console.WriteLine("Inc: " + x);
            return x /*+ 1*/;
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

        static Action<int, int> print = (a, b) => Console.WriteLine("{0,2} = {1,2} * {2,3} + {3,3} ", a, b, a / b, a % b);

        struct Foo7
        {
            int value;
            public override string ToString()
            {
                if (value == 2)
                    return "Baz";
                return (value++ == 0) ? "Foo" : "Bar";
            }
        }

        public struct Foo8
        {
            public int Value;
            public void Change(int newValue)
            {
                Value = newValue;
            }
        }
        public class Bar5
        {
            public Foo8 Foo8 { get; set; }
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
        static readonly int Foo10 = 42;

        static void Main(string[] args)
        {
            var list_ = new ConcurrentBag<string>();
            string[] dirNames = { ".", ".." };
            List<Task> tasks = new List<Task>();
            foreach (var dirName in dirNames)
            {
                Task t = new Task(() => {
                    foreach (var path in Directory.GetFiles(dirName))
                        list_.Add(path);
                });
                tasks.Add(t);
                t.Start();
            }
            Task.WaitAll(tasks.ToArray());
            foreach (Task t in tasks)
                Console.WriteLine("Task {0} Status: {1}", t.Id, t.Status);

            Console.WriteLine("Number of files read: {0}", list_.Count);
            Console.WriteLine("****************************************");


            Console.WriteLine("|||||| ООП ||||||");
            Console.WriteLine("--- 1. Что выведет следующий код? ---");
            Foo();
            Foo(null);
            Foo(new Bar());
            Foo(new Bar(), new Bar());
            Foo(new Bar(), new object());

            Console.WriteLine("--- 2. Что выведет следующий код? ---");
            new Bar2().Quux(42);
            new Baz().Quux(42);

            //Bar2 a = new Bar2();
            //Baz b = new Baz();
            //a.Quux(42);
            //b.Quux(42);
            Console.WriteLine("--- 3. Что выведет следующий код? ---");
            new Baz3();

            Console.WriteLine("--- 4. Что выведет следующий код? ---");
            new Baz4();

            Console.WriteLine("--- 5. Что выведет следующий код? ---");
            Foo5<int>.Bar++;
            Console.WriteLine(Foo5<double>.Bar);

            Console.WriteLine("|||||| LINQ ||||||");
            Console.WriteLine("--- 1. Что выведет следующий код? ---");
            EnumerableToArray();

            Console.WriteLine("--- 2. Что выведет следующий код? ---");
            foreach (var str in Foo6())
                Console.Write(str);

            Console.WriteLine("--- 5. Что выведет следующий код? ---");
            var list = new List<string> { "Foo", "Bar", "Baz" };
            var query = list.Where(c => c.StartsWith("B"));
            list.Remove("Bar");
            Console.WriteLine(query.Count());

            Console.WriteLine("--- 6. Что выведет следующий код? ---");
            var numbers = Enumerable.Range(0, 10);
            var query2 =
                (from number in numbers
                 let number2 = Inc(number)
                 where number2 % 2 == 0
                 select number2)/*.Take(2)*/;
            //var query2 =
            //    (from number in numbers
            //     where number % 2 == 0
            //     select number);
            foreach (var number in query2)
                Console.WriteLine("Number: " + number);


            Console.WriteLine("--- 6. В какой момент произойдёт Exception? ---");
            var numbers2 = GetSmallNumbers();
            var evenNumbers = numbers2.Select(n => n * 2);
            Console.WriteLine(evenNumbers.FirstOrDefault());

            Console.WriteLine("--- 7. Что выведет следующий код? ---");
            Console.WriteLine(GetSmallNumbers2().First());

            Console.WriteLine("|||||| Математика ||||||");
            Console.WriteLine("1. Что выведет следующий код?");
            Console.WriteLine(
            "| Number | Round | Floor | Ceiling | Truncate | Format |");
            foreach (var x in new[] { -2.9, -0.5, 0.3, 1.5, 2.5, 2.9 })
            {
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "| {0,6} | {1,5} | {2,5} | {3,7} | {4,8} | {0,6:N0} |", x, Math.Round(x), Math.Floor(x), Math.Ceiling(x), Math.Truncate(x)));
            }

            Console.WriteLine("2. Что выведет следующий код?");
            Console.WriteLine(" a = b * (a/b) + (a%b)");
            print(7, 3);
            print(7, -3);
            print(-7, 3);
            print(-7, -3);

            Console.WriteLine("3. Что выведет следующий код?");
            Console.WriteLine("0.1 + 0.2 {0} 0.3", 0.1 + 0.2 == 0.3 ? "==" : "!=");

            Console.WriteLine("4. Что выведет следующий код?");
            var zero = 0;
            try
            {
                Console.WriteLine(42 / 0.0);
                Console.WriteLine(42.0 / 0);
                Console.WriteLine(42 / zero);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("DivideByZeroException");
            }

            Console.WriteLine("5. Что выведет следующий код?");
            int ten = 10;
            Console.WriteLine(unchecked(2147483647 + ten));
            var maxInt32 = Int32.MaxValue;
            var maxDouble = Double.MaxValue;
            var maxDecimal = Decimal.MaxValue;
            var maxByte = Byte.MaxValue;
            checked
            {
                Console.Write("Checked Int32 increased max: ");
                try { Console.WriteLine(maxInt32 + 42); }
                catch { Console.WriteLine("OverflowException"); }
                Console.Write("Checked Double increased max: ");
                try { Console.WriteLine(maxDouble + 42); }
                catch { Console.WriteLine("OverflowException"); }
                Console.Write("Checked Decimal increased max: ");
                try { Console.WriteLine(maxDecimal + 42); }
                catch { Console.WriteLine("OverflowException"); }
            }
            unchecked
            {
                Console.Write("Unchecked Int32 increased max: ");
                try { Console.WriteLine(maxInt32 + 42); }
                catch { Console.WriteLine("OverflowException"); }
                Console.Write("Unchecked Double increased max: ");
                try { Console.WriteLine(maxDouble + 42); }
                catch { Console.WriteLine("OverflowException"); }
                Console.Write("Unchecked Decimal increased max: ");
                try { Console.WriteLine(maxDecimal + 42); }
                catch { Console.WriteLine("OverflowException"); }
            }

            Console.WriteLine("7. Что выведет следующий код?");
            byte foo = 1;
            dynamic bar = foo;
            Console.WriteLine(bar.GetType());
            bar += foo;
            Console.WriteLine(bar.GetType());


            Console.WriteLine("|||||| Значимые типы ||||||");
            Console.WriteLine("1. Что выведет следующий код?");
            var foo7 = new Foo7();
            Console.WriteLine(foo7);
            Console.WriteLine(foo7);
            object bar2 = foo7;
            object qux = foo7;
            object baz = bar;
            Console.WriteLine(baz);
            Console.WriteLine(bar2);
            Console.WriteLine(baz);
            Console.WriteLine(qux);

            Console.WriteLine("2. Что выведет следующий код?");
            var bar5 = new Bar5 { Foo8 = new Foo8() };
            bar5.Foo8.Change(5);
            Console.WriteLine(bar5.Foo8.Value);

            Console.WriteLine("3. Что выведет следующий код?");
            var x2 = new
            {
                Items = new List<int> { 1, 2, 3 }.GetEnumerator()
            };

            //while (x2.Items.MoveNext())
            //    Console.WriteLine(x2.Items.Current);

            Console.WriteLine("4. Что выведет следующий код?");
            Console.WriteLine(Marshal.SizeOf(typeof(Foo9)));
            //Console.WriteLine(Marshal.SizeOf(typeof(Bar6)));
            //Console.WriteLine(Marshal.SizeOf(typeof(Int32)));


            Console.WriteLine("|||||| Строки ||||||");
            Console.WriteLine("1. Что выведет следующий код?");
            Console.WriteLine(1 + 2 + "A");
            Console.WriteLine(1 + "A" + 2);
            Console.WriteLine("A" + 1 + 2);

            Console.WriteLine("2. Что выведет следующий код?");
            Console.WriteLine(1 + 2 + 'A');
            Console.WriteLine(1 + 'A' + 2);
            Console.WriteLine('A' + 1 + 2);

            Console.WriteLine("3. Что выведет следующий код?");

            //string aaa = "hello world";
            //string bbb = aaa;
            //aaa = aaa.Replace("h", "a");
            //var aaa2 = aaa;
            //var bbb2 = bbb;

            //string aaa = "hello world";
            //string bbb = "hello world";
            //aaa = null;
            //var aaa2 = aaa == bbb;
            //var bbb2 = (object)aaa == (object)bbb;

            Console.WriteLine("9. Что выведет следующий код?");
            var x3 = "AB";
            var y = new StringBuilder().Append('A').Append('B').ToString();
            var z = string.Intern(y);
            Console.WriteLine(x3 == y);
            Console.WriteLine(x3 == z);
            Console.WriteLine((object)x3 == (object)y);
            Console.WriteLine((object)x3 == (object)z);


            Console.WriteLine("|||||| Многопоточность ||||||");
            Console.WriteLine("1. Что выведет следущий код?");
            var thread = new Thread(() => Console.WriteLine(Foo10));
            thread.Start();
            thread.Join();
            Console.ReadLine();
        }


    }
}
