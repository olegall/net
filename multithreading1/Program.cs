// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var arr = new int[2];

Console.WriteLine("Main " + Thread.CurrentThread.ManagedThreadId);

var obj = new Object();

void Write1()
{
    lock (obj) // TODO за этот участок д. конкурировать больше одного потока. сейчас у каждого потока по методу - по локу. бессмысленно
    {
        // если под потоком, ManagedThreadId не равен как у главного потока, без потока - равны TODO .Name почему-то null
        //Console.WriteLine("Write1 " + Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(10);
        arr[0] = 1;
        Thread.Sleep(10);
        arr[1] = 2;
        //arr[2] = 12;
        //arr[3] = 13;
        //arr[4] = 14;
        //arr[5] = 15;
        //arr[6] = 16;
        //arr[7] = 17;
        //arr[8] = 18;
        //arr[9] = 19;
    }
}

void Write2()
{
    lock (obj)
    {
        //Console.WriteLine("Write2 " + Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(10);
        arr[0] = 3;
        Thread.Sleep(10);
        arr[1] = 4;
        //arr[2] = 22;
        //arr[3] = 23;
        //arr[4] = 24;
        //arr[5] = 25;
        //arr[6] = 26;
        //arr[7] = 27;
        //arr[8] = 28;
        //arr[9] = 29;
    }
}

void Write()
{
    lock (obj)
    {
        Thread.Sleep(10);
        arr[0] = Thread.CurrentThread.ManagedThreadId;
        Thread.Sleep(10);
        arr[1] = Thread.CurrentThread.ManagedThreadId;
    }
}

//var t1 = new Thread(Write1);
//var t2 = new Thread(Write2);
//t1.Start();
//t1.Start(); // TODO 2 раза подряд Start нельзя - уже запущен
for (int i = 0; i < 20; i++)
{
    new Thread(Write).Start();
    new Thread(Write).Start();
    //Write1(); // TODO под главном потоком с/без lock
    //Write2();
    if (!(arr[0] == 1 && arr[1] == 2) || !(arr[0] == 3 && arr[1] == 4) && arr[0] != 0 && arr[1] != 0) // TODO попадает с нарушением условия - главный поток показывает, новый поток ещё не записал
    {
        //Console.Write("***** " + a1[0] + " " + a1[1] + " *****" + "\n");
    }
    //Console.Write(arr[0]+" "+arr[1] + " " + arr[2] + " " + arr[3] + " " + arr[4] + " " + arr[5] + " " + arr[6] + " " + arr[7] + " " + arr[8] + " " + arr[9] + "\n");
    Thread.Sleep(100); // без паузы - хоть и lock - разнобой. Console.Write м. сработать быстрее записи актуальных данных потоком
    Console.Write(arr[0] + " " + arr[1] + "\n"); // без lock - разнобой. 
    //Debug.WriteLine("");
}

Console.WriteLine();