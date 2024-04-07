using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__NET5
{
    internal class Finalize_
    {
        public class ExampleClass
        {
            Stopwatch sw;

            public ExampleClass()
            {
                sw = Stopwatch.StartNew();
                Console.WriteLine("Instantiated object");
            }

            public void ShowDuration()
            {
                Console.WriteLine("This instance of {0} has been in existence for {1}", this, sw.Elapsed);
            }

            ~ExampleClass()
            {
                Console.WriteLine("Finalizing object");
                sw.Stop();
                Console.WriteLine("This instance of {0} has been in existence for {1}",  this, sw.Elapsed);
            }
        }

        public class Demo
        {
            public static void Main1()
            {
                ExampleClass ex = new ExampleClass();
                ex.ShowDuration();
            }
        }
    }
}
