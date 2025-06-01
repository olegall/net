Console.WriteLine();
//#region Microsoft
//static Thread thread1, thread2;

//public void ThreadingMicrosoft()
//{
//    for (int i = 0; i < 5; i++)
//    {
//        Console.WriteLine("\n*********************\n");
//        thread1 = new Thread(ThreadProc);
//        thread1.Name = "Thread1";
//        thread1.Start();

//        thread2 = new Thread(ThreadProc);
//        thread2.Name = "Thread2";
//        thread2.Start();
//        Thread.Sleep(1000);
//    }
//}

//static void ThreadProc()
//{
//    Console.WriteLine("Current thread: {0}", Thread.CurrentThread.Name);

//    if (Thread.CurrentThread.Name == "Thread1" && thread2.ThreadState != System.Threading.ThreadState.Unstarted)
//    {
//        thread2.Join();
//    }

//    //Console.WriteLine("----- Sleep -----");            
//    Thread.Sleep(500);

//    //Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
//    Console.WriteLine("Thread1: {0}", thread1.ThreadState);
//    Console.WriteLine("Thread2: {0}\n", thread2.ThreadState);
//    //Thread.ThreadState - методы, классы экземплярного, статического Thread
//}

////internal sealed class MyForm : Form
////{
////    private readonly TaskScheduler m_syncContextTaskScheduler;

////    public MyForm()
////    {
////        m_syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
////        Text = "Synchronization Context Task Scheduler Demo";
////        Visible = true; 
////        Width = 600; 
////        Height = 100;
////    }

////    // Получение ссылки на планировщик заданий
////    private readonly TaskScheduler m_syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
////    private CancellationTokenSource m_cts;

////    protected override void OnMouseClick(MouseEventArgs e)
////    {
////        if (m_cts != null)
////        { // Операция начата, отменяем ее
////            m_cts.Cancel();
////            m_cts = null;
////        }
////        else
////        { // Операция не начата, начинаем ее
////            Text = "Operation running";
////            m_cts = new CancellationTokenSource();

////            // Задание использует планировщик по умолчанию и выполняет поток из пула
////            Task<Int32> t = Task.Run(() => Sum(m_cts.Token, 20000), m_cts.Token);

////            // Эти задания используют планировщик контекста синхронизации и выполняются в потоке графического интерфейса
////            t.ContinueWith(task => Text = "Result: " + task.Result, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, m_syncContextTaskScheduler);
////            t.ContinueWith(task => Text = "Operation canceled", CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, m_syncContextTaskScheduler);
////            t.ContinueWith(task => Text = "Operation faulted", CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, m_syncContextTaskScheduler);
////        }
////        base.OnMouseClick(e);
////    }
////}

//internal static class TimerDemo
//{
//    private static Timer s_timer;

//    //public static void Main()
//    //{
//    //    Console.WriteLine("Checking status every 2 seconds");

//    //    // Создание таймера, который никогда не срабатывает. Это гарантирует, что ссылка на него будет храниться в s_timer, до активизации Status потоком из пула
//    //    s_timer = new Timer(Status, null, Timeout.Infinite, Timeout.Infinite);

//    //    // Теперь, когда s_timer присвоено значение, можно разрешить таймеру срабатывать; мы знаем, что вызов Change в Status не выдаст исключение NullReferenceException
//    //    s_timer.Change(0, Timeout.Infinite);

//    //    Console.ReadLine(); // Предотвращение завершения процесса
//    //}

//    // Сигнатура этого метода должна соответствовать сигнатуре делегата TimerCallback
//    private static void Status(Object state)
//    {
//        // Этот метод выполняется потоком из пула
//        Console.WriteLine("In Status at {0}", DateTime.Now);
//        Thread.Sleep(1000); // Имитация другой работы (1 секунда). Заставляем таймер снова вызвать метод через 2 секунды
//        s_timer.Change(2000, Timeout.Infinite);
//        // Когда метод возвращает управление, поток возвращается в пул и ожидает следующего задания
//    }
//}

//internal static class DelayDemo
//{
//    //public static void Main()
//    //{
//    //    Console.WriteLine("Checking status every 2 seconds");
//    //    Status();
//    //    Console.ReadLine(); // Предотвращение завершения процесса
//    //}

//    // Методу можно передавать любые параметры на ваше усмотрение
//    private static async void Status()
//    {
//        while (true)
//        {
//            Console.WriteLine("Checking status at {0}", DateTime.Now);
//            // Здесь размещается код проверки состояния... В конце цикла создается 2-секундная задержка без блокировки потока
//            await Task.Delay(2000); // await ожидает возвращения управления потоком
//        }
//    }
//}

// class Window // aleek
//{
//    protected virtual void OnActivated(EventArgs e)
//    {
//        throw new NotImplementedException();
//    }
//}
//#endregion
