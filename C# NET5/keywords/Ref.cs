using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__NET5.keywords
{
    internal class Ref
    {
        public class Foo
        {
            public string Name { get; set; }
        }

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
            foo = new Foo(); // меняется ссылка, а не сам объект. состояние объекта сохраняется
            foo.Name = "3";
        }

        public void Run()
        {
            void Foo(ref int a)
            {
                a = 1;
            }

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
