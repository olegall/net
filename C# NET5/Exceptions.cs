using System;

namespace C__NET5
{
    internal class Exceptions
    {
        internal Exceptions()
        {
            Foo1();
            Foo3();
        }

        public static int Foo1()
        {
            var a = 0;
            //throw new ArgumentNullException();
            try 
            {
                //Foo2(); // м.б. что-то такое в Foo2, что НЕ приведёт к отработке finally?
                a = 1;
                throw new ArgumentNullException();
                //throw new Exception();
            }
            //catch (ArgumentNullException)

            catch (ArgumentNullException e) // от производного к общему исключению. иначе ошибка
            {
                //var zero = 0; var a1 = 1 / zero; // не попадёт в catch
                //throw new Exception(); // resets the stack trace
                //throw; // rethrows existing exception
                //throw e; // resets the stack trace

                // видимо в другой catch попасть уже невозможно, срабатывает только 1 catch
            }

            //catch (Exception e)
            //{
            //    a = 2;
            //    //throw;
            //    //throw e;
            //    return 2; // finally сработает, но отсюда вернётся
            //}

            //catch (ArgumentNullException e) {}
            // зачем нужен finally, если всё можно сделать в catch - освободить ресурсы и т.д.
            // без finally программа аварийно не остановится
            // finally - гарантия, что код в нём будет выполнен или при благоприятном исходе (try) или при исключении (catch)
            finally
            {
                a = 3;
                //return 2; нельзя
            }

            return a;
        }

        //private static int Foo2()
        //{
        //    throw new Exception("foo2"); // почему без return<int> работает?
        //}

        private static int Foo2() 
        {
            try
            {
                throw new Exception("Foo2");
            }
            catch (Exception e)
            {

            }
            finally
            {
            }

            return 0;
        }

        public static void Foo3()
        {
            try
            {
                var zero = 0;
                //var a1 = 1 / zero; // без catch в finally не зайдёт
                //throw new Exception(); // без catch в finally не зайдёт
            }
            //catch (Exception e)
            //{
            //}
            finally // без finally пришлось бы освобождать ресурсы и в try, и в catch, а сработал бы только 1 блок. 1. Где-то освобождение ресурсов лишнее, 2. Дублирование кода
            {
            }
        }
        
        public static void Foo4()
        {
            try
            {
                var zero = 0;
                
                var a1 = 1 / zero;
                // vs - эквивалентно? что происходит на уровне кода, CLR?
                throw new DivideByZeroException();
            }
            catch (Exception e)
            {
            }
        }
        
        public static void Foo5()
        {
            try
            {
            }
            catch (Exception e)
            {   // различия
                throw;
                throw e;
                throw new Exception();
            }
        }
    }
}