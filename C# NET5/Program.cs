using C__NET5.async_await;
using C__NET5.keywords;
using C__NET5.сsharpcorner_сom;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using static C__NET5.VirtualOverride;

namespace C__NET5
{
    internal class Program
    {
        #region by reference
        class Contact
        {
            public int X { get; set; }
        }

        static void SetContact(Contact contact)
        {
            contact.X = 1;
        }
        #endregion

        #region async await
        private static string str = "initial";
        static AsyncAwait asyncAwait = new AsyncAwait();
        static Common common = new Common();
        static Cast cast = new Cast();

        async static Task<string> Delay()
        {
            Thread.Sleep(5000);
            //for (long i = 0; i < 10000000000; i++) i++; // delay 7000

            str = "after sleep";
            return "default";
        }

        static string Delay2()
        {
            Thread.Sleep(5000);
            str = "after sleep";
            return "default";
        }

        static Task Delay3_()
        {
            Task.Delay(5000); // паузы не будет. следующая строка тут же сработает
            str = "after sleep";
            return Task.CompletedTask;
        }

        static async Task Delay3()
        {
            await Task.Delay(5000); // будет пауза, если в вызывающем коде - await
            str = "after sleep";
        }
        
        static async Task Delay3NoAwait()
        {
            Task.Delay(5000); // паузы не будет, т.к. нет await следующая строка тут же сработает
            str = "after sleep";
        }

        static Task Delay4()
        {
            for (long i = 0; i < 10000000000; i++) i++; // delay 7000
            str = "after sleep";
            return Task.CompletedTask;
        }

        static async Task Delay5()
        {
            await Task.Delay(5000); // паузы не будет
        }
        
        static Task DelayNoAsyncInSignature()
        {
            Task.Delay(5000); // паузы не будет. await нельзя
            return Task.CompletedTask;
        }

        // habr
        static async Task ExecuteOperation() // задача внутри задачи?
        {
            Console.WriteLine($"Before: {Thread.CurrentThread.ManagedThreadId}"); // ManagedThreadId не меняется
            await Task.Run(() =>
            {
                Console.WriteLine($"Inside before sleep: {Thread.CurrentThread.ManagedThreadId}"); // ManagedThreadId меняется
                Thread.Sleep(1000);
                Console.WriteLine($"Inside after sleep: {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"After: {Thread.CurrentThread.ManagedThreadId}");
        }
        
        static async Task ExecuteOperationNoAwait()
        {
            Console.WriteLine($"Before: {Thread.CurrentThread.ManagedThreadId}");
            Task.Run(() => // без await результат другой. хоть в вызывающем методе await - Task.Run ... не дожидается выполнения
            {
                Console.WriteLine($"Inside before sleep: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Console.WriteLine($"Inside after sleep: {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"After: {Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task ExecuteOperationTaskDelay()
        {
            Console.WriteLine($"Before: {Thread.CurrentThread.ManagedThreadId}"); // ManagedThreadId не меняется
            await Task.Run(() =>
            {
                Console.WriteLine($"Inside before sleep: {Thread.CurrentThread.ManagedThreadId}"); // ManagedThreadId меняется
                Task.Delay(5000); // паузы нет
                Console.WriteLine($"Inside after sleep: {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"After: {Thread.CurrentThread.ManagedThreadId}");
        }
        
        static async Task ExecuteOperationTaskDelayAsyncLambda()
        {
            Console.WriteLine($"Before: {Thread.CurrentThread.ManagedThreadId}"); // ManagedThreadId не меняется
            await Task.Run(async () => // можно без async. тогда надо убрать await
            {
                Console.WriteLine($"Inside before sleep: {Thread.CurrentThread.ManagedThreadId}"); // ManagedThreadId меняется
                await Task.Delay(5000); // можно без await. тогда паузы не будет
                Console.WriteLine($"Inside after sleep: {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"After: {Thread.CurrentThread.ManagedThreadId}");
        }
        #endregion

        static async Task Main(string[] args)
        {
            var ref_ = new Ref();
            #region
            var foo = new Ref.Foo();
            ref_.Bar(foo);
            ref_.Bar2(foo);
            ref_.Bar3(foo); // Name не меняется

            foo = new Ref.Foo();
            ref_.Bar(foo);
            ref_.BarRef2(ref foo);
            ref_.BarRef3(ref foo);
            #endregion
            ref_.Run();

            #region by reference
            var contact = new Contact();
            SetContact(contact);
            #endregion

            #region async await
            //await ExecuteOperation();
            //ExecuteOperation(); // выполняется меньше кода
            //await ExecuteOperationNoAwait(); // выполняется больше кода. по любому строки вне Task.Run
            //ExecuteOperationNoAwait(); // выполняются синхронные строки Before, After, хоть и не дожидаемся ничего
            //await ExecuteOperationTaskDelay();
            //await ExecuteOperationTaskDelayAsyncLambda();
            //var breakpoint = 0;
            //Delay5();
            //DelayNoAsyncInSignature();

            //asyncAwait.AsyncVoidExceptions_CannotBeCaughtByCatch();
            //asyncAwait.AsyncVoidExceptions();
            //AsyncAwait.f(); // неудачный пример
            //AsyncAwait.g();

            // Delay3();
            //await Delay3();
            //await Delay3_();

            // результат идентичный
            //Delay4();
            //Delay4().Wait();
            //await Delay4();

            //Delay2();

            //Task.Run(() => Delay2());
            //var t = Task.Run(() => Delay2()); t.Wait();
            //await Task.Run(() => Delay2());
            //await asyncAwait.Main1();

            //AsyncAndAwaitInCSharp.Main1();

            //Test t = new Test();
            //try
            //{
            //    //t.Call();
            //    t.Call();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            var results = new List<int>();
            for (int i = 0; i < 30; i++)
            {
                var result = ConcurrencyInCsharpCookBook.ParallelSum(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }); // 15
                results.Add(result);
            }
            //var resultsArr = results.ToArray().ToString();
            #endregion

            #region virtual
            double r = 3.0, h = 5.0;
            Shape c = new Circle(r);
            Shape s = new Sphere(r);
            Shape l = new Cylinder(r, h);
            // Display results:
            var a1 = c.Area();
            var a2 = s.Area();
            var a3 = l.Area();
            #endregion

            #region virtual override
            var a1_vo = new A1();
            var b1_vo = new B1();
            var c1_vo = new C1();
            #endregion

            new Exceptions();

            #region common
            common.Run();
            common.Run3();
            #endregion

            OOP.Main_();
            Finalize_.Demo.Main1();
            cast.Run();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
            //SafeHandle
        }
    }
}
