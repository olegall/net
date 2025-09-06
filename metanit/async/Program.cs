// https://metanit.com/sharp/tutorial/13.7.php
#region
//var square5 = SquareAsync(5);
//var square6 = SquareAsync(6);

//Console.WriteLine("Остальные действия в методе Main");

//int n1 = await square5; 
//int n2 = await square6;
//Console.WriteLine($"n1={n1}  n2={n2}"); // n1=25  n2=36 // TODO вывод не детерминирован?

//async Task<int> SquareAsync(int n)
//{
//    await Task.Delay(0);
//    var result = n * n;
//    Console.WriteLine($"Квадрат числа {n} равен {result}");
//    return result;
//}
#endregion

#region
//Task<int> AddAsync(int a, int b) => Task.FromResult(a + b); // распараллеливание синхронного кода. м.б. сложная формула, которая долго вычисляется
//var result = await AddAsync(4, 5);
//Console.WriteLine(result);

ValueTask<int> AddAsync(int a, int b) => new ValueTask<int>(a + b);
var result = await AddAsync(4, 5);
Console.WriteLine(result);
#endregion