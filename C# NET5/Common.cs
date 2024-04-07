using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace C__NET5
{
    internal class Common 
    {
        class Empl : ICloneable
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public object Clone()
            {
                return new Empl();
            }
        }

        class EmplCopy : Empl
        {
        }

        //var empls = new List<Empl>() { new Empl { Id = 1, Name = "Pete" } }; // почему нельзя объявить через var на уровне класса?

        public void Run()
        {
            var empls = new List<Empl>() { new Empl { Id = 1, Name = "Pete" } };
            
            var emplCopy = new List<Empl>();
            
            foreach (var empl in empls)
            {
                //emplCopy.Add(empl);
                //var empl_ = empl; emplCopy.Add(empl_); // не поможет
                emplCopy.Add((Empl)empl.Clone()); // фикс
            }
            emplCopy.First().Name = ""; // повлияло на empls. empls.First().Name = ""
        }
        
        public void Run3()
        {
            var empl = new Empl { Id = 1, Name = "Pete" };

            //var emplCopy = empl; // повлияет на empl. empl.Name = ""
            //var emplCopy = (Empl)empl.Clone(); // fix
            var emplCopy =  new Empl { Id = empl.Id, Name = empl.Name }; // fix
            
            emplCopy.Name = "";
        }
        
        public void Run4() // сделать Empl не класс, а структурой. это тип значения, эффект запоминания данных через ссылки уйдёт
        {
        }

        class ChangeArrayItems
        {
            //string[] a1 = new[] { "a", "b" };
            IEnumerable<string> a1 = new string[] { "a", "b" };
            List<string> a2 = new List<string>() { "a", "b" }; // чтобы далее не вызывать IList, объявлен именно как List

            class A { public string Name { get; set; } }
            List<A> items = new List<A>() { new A { Name = "a" }, new A { Name = "b" } };

            public ChangeArrayItems()
            {
                a1.ToList().ForEach(x => x += "_" ); // не изменит. строка ссылочный тип, ведёт себя как тип значений
                a1.ToList().ForEach(x => x = null );
                a1.Select(x => { x += "__"; return x; });
                
                a2.ForEach(x => x += "_"); // не изменит

                items.ForEach(x => x.Name += "_"); // изменит
                items.Select(x => { x.Name += "__"; return x; }); // не изменит. lazy
                items.Select(x => { x.Name += "__"; return x; }).ToArray(); // изменит
            }
        }

        public Common()
        {   
            //using var client = new WebClient();
            //using var a1 = new A();

            new ChangeArrayItems();
            
            //Foo(ref 1);
            int a1;
            //Foo(ref int a1);
        }
    }
}