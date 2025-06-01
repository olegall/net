/// <summary>
/// Что будет выведено на экран? AC. B не будет - задача улетает, хоть внутри есть авейт, д.б. внешний авейт
/// // await Task.Delay(1000); await Process(); - рез-т другой, static не влияет
/// </summary>
static async Task Process()
{
    Console.WriteLine("A");
    await Task.Delay(1000);
    Console.WriteLine("B");
}

Process();
Console.WriteLine("C");