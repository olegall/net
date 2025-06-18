using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace C__NET5;

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
/// <summary>
/// task under the hood
/// async await under the hood
/// </summary>
internal class AsyncAwait
{
    #region learn.microsoft.com
    private async void ThrowExceptionAsync() { throw new InvalidOperationException(); } // - catch
    //private void ThrowExceptionAsync() { throw new InvalidOperationException(); } // + catch

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

    #region эквивалентно синхронному коду
    private async Task ThrowExceptionAsyncTaskInSignature() { throw new InvalidOperationException(); } // + catch
    //private async void ThrowExceptionAsyncTaskInSignature() { throw new InvalidOperationException(); } // - catch. придётся убрать await - рассинхрон
    //private async void ThrowExceptionAsyncTaskInSignature() { await Task.Run(() => throw new InvalidOperationException()); } // - catch

    public async Task AsyncVoidExceptions()
    {
        try
        {
             await ThrowExceptionAsyncTaskInSignature();
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region stackoverflow
    public static async void f()
    {
         await h();
    }

    public static async Task g() // можно без static
    {
         await h();
    }

    private static async Task h() // если void - в любом случае исключение
    {
        throw new NotImplementedException();
    }
    #endregion

    async Task<int> f1()
    {
        // эквивалентно, но Thread.Sleep блокирует вызывающий поток
        await Task.Delay(1000);
        //Thread.Sleep(1000);
        
        return 1;
        //return await Task.FromResult(1);
    }

    async Task<int> f2()
    {
        await Task.Delay(2000);
        return 2;
    }
    
    void Foo1()
    {
        var t = Task.Delay(2000);
        var dt1 = Now;
        // 
        var tasks = new List<Task> { Task.Delay(1000), t };// см. Status
        
        // просто преобразуется в IEnumerable. эквивалентно
        //var a1 = tasks.Select(x => x); 
        //var a2 = tasks.Select(x => x).Select(x => x);
        
        //var a3 = tasks.Select(x => x.Wait()); // нельзя, т.к. Wait возвращает void, а делегат в Select - Func - возвращает значение. await нельзя - только у метода, Wait() у задачи
        
        //var a4 = tasks.Select(x => x.GetAwaiter());
        //tasks.Select(x => x.GetAwaiter()); // можно не присваивать, хоть Select и возвращает
        
        //var dt2 = Now; // dt2 - dt1 бывает пара секунд при каких-то обстоятельствах

        // 2 цикла - выполняются параллельно, т.к. каждый из них выполняется асинхронно. эквивалентно
        //foreach (var task in tasks) { task.Wait(); } // самая долгая. сами итерации foreach асинхронны
        //tasks.ForEach(x => x.Wait()); // самая долгая. сами итерации foreach асинхронны

        // какого типа возвращает return - так типизирован IEnumerable Select-а. 
        // без ToArray() пауз нет - отложенное выполнение
        tasks.Select(x => { x.Wait(); return 0; }).ToArray(); // самая долгая
        //var a5 = tasks.Select(x => { x.Wait(); return 0; }); a5.ToArray();
        var a1 = new int[] { 1, 2 }.Select(x => x = 0)/*.ToArray()*/; // в это случае ToArray не повлияет

        // TODO сделать задержку - сумма задержек всех задач, нечётных...
        var dt3 = Now;

        //Now = Now; // встать на брейкпоинт, периодически наводить мышь - значение меняется
    }

    // TODO сделать синхронное выполнение задач Task.Delay(3000), Task.Delay(5000)
    async void TasksSync1()
    {
        var t1 = Task.Delay(1000);
        var t2 = Task.Delay(2000);

        async Task f1_() => await Task.Delay(1000);
        async Task f2_() => await Task.Delay(1000);

        var dt1 = Now;

        //t1.Wait(); // ~ await vs .Wait(). .Wait() блокирует вызывающий поток, await нет https://stackoverflow.com/questions/13140523/await-vs-task-wait-deadlockTODO
        //t2.Wait();
        // TODO результат не адекватный. самая долгая задача. в объявленных задачах нет авейта; оптимизация компилятора? зачем ждать задачу короче, если отработала задача длиннее 
        await t1;
        await t2;
        //await f1_();
        //await f2_();
        //await Task.Delay(1000); // сумма
        //await Task.Delay(1000);
        var dt2 = Now;

        var tasks = new List<Task> { Task.Delay(3000), Task.Delay(5000) };
        // google linked tasks in list, list of tasks. tasks in list are linked
        // быстро перейти в дебаге по двум вейтам - ощущается пауза. стоять в дебаге на первом вейте больше самой длительной задачи - пауз нет - оптимизация компилятора?
        // не стоять в дебаге на первом вейте - пауза = самой длительной задаче
        var dt3 = Now;
        tasks[0].Wait(); // почему-то меняется x.Status следующей задачи
        tasks[1].Wait(); // паузы нет, возможно компилятор делает оптимизацию. [пауза = разница задержек задач?]
        var dt4 = Now;
        // возможно такую синхронизацию сделать невозможно
        //Now = Now; // встать на брейкпоинт, периодически наводить мышь - значение меняется
    }
    string dt1; // TODO в методе в дебаге не видно, возможно из-за привязки к Now. Прописать напрямую DateTime
    string dt2;
    // TODO сделать последовательное выполнение задач - в итоге паузу = сумме пауз задач
    void TasksSync2()
    {
        var tasks = new List<Task> { Task.Delay(1000), Task.Delay(2000) }; // менять паузы и проверять
        //var tasks = new List<Task> { Task.Run(async () => await Task.Delay(1000)), Task.Run(async () => await Task.Delay(2000)) };
        //tasks[0].Wait(); // c Wait ContinueWith сработает мгновенно - без Wait ContinueWith подождал бы
        dt1 = Now;
        tasks[0].ContinueWith(x => {
            var st = tasks.Select(x => x.Status);
            dt2 = Now; // ок - подождалась 1-я задача
            
            //tasks[1].Wait();
            //var dt3 = Now; // TODO остаток времени от задачи 2. время до остатка шло одновременно с задачей 1. оптимизация компилятора?
            // vs
            tasks[1].ContinueWith(x => {
                var dt3 = Now;
            });

            // поставить здесь брейк, смотреть смещение всех Now
            return x; // потом сработает брейкпоинт2 (поток задачи)
        });
        var dt3 = Now;
    } // сначала сработает брейкпоинт1 (главный поток)
    
    string dt1_4;
    string dt2_4;
    string dt3_4;
    // TODO
    public void TasksSync4()
    {
        var t1 = Task.Delay(1000);
        //var t1 = Task.Run(() => Task.Delay(1000)); // в вызывающем коде - подождётся задача вне делегата
        //var t1 = Task.Run(async () => await Task.Delay(1000)); //   - подождётся задача в делегате, в вызывающем коде подождётся мгновенно
        var t2 = Task.Delay(2000);
        //var tasks = new List<Task> { t1, t2 };

        dt1_4 = Now;
        //t1.ContinueWith(x => t2).ContinueWith(x => // x => t1/t2 всё равно
        //{
        //    dt2_4 = Now; // задержка только по 1-й задаче. Id != Id исходных задач
        //});
        t1.ContinueWith(x =>
        {
            dt2_4 = Now; // ok
            t2.ContinueWith(x =>
            {
                dt3_4 = Now; // задержка только по более длительной задаче
            });
        });

    }

    string dt1_5;
    string dt2_5;
    string dt3_5;
    public async Task TasksSync5() // синхронный вариант, без async/await везде
    {
        var t1 = Task.Delay(1000);
        var t2 = Task.Delay(2000);

        dt1_5 = Now;
        await t1;
        await t2; // не ok
        //await Task.Delay(1000);
        //await Task.Delay(2000); // ok
        dt2_5 = Now;
        //await t1.ContinueWith(async x => // await/без await - в любом случае подождётся?
        //{
        //    dt2_5 = Now;
        //    await t2.ContinueWith(x =>
        //    {
        //        dt3_5 = Now; // задержка только по более длительной задаче
        //    });
        //});
    }

    //Now = Now; // встать на брейкпоинт, периодически наводить мышь - значение меняется

    string Now { get { return DateTime.Now.Second + ":" + DateTime.Now.Millisecond; } set { } }

    void TaskRun()
    {
        var t0 = new Task(() => { 
        });
        t0.Start(); // без Start не попадёт в коллбэк

        var t = Task.Run(() =>
        {
            // без Wait задержки в вызывающем коде при t.Wait не будет. 
            // задача в задаче. задача ожидает задачу, а должна ждать что-то конкретное 
            //Task.Delay(3000).Wait();
            Thread.Sleep(3000); // есть задержка, синхрон
        }); // 2 попадёт в коллбэк. Start под капотом
        //var t = Task.Delay(5000); // задержка есть
        t.Wait(); // 1 задержки нет
        // 3
        var t1 = new Task(TaskCallback);
        
        Action value = () => { };
        var value2 = () => { }; // см. var
        var t2 = new Task(value);
        var t3 = new Task(delegate () { });

        Task.Delay(3000).Wait(); // задержка
        
        void TaskCallback() { }
    }
    
    void TaskRun2()
    {
        var t = new Task(() => { 
            Thread.Sleep(3000); // 4
        }); // попадёт в коллбэк через паузу Thread.Sleep
        Thread.Sleep(1000); // 1
        t.Start(); // 2
        t.Wait(); // 3
        t.Dispose(); // 5 посмотреть занятые ресурсы до и после - профайлером
    }

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
        //await Task.Run(() => { }); // предупреждение уйдёт
        //Thread.Sleep(3000); // будет пауза await/без await в вызывающем коде

        // await в вызывающем коде - пауза. он нужен - хоть и здесь await, но метод возвращает задачу
        //await Task.Run(() => { Thread.Sleep(3000); });

        // await/без await в вызывающем коде - паузы нет. тк здесь результат задачи не возвращается - ждать в вызывающем коде нечего
        // Task.Run(() => { Thread.Sleep(3000); });

        //var a1 = await Task.Run(() => { Thread.Sleep(3000); }); // нельзя присвоить - await ничего не возвращает
    }

    #region
    // TODO async vs async Task, combinations of async methods
    // метод м.б. 
    // асинхронный (есть асинк в сигн-ре)
    // синхронный (нет асинк в сигн-ре)
    // возвр задачу
    // возвр объект (int, string, класс)
    // ничего не возвращает void
    async Task AsyncTask_() { }// был бы await - выполнялся бы асинхронно
    Task Task_() { return null; }// выполняется синхронно - просто возвращает задачу

    // https://stackoverflow.com/questions/48392630/task-vs-async-task
    // async is an indicator to the compiler that the method contains an await. 
    // When this is the case, your method implicitly returns a Task, so you don't need to.
    // чем отличаются
    Task Foo() => Task.Run(() => { });
    async Task Foo2() => await Task.Run(() => { });
    async Task Foo3() => await Task.Run(async() => await Task.Run(async () => await Task.Run(() => { })));

    //async Foo2() => await Task.Run(() => { }); // просто async быть не может
    #endregion
    
    #region https://stackoverflow.com/questions/25191512/async-await-return-task
    public Task MethodName() // просто Task
    {
        return Task.FromResult<object>(null);
        return Task.Run(() => { });
    }
    // vs
    public async Task<object> MethodName_() // async/await
    {
        return await Task.FromResult<object>(null);
    }

    //public async Task MethodName2()
    //{
    //    
    //}

    private async Task<int> MethodName3()
    {
        //await SomethingAsync();
        //return await Task.Run(() => {}); // нужен int
        return await Task.FromResult<int>(42);
        return 42;//Note we return int not Task<int> and that compiles
    }

    //public async Task<Task<object>> MethodName4()
    public async Task<object> MethodName4()
    {
        return Task.FromResult<object>(null);//This will compile
    }

    public async Task MethodName5()
    {
        return;//This should work but return is redundant and also method is useless.
    }

    public async Task<object> MethodName6()
    {
        var a1 = await Task.FromResult<object>(null); // не задача, а в сигнатуре задача
        return a1;
    }
    #endregion

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

    public async Task Main1()
    {
        #region
        //var dt4 = Now;
        //var t1 = Task.Run(() => f1());
        //var t2 = Task.Run(() => f2());
        //var t1aw = Task.Run(async () => await f1());
        //var t2aw = Task.Run(async () => await f2());

        // ожидать все 4 - суммы только первой пары, тк задачи уже выполнены
        //await f1();// сумма
        //await f2();
        //await t1;// самая длинная - не сумма! - нет await внутри
        //await t2;
        //await t1aw;// сумма
        //await t2aw;

        ////await Task.WhenAll(t1, t2); // самая длинная
        ////await Task.WhenAny(t1, t2); // самая короткая
        //var dt5 = Now;
        #endregion

        #region
        Foo1();
        //Foo2();
        //await Delay2(); // если внутри await нет - задержки нет
        //TasksSync1();
        //TasksSync2();
        //TasksSync4();
        //TasksSync5();
        //TaskRun();
        TaskRun2();
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

        var a1_ = ThreadPool.ThreadCount;

        //f(); // void - нет подчёркивания
        //g(); // без await исключения нет. оно возникнет в потоке асинхронной задачи и не передастся в вызывающий поток, тк нет await
        #endregion

        #region
        var a2_ = AsyncTask_();
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
