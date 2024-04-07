using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C__NET5
{
    #region learn.microsoft.com
    public static class DeadlockDemo
    {
        private static async Task DelayAsync()
        {
            await Task.Delay(1000);
        }
        // This method causes a deadlock when called in a GUI or ASP.NET context.
        public static void Test()
        {
            // Start the delay.
            var delayTask = DelayAsync();
            // Wait for the delay to complete.
            delayTask.Wait();
        }
    }
    
    public static class NotFullyAsynchronousDemo
    {
        // This method synchronously blocks a thread.
        public static async Task TestNotFullyAsync()
        {
            await Task.Yield();
            Thread.Sleep(5000);
        }
    }
    #endregion

    internal class AsyncAwait
    {
        #region learn.microsoft.com
        private async void ThrowExceptionAsync()
        {
            throw new InvalidOperationException();
        }

        public void AsyncVoidExceptions_CannotBeCaughtByCatch()
        {
            try
            {
                ThrowExceptionAsync();
            }
            catch (Exception)
            {
                // The exception is never caught here!
                throw;
            }
        }
        #endregion

        private async Task ThrowExceptionAsyncTaskInSignature()
        {
            throw new InvalidOperationException();
        }

        public async Task AsyncVoidExceptions()
        {
            try
            {
                await ThrowExceptionAsyncTaskInSignature();
            }
            catch (Exception)
            {   // будет исключение
                throw;
            }
        }
        
        #region stackoverflow
        public static async void f()
        {
            await h();
        }

        public static async Task g()
        {
            await h();
        }

        public static async Task h()
        {
            throw new NotImplementedException();
        }
        #endregion

        async Task<int> f1()
        {
            await Task.Delay(5000);
            //Thread.Sleep(5000);
            return 1;
            //return await Task.FromResult(1);
        }

        async Task<int> f2()
        {
            await Task.Delay(5000);
            //Thread.Sleep(5000);
            return 2;
        }
        
        async Task<int> f3()
        {
            await Task.Delay(1000);
            return 3;
        }

        void Foo1() 
        {
            var tasks = new List<Task>  // Просто объявили задачи? Не запустили?
            {
                Task.Delay(2000),
                Task.Delay(5000)
            };
            //tasks.ForEach
        }
        
        void Foo2()
        {
            Task.Delay(2000);
            Task.Delay(5000);
        }

        async Task Foo3()
        {
            Task.Delay(2000); 
            Task.Delay(5000);
        }
        
        // Отличия
        async Task InnerAsyncFunction() { /* ... */ }
        async void InnerAsyncFunctionVoid() { /* ... */ }

        public async Task Main1()
        {
            //var dt1 = DateTime.Now;
            //var a1 = await f1();
            //var dt2 = DateTime.Now;
            //var a2 = await f2();
            //var dt3 = DateTime.Now;
            //var dtResult = $"{dt1.Second} {dt2.Second} {dt3.Second}";
            //var result1 = a1 + a2;
            
            #region example1
            var dt4 = DateTime.Now;
            var t1 = Task.Run(() => f1());
            var t2 = Task.Run(() => f2());
            await Task.WhenAll(t1, t2);
            var dt5 = DateTime.Now;
            var dtResult2 = $"{dt4.Second}:{dt4.Millisecond} {dt5.Second}:{dt5.Millisecond}";

            var t3 = Task.Run(() => f3());
            await Task.WhenAny(t1, t2, t3);
            var result2 = t1.Result + t2.Result;
            #endregion

            #region example2
            //Foo1();
            Foo2();
            Foo3();
            #endregion

            var a1 = ThreadPool.ThreadCount;


        }
    }
}
