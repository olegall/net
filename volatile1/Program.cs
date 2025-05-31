https://habr.com/ru/articles/130318/
new ReorderTest().Foo();

class ReorderTest
{
    private /*volatile*/ int _a; // программа не зависает без volatile

    public void Foo()
    {
        var task = new Task(Bar);
        task.Start();
        Thread.Sleep(1000);
        _a = 0;
        task.Wait();
    }

    public void Bar()
    {
        _a = 1;
        while (_a == 1)
        {
            Console.Write("*");
        }
    }
}

