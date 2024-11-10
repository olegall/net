using System;
using System.Collections.Generic;
using System.Linq;
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
        // асинхронный метод, ничего не возвращает. поток выделяется при входе? смысл такого метода? когда не нужен результат?
        public static async void f()
        {
             await h();
        }

        public static async Task g() // можно без static
        {
             await h();
        }

        public static async Task h() // если void - в любом случае исключение
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
            var dt1 = Now;
            var tasks = new List<Task> { Task.Delay(4000), Task.Delay(5000) };// см. Status
            //var a1 = tasks.Select(x => x); var a2 = tasks.Select(x => x).Select(x => x); // a1, a2 = tasks
            //var a3 = tasks.Select(x => x.Wait()); // нельзя, т.к. Wait возвращает void, а делегат в Select - Func - возвращает значение. await нельзя - только у метода, Wait() у задачи
            //var a4 = tasks.Select(x => x.GetAwaiter());
            // tasks.Select(x => x.GetAwaiter()); // можно не присваивать, хоть Select и возвращает
            var dt2 = Now; // dt2 - dt1 бывает пара секунд при каких-то обстоятельствах

            //// если запустить все 3 цикла - выполняются параллельно
            //foreach (var task in tasks) { task.Wait(); } // время выполнения foreach - время выполнения задачи с максимальной задержкой
            //tasks.ForEach(x => x.Wait());
            //tasks.Select(x => { x.Wait(); return 0; }).ToArray(); // без ToArray() не выполнится - отложенное выполнение, паузы не будет
            ////var a5 = tasks.Select(x => { x.Wait(); return 0; }); a5.ToArray();
            //var dt3 = Now;

            //Now = Now; // встать на брейкпоинт, периодически наводить мышь - значение меняется
        }

        // TODO сделать синхронное выполнение задач Task.Delay(4000), Task.Delay(5000)
        void TasksSync1()
        {
            var tasks = new List<Task> { Task.Delay(4000), Task.Delay(5000) };
            // google linked tasks in list, list of tasks. tasks in list are linked
            var st_ = tasks.Select(x => x.Status);
            tasks[0].Wait(); // почему-то меняется статус следующей задачи. если долго стоять на брейкпоинте - паузы нет - оптимизация компилятора?
            var st = tasks.Select(x => x.Status);
            var dt4 = Now;
            tasks[1].Wait(); // паузы нет, возможно компилятор делает оптимизацию. пауза = разница задержек задач
            // возможно такую синхронизацию сделать невозможно
            //Now = Now; // встать на брейкпоинт, периодически наводить мышь - значение меняется
        }

        string dt1;
        void TasksSync2()
        {
            var tasks = new List<Task> { Task.Delay(2000), Task.Delay(3000) };
            //tasks[0].Wait(); // c Wait ContinueWith сработает мгновенно
            dt1 = Now;
            tasks[0].ContinueWith(x => {
                var st = tasks.Select(x => x.Status);
                var dt2 = Now;
                tasks[1].Wait();
                var dt3 = Now; // синхронизация некорректная. оптимизация компилятора?
                return x; // потом сработает брейкпоинт2 (поток задачи)
            });
            var dt3 = Now;
        } // сначала сработает брейкпоинт1 (главный поток)
        
        string dt1_3;
        string dt2_3;
        void TasksSync3()
        {
            var tasks = new List<Task> { Task.Delay(2000), Task.Delay(3000) };
            dt1_3 = Now;
            tasks[0].ContinueWith(x => {
                var st = tasks.Select(x => x.Status);
                dt2_3 = Now;
                tasks[1].ContinueWith(x => {
                    var dt3 = Now; // синхронизация некорректная. оптимизация компилятора?
                    return x; 
                });
                return x;
            });
            var dt3 = Now;
        }

        string dt1_4;
        string dt2_4;
        public void TasksSync4()
        {
            var t1 = Task.Delay(5000);
            var t2 = Task.Delay(3000);
            //var t1 = Task.Run(() => Task.Delay(2000));
            //var t1 = Task.Run(() => Thread.Sleep(2000));
            var tasks = new List<Task> { t1, t2 };

            dt1_4 = Now;
            //t1.ContinueWith(x => t2).ContinueWith(x => // x => t1/t2 всё равно
            //{
            //    dt2_4 = Now; // задержка только по 1-й задаче
            //    return x; // Id != Id исходных задач
            //});
            t1.ContinueWith(x => 
            {
                t2.ContinueWith(x =>
                {
                    dt2_4 = Now;
                    return x; // задержка только по более длительной задаче
                });
            });
        }
            
        //Now = Now; // встать на брейкпоинт, периодически наводить мышь - значение меняется

        string Now { get { return DateTime.Now.Second + ":" + DateTime.Now.Millisecond; } set { } }

        void TaskRun()
        {
            var t0 = new Task(() => { 
            });
            t0.Start(); // без Start не попадёт в коллбэк
            
            var t = Task.Run(() => {
            }); // попадёт в коллбэк. Start под капотом
            t.Wait(); // зачем?

            var t1 = new Task(TaskCallback);
            
            Action value = () => { };
            var value2 = () => { }; // см. var
            var t2 = new Task(value);

            var t3 = new Task(delegate () { });
        }
        
        void TaskRun2()
        {
            var t0 = new Task(() => { 
            });
            t0.Start(); // без Start не попадёт в коллбэк
            
            var t = Task.Run(() => {
            }); // попадёт в коллбэк. Start под капотом

            var t1 = new Task(TaskCallback);
            
            Action value = () => { };
            var value2 = () => { }; // см. var
            var t2 = new Task(value);

            var t3 = new Task(delegate(){});

            var t4 = new Task(() => { 
                Thread.Sleep(1000);
            }); // попадёт в коллбэк через паузу Thread.Sleep
            Thread.Sleep(5000);
            t4.Start();
            t4.Wait();
            t4.Dispose();
            Task.Delay(3000).Wait();
        }

        void TaskCallback() { }

        void Delay1() // эвейтить нельзя, задержки нет. смысла в таком методе нет
        {
            Task.Delay(5000);
        }

        async Task Delay2()
        {
            Task.Delay(5000); // await уберёт предупреждение
        }

        async Task AsyncTask() // просто async или просто task нельзя
        {
            //await Task.Run(() => { }); // если убрать - предупреждение
            //Thread.Sleep(3000); // будет пауза await/без await в вызывающем коде

            // await в вызывающем коде - пауза. он нужен - хоть и здесь await, но метод возвращает задачу
            //await Task.Run(() => { Thread.Sleep(3000); });  // без await

            // await/без await в вызывающем коде - паузы нет. тк здесь результат задачи не возвращается - ждать в вызывающем коде нечего
            Task.Run(() => { Thread.Sleep(3000); });

            //var a1 = await Task.Run(() => { Thread.Sleep(3000); }); // нельзя присвоить - await ничего не возвращает
        }

        async void AsyncVoid()
        {
            //await Task.Run(() => { }); // если убрать - предупреждение
            // await/без await в вызывающем коде - пауза. смысла от async нет, эквивалентно async void AsyncVoid(){} по идее слово async компилятор должен запретить
            Thread.Sleep(3000); // будет блокировка? пауза будет тк не возвращает задачу
        }

        void PrintName(string name)
        {
            Thread.Sleep(3000);     // имитация продолжительной работы
            //Console.WriteLine(name); // зависает
        }

        // определение асинхронного метода
        async Task PrintNameAsync(string name)
        {
            await Task.Delay(3000);     // имитация продолжительной работы
            Console.WriteLine(name); // зависает
        }

        async Task AsyncTask_() { }// был бы await - выполнялся бы асинхронно
        Task Task_() { return null; }// выполняется синхронно - просто возвращает задачу

        public async Task Main1()
        {
            #region
            //var dt1 = DateTime.Now;
            //var a1 = await f1();
            //var dt2 = DateTime.Now;
            //var a2 = await f2();
            //var dt3 = DateTime.Now;
            //var dtResult = $"{dt1.Second} {dt2.Second} {dt3.Second}";
            //var result1 = a1 + a2;
            #endregion

            #region
            //var dt4 = DateTime.Now;
            //var t1 = Task.Run(() => f1());
            //var t2 = Task.Run(() => f2());
            //await Task.WhenAll(t1, t2);
            //var dt5 = DateTime.Now;
            //var dtResult2 = $"{dt4.Second}:{dt4.Millisecond} {dt5.Second}:{dt5.Millisecond}";

            //var t3 = Task.Run(() => f3());
            //await Task.WhenAny(t1, t2, t3);
            //var result2 = t1.Result + t2.Result;
            #endregion

            #region
            //Foo1();
            //Foo2();
            //await Delay2(); // если внутри await нет - задержки нет
            //TasksSync1();
            //TasksSync2();
            //TasksSync3();
            //TasksSync4();
            //TaskRun();
            //TaskRun2();
            //AsyncTask();
            AsyncVoid();
            #endregion

            #region
            //PrintName("Tom");
            //PrintName("Bob");
            //PrintName("Sam"); // 9 сек

            await PrintNameAsync("Tom");
            await PrintNameAsync("Bob");
            await PrintNameAsync("Sam"); // 9 сек

            //var tomTask = PrintNameAsync("Tom"); // точка останова в конце метода сработает, когда await в вызывающем коде
            //var bobTask = PrintNameAsync("Bob");
            //var samTask = PrintNameAsync("Sam");

            //await tomTask;
            //await bobTask;
            //await samTask; // 3 сек

            var a1 = ThreadPool.ThreadCount;

            //f(); // void - нет подчёркивания
            //g(); // без await исключения нет. оно возникнет в потоке асинхронной задачи и не передастся в вызывающий поток, тк нет await
            #endregion

            #region
            var a2 = AsyncTask_();
            Task_(); // не дождёмся выполнения задачи. улетит в потоке из ThreadPool, результата не будет. код бессмысленный
            // пока поток ждёт результата - он может заняться чем-то другим (ответить на вызов API)(неблокирующий вызов). если бы Task_ был синхронным - все дальнейшие работы - после завершения Task_
            await Task_();
            var a3 = Task_(); // просто перехватили задачу. результата нет
            using var a4 = Task_(); // так всегда делать?
            // разные ошибки. Task_ не типизирован - возвращает void. await - не часть сигнатуры, а только маркер ожидания
            //var a4 = await Task_();
            //object a5 = await Task_();
            #endregion
        }
    }
}
