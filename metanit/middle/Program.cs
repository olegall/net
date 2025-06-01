using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace q7
{
    // вопрос 7
    public class A
    {
        public virtual void Print1()
        {
            Console.Write("A");
        }
        public void Print2()
        {
            Console.Write("A");
        }
    }
    public class B : A
    {
        public override void Print1()
        {
            Console.Write("B");
        }
    }
    public class C : B
    {
        new public void Print2()
        {
            Console.Write("C");
        }
    }
}


namespace ConsoleApplication1
{
    // Вопрос 1
    class A
    {
        public virtual void Foo()
        {
            Console.Write("Class A");
        }
    }
    class B : A
    {
        public override void Foo()
        {
            Console.Write("Class B");
        }
    }


    class Program
    {
        // вопрос 7
        private static Object syncObject = new Object();
        private static void Write()
        {
            //lock (syncObject)
            //{
            //    lock (new Object())
            //    {
            //        lock (new Object())
            //        {
            //            Console.WriteLine("test");
            //        }
            //    }
            //}
            lock (syncObject)
            {
                Console.WriteLine("test");
            }
        }

        // вопрос 8
        static IEnumerable<int> Square(IEnumerable<int> a)
        {
            foreach (var r in a)
            {
                Console.WriteLine(r * r);
                yield return r * r;
            }
        }
        class Wrap
        {
            private static int init = 0;
            public int Value
            {
                get { return ++init; }
            }
        }

        private static void Work()
        {
            Thread.Sleep(1000);
        }

        // вопрос 14
        class MyCustomException : DivideByZeroException
        {

        }

        private static void Calc()
        {
            int result = 0;
            var x = 5;
            int y = 0;
            try
            {
                result = x / y;
            }
            catch (MyCustomException e) // 1. сюда не попадёт, т.к. MyCustomException наследник DivideByZeroException
            {
                Console.WriteLine("Catch DivideByZeroException");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch Exception");
            }
            finally
            {
                throw new MyCustomException(); // отрабатает MyCustomException
            }
        }

        public struct S : IDisposable
        {
            private bool dispose;
            public void Dispose()
            {
                dispose = true;
            }
            public bool GetDispose()
            {
                return dispose;
            }

            // задача 5

        }

        private enum En : byte
        {
            First = 15,
            Second,
            Third = 54
        }

