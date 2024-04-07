using System.Collections.Concurrent;
using System;

namespace C__NET5
{
    internal class Cast
    {
        class Base
        {
        }

        class Derived : Base
        {
        }
        
        class C : Derived
        {
        }

        private void Cast_(object o) 
        {
            var result = (BlockingCollection<Int32>)o;
        }

        public void Run() 
        {
            //var a = new A();
            //var aCopy = (B)a; // A ничего не знает про B
            
            var d = new Derived();
            //var dCopy = d;
            //var dCopy = (Base)d; // Derived является Base
            //A dCopy = (A)d; // у b тип Derived и одновременно тип Base, т.к. он его наследует
            //Derived dCopy = (Base)d; // хоть B наследует A, это 2 разных типа
            //var type1 = d.GetType();
            //var type = dCopy.GetType();

            Cast_(new BlockingCollection<Int32>()); // ok
            //Cast_(new BlockingCollection<Int16>()); // runtime exception
            //Cast_(new Int32()); // runtime exception
        }
    }
}
