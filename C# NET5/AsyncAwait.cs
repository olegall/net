using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
