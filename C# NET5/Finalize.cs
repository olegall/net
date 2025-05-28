using System;
using System.Diagnostics;

namespace C__NET5;

public class Finalize_
{
    Stopwatch sw;

    public Finalize_()
    {
        sw = Stopwatch.StartNew();
        Console.WriteLine("Instantiated object");
        ShowDuration();
    }

    public void ShowDuration()
    {
        Console.WriteLine("This instance of {0} has been in existence for {1}", this, sw.Elapsed);
    }

    // Finalizers cannot be called. They are invoked automatically.
    // как отладить вызов финализатора гарбаж коллектором?
    // how to detect finalizer is called
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/finalizers
    ~Finalize_()
    {
        Console.WriteLine("Finalizing object");
        sw.Stop();
        Console.WriteLine("This instance of {0} has been in existence for {1}",  this, sw.Elapsed);
    }
}