        private class Test
        {
            public void Print()
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception)
                {
                    Console.Write("9");
                    throw new Exception();
                }
                finally
                {
                    Console.Write("2");
                }
            }
        }

        static String location;
        static DateTime time;

        static void Main(string[] args)
        {
            Console.WriteLine("-------- вопрос 1 ---------");
            // Что выведут на консоль такие вызовы метода Foo():
            // B не видит A, т.к. он наследник - ошибка
            //B obj1 = new A();
            //obj1.Foo();

            // Так нет ошибки - A видит всех наследников (сейчас это B) 
            A obj1 = new B();

            B obj2 = new B();
            obj2.Foo(); // класса B

            A obj3 = new B(); // позднее связывание
            obj3.Foo(); // класса B

            Console.WriteLine("-------- вопрос 2 ---------");
            var s = new S();
            using (s)
            {
                var a = s.GetDispose();
            }
            //var b = s.GetDispose();

            //new Foo().foo();

            Console.WriteLine("-------- вопрос 3 ---------");
            // Статья "Как замкнуть переменную в C# и не выстрелить себе в ногу"

            List<Action> actions = new List<Action>();
            for (var count1 = 0; count1 < 10; count1++)
            {
                actions.Add(() => Console.WriteLine(count1));
            }
            foreach (var action in actions)
            {
                action();
            }

            //List<Func<int, string>> actions = new List<Func<int, string>>();
            //for (var count1 = 0; count1 < 10; count1++)
            //{
            //    actions.Add((int a) => { return a.ToString(); });
            //    //actions.Add((int a) => a.ToString());
            //}
            //foreach (var action in actions)
            //{
            //    action(1);
            //    //action.Invoke(1);
            //}

            Console.WriteLine("-------- вопрос 4 ---------");
            int i1 = 1;
            object obj = i1;
            ++i1;
            Console.WriteLine(i1);
            Console.WriteLine(obj);
            //Console.WriteLine((short)obj);


            Console.WriteLine("-------- вопрос 5 ---------");
            var s1 = string.Format("{0}{1}", "abc", "cba");
            var s2 = "abc" + "cba";
            var s3 = "abccba";

            var _5_1 = s1 == s2;
            var _5_2 = (object)s1 == (object)s2;
            var _5_3 = s2 == s3;
            var _5_4 = (object)s2 == (object)s3;
            Console.WriteLine(s1 == s2);
            Console.WriteLine((object)s1 == (object)s2);
            Console.WriteLine(s2 == s3);
            Console.WriteLine((object)s2 == (object)s3);

            /*
             если две строки имеют одинаковый набор символов и создаются во время компиляции, 
             то они фактически указывают на один и тот же объект. строка s1 создается в процессе выполнения, 
             поэтому она будет указывать на другой объект, отличный от s2 и s3. 
             А так как при сравнении объектов сравниваются ссылки, 
             то поэтому мы получим false, так как s1 и s2 разные объекты.
             */

            Console.WriteLine("-------- вопрос 6 ---------");
            lock (syncObject)
            {
                Write();
            }

            Console.WriteLine("-------- вопрос 7 ---------");
            // К какому результату приведет выполнение следующего кода:

            var c = new q7.C();
            q7.A a1 = c;
            var aa = a1.GetType();// почему разная вложенность?
            var bb = c.GetType();

            a1.Print2();
            a1.Print1();
            c.Print2();
            a1.Print2(); // почему объект A?
            // Варианты ответов:
            // ABC
            // CCC
            // ACC
            // AAC

            Console.WriteLine("-------- вопрос 8 ---------");
            var w = new Wrap();
            var wraps = new Wrap[3];
            for (int i = 0; i < wraps.Length; i++)
            {
                wraps[i] = w;
            }

            var values = wraps.Select(x => x.Value);
            var results = Square(values);
            int sum = 0;
            int count = 0;
            foreach (var r in results)
            {
                count++;
                sum += r;
            }
            Console.WriteLine("Count {0}", count);
            Console.WriteLine("Sum {0}", sum);

            Console.WriteLine("Count {0}", results.Count());
            Console.WriteLine("Sum {0}", results.Sum());


            Console.WriteLine("-------- вопрос 13 ---------");
            object sync = new object();
            var thread = new Thread(() =>
            {
                try
                {
                    Work();
                }
                finally
                {
                    lock (sync)
                    {
                        Monitor.PulseAll(sync);
                    }
                }
            });
            thread.Start();
            lock (sync) // тут только главный поток. lock заблокирует Monitor.Wait(sync); только для него
            {
                Monitor.Wait(sync);
            }
            Console.WriteLine("test");
            // Monitor.PulseAll(sync); освобождает поток thread от работы с методом Work
            // и уведомляет другие потоки, что они могут работать с методом Work

            Console.WriteLine("-------- вопрос 14 ---------");

            try
            {
                Calc();
            }
            catch (MyCustomException e)
            {
                Console.WriteLine("Catch MyCustomException");
                //throw;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Catch Exception");
            }

            Console.WriteLine("-------- вопрос 53 ---------");
            Console.WriteLine((int)En.Second);

            Console.WriteLine("-------- вопрос 55 ---------");
            int c1 = 3;
            int c2 = 0;
            Console.Write(Sum(5, 3, out c1, out c2) + "_");
            Console.Write(c1);

            Console.WriteLine("-------- вопрос 56 ---------");
            //var a = null;
            //a = 10;
            //Console.WriteLine(a);

            Console.WriteLine("-------- вопрос 57 ---------");
            try
            {
                var array = new int[] { 1, 2 };
                Console.Write(array[5]);
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(1);
            }
            catch (SystemException e)
            {
                Console.WriteLine(2);
            }
            catch (Exception e)
            {
                Console.WriteLine(3);
            }

            Console.WriteLine("-------- вопрос 58 ---------");
            var test = new Test();
            try
            {
                test.Print();
            }
            catch (Exception)
            {
                Console.WriteLine("5");
            }
            finally
            {
                Console.WriteLine("4");
            }

            Console.WriteLine("--- Какое значение присвоено x, если приведенный ниже код выводит False? ---");
            float x1 = float.NaN;
            Console.WriteLine(x1 == x1);


            Console.WriteLine("--- А почему следующий код выводит False? ---");
            Test t = new Test();
            Console.WriteLine(t.Equals(t));
            // в Test так переопределен Equals(),
            // Здесь ссылочный тип, а значит сравниваться будут ссылки на объекты, а в этой ситуации (боюсь ошибиться) они будут различны.


            Console.WriteLine("--- Что будет выведено на экран при выполнении приведенного ниже кода? ---");
            char a2 = 'a';
            int b = 0;
            Console.WriteLine(true ? a2 : b);
            /*
            Приведение типов будет, т.е.на экране будет код символа ‘a’. 
            Во - первых, компилятор уберет b, т.к.выражение тут всегда верное, 
            а значит будет типа такого: WriteLine(a)… но т.к.была переменная с типом int, 
            то будет приведение(int)а, т.к.если я не ошибаюсь, то к типу int компилятор 
            в таких ситуацию приводит почти все если может. А char он привести может.
            */

            Console.WriteLine("--- А в этом случае, что будет на экране? ---");
            //NameValueCollection col = new NameValueCollection();
            //Console.WriteLine("Элемент test " + col["test"] != null ? "Существует!" : "Не существует!");

            /*
             Всегда будет "Существует". Тут проблема с приоритетами операций – 
             сначала посчитается значение выражения ("Элемент test " + col["test"]), 
             которое, разумеется, никогда не будет null. Потом оно сравнится с null 
             (результат – всегда false). 
             И уже потом этот результат используется в операторе ?: в качестве условия. 
             */

            Console.WriteLine("--- Что следует ожидать на экране? ---");
            Console.WriteLine("A" + "B" + "C");
            Console.WriteLine('A' + 'B' + 'C'); // 198 - сумма ASCII кодов этих символов
            Console.WriteLine((int)'A'); // код одного символа

            Console.WriteLine("--- Циклическая инициализация полей? Интересненько, а в результате что будет на консоли выведено?--- ");
            Console.WriteLine("A.x = " + A1.x);
            Console.WriteLine("B.y = " + B1.y);

            /*
            Попробую определить логику компилятора:
            Итак, нужно вывести A.x , компилятор пытается вычислить A.x по формуле B.y+1; но B.y равен A.x+1, 
            т.к A.x на данный момент не вычеслен, а по умолчанию переменные класса типа int если они не заданы
            инициализируются 0 , значит формула A.x+1 превращается в 0+1 тем самым B.y=1, далее формула B.y+1 
            превращается в 1+1 тем самым A.x=2; А вот дальше не понятно так как A.x=2 
            то формула A.x+1 должна вернуть 3 , но y-ку присваиваится почему то 1, 
            видимо потому что компилятор думает что A.x всё ещё не вычеслен и по умолчанию инициализирован  0-ём.             
             */

            Console.WriteLine("--- Инкремент, инкремент, а что же будет? ---");
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                j = j++;
            }
            Console.WriteLine(j);

            Console.WriteLine("--- Какой же метод выберет компилятор? ---");
            B2 b2 = new B2();
            b2.Test(5);

            Console.WriteLine("--- А в этом случае? ---");
            Test2 t2 = new Test2(null);

            Console.WriteLine("--- Что будут выведено на экран в результате выполнения кода приведенного ниже? ---");
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> all = list.FindAll(
                i => { Console.WriteLine(i); return i < 3; }
            );

            Console.WriteLine("--- А такого кода? ---");
            List<int> list2 = new List<int>() { 1, 2, 2 };
            var x2 = list2.GroupBy(i => { Console.WriteLine(i); return i; });
            var y = list2.ToLookup(i => { Console.WriteLine(i); return i; });

            Console.WriteLine("--- И наконец, сложный вопрос из трех частей. Что будет выведено на экран в каждом из трех случаев, приведенных ниже: ---");

            //try
            //{
            //    Console.WriteLine("Hello ");
            //    return;
            //}
            //finally { Console.WriteLine("Goodbye "); // отработает
            //    Console.ReadLine();
            //}

            //try
            //{
            //    Console.WriteLine("Hello ");
            //    //Thread.CurrentThread.Abort();   // ошибка
            //}
            //finally { Console.WriteLine("Goodbye ");
            //    Console.ReadLine();
            //}

            //try
            //{
            //    Console.WriteLine("Hello ");
            //    System.Environment.Exit(0);
            //}   
            //finally // не отработает
            //{
            //    Console.WriteLine("Goodbye ");
            //    Console.ReadLine();
            //}

            Console.WriteLine("--- Напишите программу для сложения всех чётных чисел в массиве ---");
            var evenNumbers = TotalAllEvenNumbers(new int[] { 1, 2, 4, 3 });
            var evenNumbers2 = TotalAllEvenNumbers2(new int[] { 1, 2, 4, 3 });


            Console.WriteLine("--- Каков будет результат короткой программы ниже? ---");

            Console.WriteLine(location == null ? "location is null" : location);
            Console.WriteLine(time == null ? "time is null" : time.ToString());
            // Обе переменные неинициализированы, но String со ссылочным типом, а DateTime — с типом значения.
            // В качестве типа значения для DateTime устанавливается значение по умолчанию в полночь 1 / 1 / 1, а не null.
            Console.WriteLine("--- Является ли сравнение time и null в выражении if валидным? Почему? ---");

            if (time == null)
            {
                /* do something */
            }
            var a3 = time == null;


            Console.WriteLine("--- Напишите код на C# для вычисления окружности, без изменения класса circle ---");
            var radius = 1;
            var okr = new Circle().Calculate(r => 2 * Math.PI * radius);


            Console.WriteLine("--- Какой результат выведет на консоль данная программа? ---");
            var numbers = new int[] { 7, 2, 5, 5, 7, 6, 7 };
            var result = numbers.Sum() + numbers.Skip(2).Take(3).Sum();
            var y1 = numbers.GroupBy(x => x)
                            .Select(x => { result += x.Key; return x.Key; });
            //.Select(x => x);
            Console.WriteLine(result);

            Foo();

            Console.WriteLine("--- Какой результат выведет на консоль данная программа? ---");


            Console.ReadLine();
        }

        private static string GetNumber(int input)
        {
            try
            {
                throw new Exception(input.ToString());
            }
            catch (Exception e)
            {
                throw new Exception((int.Parse(e.Message) + 3).ToString());
            }
            finally
            {
                throw new Exception((++input).ToString());
            }

            return (input += 4).ToString();
        }


        static int Foo()
        {
            try
            {
                //throw new Exception();
            }
            catch
            {
                //throw new Exception();
            }
            finally
            {
                //throw new Exception(); // без этой строки сработает return. 
                // Если throw new Exception() в try и catch - return не сработает. 
                // Если throw new Exception() только в try - return не сработает.
            }
            return 1;
        }

        public sealed class Circle
        {
            private double radius;

            public double Calculate(Func<double, double> op)
            {
                return op(radius);
            }
        }

        static long TotalAllEvenNumbers(int[] intArray)
        {
            return intArray.Where(i => i % 2 == 0).Sum(i => (long)i);
        }

        static long TotalAllEvenNumbers2(int[] intArray)
        {
            return (from i in intArray where i % 2 == 0 select (long)i).Sum();
        }


        // вопрос 55
        static int Sum(int a, int b, out int c, out int c2)
        {
            c = 1;
            c2 = 2;
            return a + b;
        }



    }

    public class Test2
    {
        public Test2(object obj) { Console.WriteLine("object"); }
        public Test2(int[] obj) { Console.WriteLine("int[]"); }
    }

    class A2 { public void Test(int n) { Console.WriteLine("A"); } }
    class B2 : A2 { public void Test(double n) { Console.WriteLine("B"); } }

    public class A1 { public static int x = B1.y + 1; }
    public class B1 { public static int y = A1.x + 1; }
    //class Foo 
    //{ 
    //    public void foo()
    //    {
    //    }
    //}
    //struct foo2 { }

    //class A
    //{
    //    public virtual void Foo()
    //    {
    //        Console.Write("Class A");
    //    }
    //}
    //class B : A
    //{
    //    public override void Foo()
    //    {
    //        Console.Write("Class B");
    //    }
    //}





}
