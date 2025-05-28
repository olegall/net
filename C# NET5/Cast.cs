using System.Collections.Concurrent;
using System;

namespace C__NET5
{
    internal class Cast
    {
        class Base
        {
            public void Base_() { }
        }

        class Derived : Base
        {
            public void Derived_() { }
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
            var b1 = new Base();
            //var d1 = (Derived)b1; // System.InvalidCastException. в дизайнтайме ошибки нет, только в рантайме. меньшее множество (Derived) становится большим множеством (Base)

            var d = new Derived();
            var d2 = d;
            d.Base_(); // у наследника доступны собственные методы и методы базового класса 
            d.Derived_();

            // большее множество(Base) становится меньшим множеством(Derived)
            var b2 = (Base)d;
            var b3 = d;
            Base b4 = (Base)d; // у b тип Derived и одновременно тип Base, т.к. он его наследует
            
            b4.Base_(); // Derived_ недоступен. Base не знает о наследниках
            //Derived d5 = (Base)d; // хоть B наследует A, это 2 разных типа

            // идентичны, 2 ссылки на 1 объект 
            var type1 = d.GetType();
            var type = d2.GetType();

            Cast_(new BlockingCollection<Int32>()); // ok
            //Cast_(new BlockingCollection<Int16>()); // runtime exception
            //Cast_(new BlockingCollection<Int64>()); // runtime exception
            //Cast_(new Int32()); // runtime exception - очевидно
        }
    }
}
