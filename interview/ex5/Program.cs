// Какой будет вывод программы?
// test - бесконечный вывод
// Exception
// test +
// Зависнет

lock (A.syncObj)
{
    //lock (A.syncObj) // не зависнет
    //{
    //    lock (A.syncObj)
    //    {
    //        lock (A.syncObj) { A.Write(); }
    //    }
    //}

    A.Write();
}

class A 
{
    public static object syncObj = new();

    public static void Write()
    {
        lock (syncObj) 
        { 
            Console.WriteLine("test");
        }
    }
}