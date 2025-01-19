using System.Collections.Generic;
using System.Linq;

namespace C__NET5.keywords
{
    // ref iunder the hood
    internal class Ref
    {
        public class Foo
        {
            public string Name { get; set; }
        }
        #region
        public void Bar(Foo foo)
        {
            foo.Name = "1";
        }

        public void Bar2(Foo foo)
        {
            foo.Name = "2";
        }
        
        public void Bar3(Foo foo)
        {
            foo = new Foo(); // выделяется новая память под Foo. старый Foo сохраняется
            foo.Name = "3";
        }
        
        public void BarRef2(ref Foo foo)
        {
            foo.Name = "2";
        }
        public void BarRef3(ref Foo foo)
        {
            foo = new Foo(); // в куче создаётся новый объект, ссылка теперь указывает на него
        }
        #endregion

        #region
        void M(ref int refParameter) 
        { 
            refParameter += 42; 
        }
        //void M(ref string refParameter2) { refParameter2 += 42; } // нет ошибки string vs int
        
        void M(ref string refStr) 
        { 
            refStr += "_";
        } 

        void MNoRef(string refStr) 
        { 
            refStr += "_"; 
        }

        void M(Foo foo)
        {
            foo.Name += "_";    
        }

        void MRef(ref Foo foo)
        {
            foo.Name += "_";
        }
        #endregion

        public void Run()
        {
            int refParameter_ = 0; // изменится
            M(ref refParameter_);

            string refStr = "a"; // изменится
            M(ref refStr); //M(ref "a"); // нельзя

            // не изменится. возможно:
            // передаётся копия а не ссылка;
            // из-за особенностей строки - ссылочный/значимый тип
            // строка - immutable
            // передаётся ссылка по значению? pass reference by value vs reference by reference vs value by value vs value by reference
            // https://stackoverflow.com/questions/1096449/c-sharp-string-reference-type
            // https://stackoverflow.com/questions/31368068/impact-of-using-the-ref-keyword-for-string-parameters-in-methods-in-c
            string noRefStr = "a";  
            MNoRef(noRefStr);

            Foo foo = new Foo { Name = "a" }; // изменится
            M(foo);

            Foo fooRef = new Foo { Name = "a" }; // изменится
            MRef(ref fooRef);

            int break_ = 0;

            //ref int Foo()
            ////ref int? Foo()
            //{
            //    int a1 = 1;
            //    ref int found = ref a1;
            //    //ref int found;
            //    //ref int a1 = 1;
            //    //return ref a1;
            //    //return ref found;
            //    //return null;
            //}

            ref int FindFirst(int[] numbers)
            {
                return ref numbers[0];
            }

            ref int FindFirst2(ref int[] numbers)
            {
                return ref numbers[0];
            }
            
            int FindFirst3(ref IEnumerable<int> numbers)
            {
                return numbers.ElementAt(0);
            }
            
            //ref int FindFirst4(ref IEnumerable<int> numbers)
            //{
            //    return ref numbers.ElementAt(0);
            //}

            //ref int FindFirst5(int numbers) // c массивом так можно
            //{
            //    return ref numbers;
            //}

            ref int FindFirst6(ref int numbers)
            {
                return ref numbers;
            }
            
            //ref int FindFirst7()
            //{
            //    int numbers = 0;
            //    return ref numbers;
            //}

            int[] xs = new[] { 0, 0, 0 }; // microsoft
            ref int element = ref xs[0];
            //ref int element2;
            element = 1;
        }

        #region Swap SO
        public void Swap<T>(ref T x, ref T y)
        {
            T t = x;
            x = y;
            y = t;
        }
        
        public void Swap<T>(T x, T y)
        {
            T t = x;
            x = y;
            y = t;
        }
        #endregion
    }
}
