using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__NET5.async_await
{
    internal class AsyncAwaitExceptionsMetanit
    {
        public async Task Main1()
        {
            try
            {
                PrintAsync("Hello METANIT.COM");
                PrintAsync("Hi");       // здесь программа сгенерирует исключение и аварийно остановится
                await Task.Delay(1000); // ждем завершения задач
            }
            catch (Exception ex)    // исключение НЕ будет обработано (async void)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task PrintAsync(string message)
        {
            // если длина строки меньше 3 символов, генерируем исключение
            if (message.Length < 3)
                throw new ArgumentException($"Invalid string length: {message.Length}");
            await Task.Delay(100);     // имитация продолжительной операции
            Console.WriteLine(message);
        }

        //async void PrintAsync(string message)
        //{
        //    // если длина строки меньше 3 символов, генерируем исключение
        //    if (message.Length < 3)
        //        throw new ArgumentException($"Invalid string length: {message.Length}");
        //    await Task.Delay(100);     // имитация продолжительной операции
        //    Console.WriteLine(message);
        //}
    }
}
