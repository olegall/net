ProgramInterlocked.Main();

class ProgramInterlocked // не рабит
{
    static int _value;

    public static void Main()
    {
        for (int i = 0; i < 50; i++)
        {
            var rand = new Random().Next(2);
            Thread thread1 = new Thread(new ThreadStart(A));
            Thread thread2 = new Thread(new ThreadStart(A2));
            if (rand == 0)
            {
                thread1.Start();
                thread2.Start();
            }
            else 
            {
                thread2.Start();
                thread1.Start();
            }
            
            Console.Write("rand " + rand);
            thread1.Join();
            thread2.Join();
            _value = 0;
        }
    }

    //static void A()
    //{
    //    Thread.Sleep(100);
    //    // Add one.
    //    //Interlocked.Add(ref _value, 1);
    //    //_value += 1;
    //}

    #region
    // Вывести потоком переменную, достоверно инициализированную другим потоком
    // Некорректная ситуация (без lock): 0 (начальное значение переменной)
    static void A() // проверка lock
    {
        Thread.Sleep(100);
        _value = 1;
        //var locker = new object();
        //lock (locker)
        //{
        //    Console.WriteLine(_value);
        //}
    }

    static void A2()
    {
        Thread.Sleep(100);
        //while (_value != 1) ; // зависает
        Console.WriteLine(" value " + _value);
    }
    #endregion
}

//class ProgramInterlocked
//{
//    static int _value;

//    public static void Main()
//    {
//        Thread thread1 = new Thread(new ThreadStart(A));
//        Thread thread2 = new Thread(new ThreadStart(A));
//        thread1.Start();
//        thread2.Start();
//        thread1.Join();
//        thread2.Join();
//        Console.WriteLine(_value);
//    }

//    static void A()
//    {
//        // Add one then subtract two.
//        Interlocked.Increment(ref _value);
//        Interlocked.Decrement(ref _value);
//        Interlocked.Decrement(ref _value);
//        //_value++;
//        //_value--;
//        //_value--;
//    }
//}