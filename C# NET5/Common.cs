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

        struct Empl_
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        // сделать копию empls, но с обнулёнными полями. возможно ошибка в постановке
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
        
        // сделать независимую копию списка. чтобы изменения одного списка не влияли на другой
        public void Run3()
        {
            var empl = new Empl { Id = 1, Name = "Pete" };

            //var emplCopy = empl; // скопировали ссылку. 2 ссылки указывают на 1 объект. измение состояния объекта по любой ссылке будет отражено по всем ссылкам 
            //empl.Name = null; // emplCopy.Name = null. меняем состояние объекта через ссылку. 
            // или
            //emplCopy.Name = null; // empl.Name = null

            //var emplCopy = (Empl)empl.Clone(); // fix
            var emplCopy =  new Empl { Id = empl.Id, Name = empl.Name }; // fix. новый объект с новой ссылкой

            emplCopy.Name = "";
        }
        
        // сделать Empl не класс, а структурой. это тип значения, эффект запоминания данных через ссылки уйдёт
        public void Run4()
        {
            var s1 = new Empl_ { Name = "Oleg" };
            var s2 = s1;
            s2.Name = null;
        }

        class ChangeArrayItems
        {
            //string[] a1 = new[] { "a", "b" };
            IEnumerable<string> a1 = new string[] { "a", "b" };
            List<string> a2 = new List<string>() { "a", "b" }; // чтобы далее не вызывать ToList, объявлен именно как List
            IEnumerable<int> a3 = new int[] { 1, 2 };
            IEnumerable<int?> a4 = new int?[] { 1, 2 };

            class A { public string Name { get; set; } }
            List<A> items = new List<A>() { new A { Name = "a" }, new A { Name = "b" } };

            public ChangeArrayItems()
            {
                a1.ToList().ForEach(x => x += "_" ); // не изменит. т.к. строка - immutable, создаётся копия, ссылочный тип, ведёт себя как тип значений?
                a2.ForEach(x => x += 0);
                //a2.ForEach(x => x); // нельзя, см. делегат
                a3.ToList().ForEach(x => x = 0); // не изменит
                a4.ToList().ForEach(x => x = 0); // не изменит, хоть и nullable

                items.ForEach(x => x.Name += "_"); // изменит - ссылочный тип. т.к. .ForEach возвр List - эквивалент IEnumerable.ToList() нет lazy
                var a5 = items.Select(x => { x.Name += "__"; return x; })/*.ToArray()*/; // без ToArray не изменит. lazy
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