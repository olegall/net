using System;
using System.Threading.Tasks;

namespace C__NET5.сsharpcorner_сom
{
    internal class Test
    {
        #region async
        public async void Call()
        {
            //await ShowAsync();
            try
            {
                await ShowAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task ShowAsync()
        {
            return Task.Run(() =>
            {
                //Task.Delay(2000);
                //throw new Exception("My Own Exception"); // вызывающий код не обработает
                try
                {
                    //Task.Delay(2000);
                    throw new Exception("My Own Exception");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //return null;
                }
            });
            //try
            //{
            //    //Task.Delay(2000);
            //    throw new Exception("My Own Exception");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return null;
            //}
        }
        #endregion

        #region sync
        //public void Call()
        //{
        //    //await ShowAsync();
        //    try
        //    {
        //        ShowAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //public void ShowAsync()
        //{
        //    try
        //    {
        //        throw new Exception("My Own Exception");
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    //throw new Exception("My Own Exception");
        //}
        #endregion
    }
}