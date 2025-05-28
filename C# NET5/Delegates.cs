using System;

namespace C__NET5;

delegate String GetStatusNamespace(); // что произошло? объявили тип? экземпляр? выделилась память?
//private delegate String GetStatusNamespace(); // нельзя объявить в пространстве

internal class Delegates
{
    // объявляется только в классе. в методе нельзя - т.к. тип aleek
    // GetStatus - тип делегата?
    // делегат - это класс. объявляется класс. не принимает параметров, возвращает string
    delegate String GetStatus();
    //delegate String GetStatus(int a); // перегрузка не работает. почему?
    delegate String GetStatus2(int a);
    delegate Object GetStatus3();
    //Delegate Object GetStatus4();
    
    //static delegate void Feedback(Int32 value);
    
}
