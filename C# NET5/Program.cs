using System;
using System.Threading;
using System.Threading.Tasks;

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
            Task.Delay(5000); // паузы не будет. следующая строка тут же сработает
            str = "after sleep";
        }

        static Task Delay4()
        {
            for (long i = 0; i < 10000000000; i++) i++; // delay 7000
            str = "after sleep";
            return Task.CompletedTask;
        }

        static async Task DelayAsyncInSignature()
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
            //DelayAsyncInSignature();
            //DelayNoAsyncInSignature();

            //asyncAwait.AsyncVoidExceptions_CannotBeCaughtByCatch();
            //asyncAwait.AsyncVoidExceptions();
            //AsyncAwait.f(); // неудачный пример
            //AsyncAwait.g();

            //Delay3();
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

            #endregion

            //Console.WriteLine(str);
        }
    }
}
