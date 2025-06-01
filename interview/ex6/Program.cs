/// <summary>
/// Что будет в переменной result? [15] 
/// Какой тип у переменной q? IEnumerable
/// </summary>

var list = new List<int>();
var q = list.Where(x => x > 10).Where(x => x < 20);
list.Add(5);
list.Add(15);
list.Add(25);
var result = q.ToList();
Console.Write(result);
Console.Write(q.GetType());