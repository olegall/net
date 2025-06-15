using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using static ConsoleApp1.Program; // TODO это надо для partial?

namespace problembook.net
{
    partial class Program
    {

        public class Foo5<T>
        {
            public static int Bar; // TODO в другом partial классе в дебаге не видно значение
            //public static T Bar;
        }

        static void Main(string[] args)
        {
            var list_ = new ConcurrentBag<string>();
            string[] dirNames = { ".", ".." };
            List<Task> tasks = new List<Task>();
            foreach (var dirName in dirNames)
            {
                Task t = new Task(() =>
                {
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


            Console.WriteLine("*** ООП ***");
            Console.WriteLine("*** 1. Что выведет следующий код? ***");
            Foo();
            Foo(null);
            Foo(new Bar());
            Foo(new Bar(), new Bar());
            Foo(new Bar(), new object());

            Console.WriteLine("*** 2. Что выведет следующий код? ***");
            new Bar2().Quux(42);
            new Baz().Quux(42);

            //Bar2 a = new Bar2();
            //Baz b = new Baz();
            //a.Quux(42);
            //b.Quux(42);
            Console.WriteLine("*** 3. Что выведет следующий код? ***");
            new Baz3();

            Console.WriteLine("*** 4. Что выведет следующий код? ***");
            new Main4();

            Console.WriteLine("*** 5. Что выведет следующий код? ***");
            Foo5<int>.Bar++;
            Console.WriteLine(Foo5<double>.Bar); // 0 ? TODO
            Console.WriteLine(Foo5<int>.Bar); // 1 ?

            Console.WriteLine("|||||| LINQ ||||||");
            Console.WriteLine("*** 1. Что выведет следующий код? ***");
            EnumerableToArray(); // Foo, Bar

            Console.WriteLine("*** 2. Что выведет следующий код? ***");
            foreach (var str in Foo6()) Console.Write(str); // BarBaz TODO ?

            Console.WriteLine("*** 5. Что выведет следующий код? ***");
            var list = new List<string> { "Foo", "Bar", "Baz" };
            var query = list.Where(c => c.StartsWith("B"));
            list.Remove("Bar");
            Console.WriteLine(query.Count()); // слишком очевидно

            Console.WriteLine("*** 6. Что выведет следующий код? ***"); // TODO п2
            var numbers = Enumerable.Range(0, 10);
            // 0 1 2 3 4 5 6 7 8 9 
            var query2 =
                (from number in numbers
                 let number2 = Inc(number)
                 where number2 % 2 == 0
                 select number2)/*.Take(2)*/;
            // 1 2 3 4 5 6 7 8 9 10
            //var query2 = from number in numbers where number % 2 == 0 select number;
            foreach (var number in query2) Console.WriteLine("Number: " + number);
            // 2 4 6 8 10

            Console.WriteLine("*** 6. В какой момент произойдёт Exception? ***");
            var numbers2 = GetSmallNumbers(); // искл нет
            var evenNumbers = numbers2.Select(n => n * 2); // искл нет
            Console.WriteLine(evenNumbers.First()); // 2 искл нет First() - метод расширения IEnumerable, lazy, искл не фиксируется
            Console.WriteLine(evenNumbers.FirstOrDefault()); // 2 искл нет
            //evenNumbers.ToArray(); // искл

            Console.WriteLine("*** 7. Что выведет следующий код? ***");
            // штатно вернётся коллекция (итеративная часть метода), штатно отработает finally (оставшаяся часть метода)
            Console.WriteLine(GetSmallNumbers2().First()); // Foo 1 - почему такой порядок? TODO

            Console.WriteLine("|||||| Математика ||||||"); // TODO п2
            Console.WriteLine("1. Что выведет следующий код?");
            Console.WriteLine("| Number | Round | Floor | Ceiling | Truncate | Format |");
            foreach (var x in new[] { -2.9, -0.5, 0.3, 1.5, 2.5, 2.9 })
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "| {0,6} | {1,5} | {2,5} | {3,7} | {4,8} | {0,6:N0} |", x, Math.Round(x), Math.Floor(x), Math.Ceiling(x), Math.Truncate(x)));

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
            //object bar = foo;
            Console.WriteLine(bar.GetType()); // System.Byte
            bar += foo;
            Console.WriteLine(bar.GetType()); // System.Int32 из-за +?


            Console.WriteLine("|||||| Значимые типы ||||||");
            Console.WriteLine("1. Что выведет следующий код?");
            var foo7 = new Foo7();
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
