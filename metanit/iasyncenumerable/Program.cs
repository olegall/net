// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// TODO пример: задержки в итераторе случайные: 1000, 1015, 995 мс. весь цикл выполнится за самую долгую задержку

#region async
// каждая итерация не блокирует главный поток;
// не надо возвращать объект (список), возвращается каждый элемент коллекции, поступает во вне по мере готовности в итоге собирается в объект
async IAsyncEnumerable<int> GetNumbersAsync()
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(1000);
        yield return i;
    }
}

Console.WriteLine(DateTime.Now.Second);
await foreach (var number in GetNumbersAsync())
{
    Console.WriteLine(number);
}
Console.WriteLine(DateTime.Now.Second);
#endregion

#region sync
//IEnumerable<int> GetNumbers()
//{
//    for (int i = 0; i < 10; i++)
//    {
//        Thread.Sleep(1000);
//        yield return i;
//    }
//}

//Console.WriteLine(DateTime.Now.Second);
//foreach (var number in GetNumbers())
//{
//    Console.WriteLine(number);
//}
//Console.WriteLine(DateTime.Now.Second);
#endregion