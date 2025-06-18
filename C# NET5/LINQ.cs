using System.Linq;

namespace C__NET5;

internal class LINQ
{
    class A { public int Num { get; set; } }

    public LINQ() // ~ к-р по дефолту не публичный
    {
        var a1 = new int[] { 1, 2 }.Select(x => x = 0); // меняет, т.к. проекция. Результат - как lazy

        var a2 = new int[] { 1, 2 };
        //a2.ToList().ForEach(x => x = 0); // не меняет - просто перебирает, не возвр-т проекцию
        // foreach (var item in a2) item = 0; // ~ в foreach нельзя изменить эл-т
        foreach (var item in a2) Change(item); // чтобы поменять - нужен метод. и то не поменяет (копия). что нужно сделать, чтобы сенялось? обернуть int в ссылочный тип https://stackoverflow.com/questions/1160217/changing-element-value-in-listt-foreach-foreach-method

        var a3 = new int[] { 1, 2 }; // введение избыточности - независимость примеров. исключается ошибка, когда не то закомментировал/раскомментировал
        for (int i = 0; i < a3.Length; i++) a3[i] = 0; // поменяет

        //var a4 = new A[] { new A { Num = 1 }, new A { Num = 2 } };
        var a4 = GetA<A>();
        foreach (var item in a4) Change(item);

        new int[] { 1, 2 }.ToList().ForEach(delegate (int x) { });
    }

    void Change(int item) => item = 0;
    
    void Change(A item) => item.Num = 0;

    T[] GetA<T>() where T : A, new() => new T[] { new T { Num = 1 }, new T { Num = 2 } }; // #generic
}