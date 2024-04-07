using System;
using System.Threading;
using System.Threading.Tasks;

namespace C__NET5.async_await
{
    internal class AsyncAndAwaitInCSharp_Csharcorner
    {
        //public static async Task Main1()
        public static async void Main1()
        {
            //for (int i = 0; i < 5; i++) {
                Method1();
                Method2();
                //new Thread(Method1).Start();
                //new Thread(Method2).Start();
                Console.WriteLine();
            //}
            Console.ReadKey();
        }

        //public static void Method1()
        public static async Task Method1()
        {
            await Task.Run(() =>
            //Task.Run(() =>
            //Task.Run(async () =>
            {
                //for (int i = 0; i < 100; i++)
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Method 1");
                    // Do something
                    //Task.Delay(1000).Wait();
                    /*await*/ Task.Delay(1000); // вносит асинхрон, задержки нет
                }
            });
        }
        public static void Method2()
        //public static async Task Method2()
        {
            //for (int i = 0; i < 25; i++)
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Method 2");
                // Do something
                //Task.Delay(1000).Wait();
                Task.Delay(1000); // вносит асинхрон
            }
        }

        //результат тот же
        //public static async Task Method1()
        //public static void Method1()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Console.WriteLine("Method 1");
        //        //Task.Run(() => Console.WriteLine("Method 1"));
        //        // Do something
        //        //Task.Delay(100).Wait();
        //        //Task.Delay(100);
        //    }
        //}
        //результат тот же
        //public static async Task Method2()
        //public static void Method2()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Console.WriteLine("Method 2");
        //        //Task.Run(() => Console.WriteLine("Method 2"));
        //        // Do something
        //        //Task.Delay(100).Wait();
        //        //Task.Delay(100);
        //    }
        //}

        //public static void Method1()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Console.WriteLine("Method 1");
        //        // Do something
        //        Task.Delay(100); // закомментировать - результат тот же. микс синхрона и асинхрона - bad practice
        //    }
        //}
        //public static void Method2()
        //{
        //    //for (int i = 0; i < 25; i++)
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Console.WriteLine("Method 2");
        //        // Do something
        //        Task.Delay(100);
        //    }
        //}
    }
}