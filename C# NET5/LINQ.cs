using System.Linq;

namespace C__NET5;

internal class LINQ
{
    public void Foo1() 
    {
        var a1 = new int[] { 1, 2 }.Select(x => x = 0)/*.ToArray()*/; // TODO без ToArray всё равно изменит, а д.б. Lazy

        var arr = new int[] { 1, 2 };
        arr.ToList().ForEach(x => x += 1); // TODO не изменил массив 

        new int[] { 1, 2 }.ToList().ForEach(delegate (int x) { });
    }
}
