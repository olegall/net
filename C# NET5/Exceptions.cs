using System;
using System.Threading.Tasks;

namespace C__NET5;

internal class Exceptions
{
    internal Exceptions()
    {
        //Foo1();
        TryFinally();
        //ProcessRequest(-1);
        //Foo4();
        //Foo6();
        //Foo7();
        //Foo8();
    }

    public int Foo1()
    {
        //throw new ArgumentNullException(); // аварийно завершится, никуда не попадёт, нет болка try

        try
        {
            // - Q м.б. что-то такое в Foo2, что НЕ приведёт к отработке finally?
            // - A выброс исключения. Это другой, не связанный с данным finally стек исключений
            Exception(); 
            
            // попадёт в catch того же типа или выше по иерархии
            throw new ArgumentNullException("");
            //throw new Exception();
        }
        catch (ArgumentNullException e) // от производного к общему исключению. иначе ошибка; в базовый catch не попадёт - и-е уже обработано
        {
            //var _0 = 0; var a1 = 1 / _0; // не попадёт в catch. нужен try/catch в этом скоупе. аварийно остановится

            //throw; // rethrows existing exception. отправится обратно в try
            //throw e; // resets the stack trace. аварийно остановится
            //throw new Exception(); // resets the stack trace. аварийно остановится
        }
        catch (Exception e)
        {
            // попадёт в finally
            throw;
            //throw e;

            return 2; // finally сработает, но отсюда вернётся
        }

        //catch (ArgumentNullException e) {} // ошибка - в любом месте цепочки - уже есть такой блок

        // - Q можно в catch - освободить ресурсы и т.д.?
        // - A нет - ПО аварийно остановится. finally - гарантия, что код в нём будет выполнен или при благоприятном исходе (try) или при исключении (catch). ПО продолжит работу
        // без finally - аварийно остановится, если был выброс исключения
        // .Dispose() ресурсов, неизвестно что отработает - try или catch, и в try и в catch диспоузить избыточно  
        finally // - когда явно выброшено исключение
        {
            //return 2; return в finally нельзя. должен по любому выполниться код
        }

        return 0;
    }

    //private static int Foo2()
    //{
    //    throw new Exception("foo2"); // почему без return<int> работает?
    //}

    private void Exception() 
    {
        throw new Exception("exc"); // сработает соотв catch в вызывающем коде, finally не сработает
    }

    public async Task TryFinally()
    //public void TryFinally()
    {
        try
        {
            var a1 = 0;
            // без catch в finally: void TryFinally - не зайдёт; async Task TryFinally - зайдёт
            // - Q зачем тогда нужен try/finally, раз в finally не заходит при прямом выбросе исключения?
            // - A propagation of an exception out of the try block - microsoft
            throw new Exception();
            //Exception();

            //ProcessAsync(); // - catch + finally () - void TryFinally
            await ProcessAsync(); // + catch + finally - async Task TryFinally
        }
        catch (Exception e)
        {
        }
        finally
        {
        }
    }

    // упростил. https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements
    private async Task ProcessAsync() => // finally +, catch - эксепшн - в другом потоке (задаче)
    //private void ProcessAsync() => // finally -
        throw new ArgumentOutOfRangeException();

    #region можно не рассматривать - есть эквивалентный пример TryFinally
    //public async Task ProcessRequest(int itemId)
    //{
    //    var Busy = true;

    //    try
    //    {
    //        await ProcessAsync(itemId);
    //    }
    //    catch (Exception e)/* when (e is not OperationCanceledException)*/
    //    {
    //        //LogError(e, $"Failed to process request for item ID {itemId}.");
    //        throw;
    //    }
    //    finally
    //    {
    //        Busy = false;
    //    }

    //}
    
    //private static async Task<int> ProcessAsync(int input)
    //{
    //    if (input < 0)
    //    {
    //        throw new ArgumentOutOfRangeException(nameof(input), "Input must be non-negative.");
    //    }

    //    await Task.Delay(500);
    //    return input;
    //}
    #endregion

    public void Foo4()
    {
        try
        {
            var _0 = 0; var a1 = 1 / _0;
            // vs - эквивалентно? что происходит на уровне кода, CLR?
            throw new DivideByZeroException();
        }
        catch (Exception e) // попадёт
        {
        }
    }
    
    public void Foo6()
    {
        try
        {
            throw new Exception();
        }
        catch (ArgumentNullException e) // не попадёт - более узкая воронка
        {
        }
    }
    
    public void Foo7()
    {
        try
        {
            //throw new Exception(); // попадёт в catch
            new Exception(); // не выбросили, просто объявили объект исключения. не попадёт в catch
        }
        catch (Exception)
        {
        }
    }

    public void Foo8()
    {
        try
        {
            //var a1 = 0; // с этой и бех этой строки попадёт в finally
            return; // попадёт в finally
        }
        finally
        {
        }
    }
}