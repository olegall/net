namespace C__NET5
{
    // https://learn.microsoft.com/ru-ru/dotnet/csharp/programming-guide/classes-and-structs/knowing-when-to-use-override-and-new-keywords
    internal class VirtualOverride
    {
        #region
        class B0 
        {
            public void Foo()
            {
            }

            public void FooNew()
            {
            }

            public virtual void FooVirtualOverride() 
            { 
            }
        }
        
        class D0 : B0
        {
            public void Foo() // вызовется только у созданного объекта B0
            {
            }

            public new void FooNew() // вызовется только у созданного объекта B0
            {
            }

            public override void FooVirtualOverride() 
            { 
            }
        }
        #endregion

        #region virtual new new
        public class B1
        {
            public virtual int Value1 { get; set; } = 1;

            public virtual int Value2 { get; set; } = 1;

            public virtual int Value3 { get; set; } = 1;

            public int Value4 { get; set; } = 1;
        }

        // google new vs override
        // во всех случаях идёт перекрытие. зачем тогда нужны override, new? https://stackoverflow.com/questions/59428295/why-is-it-required-to-have-override-keyword-in-front-of-abstract-methods-when-we
        public class D1 : B1 
        {
            //public override int Value1 { get; private set; } = 1; // set нельзя, если Value у базового класса private set
            //public int Value1 { get; private set; } = 1;
            public new int Value1 { get; set; } = 2;
            public int Value1Base { get { return base.Value1; } }

            public int Value2 { get; set; } = 2; // как с new, только предупреждение
            public int Value2Base { get { return base.Value2; } }

            public override int Value3 { get; set; } = 2; // только в этом случае в дебаге не выводится это св-во у базового класса. чистое переопределение
            public int Value3Base { get { return base.Value3; } }

            public int Value4 { get; set; } = 2; // override нельзя
            public int Value4Base { get { return base.Value4; } }

            public int baseValue { get { return base.Value1 + 1; } }
        }

        public class D2 : D1
        {
            public new int Value1 { get; set; } = 3;

            public int Value2 { get; set; } = 3;

            public override int Value3 { get; set; } = 3;

            public new int Value4 { get; set; } = 3;

            public int baseValue_ { get { return base.baseValue; } }
        }
        #endregion

        #region virtual new new
        public class A2
        {
            public virtual int Value { get; set; } = 1;
        }

        public class B2 : A2
        {
            public override int Value { get; set; } = 2;
        }

        public class C2 : B2
        {
            public new int Value { get; set; } = 3;

            public int a1Value { get { return base.Value; } }
        }
        #endregion

        class A 
        { 
            void Foo() { } // ошибки нет, т.к. приватный
        }
        
        class B : A 
        {
            void Foo() { }
        }
        
        class A3 
        { 
            public void Foo() { } 
        }
        
        class B3 : A3
        {
            void Foo() { }
        }

        public void Run() 
        {
            #region
            var b0 = new B0();
            // в дизайн тайме VS различает virtual и override
            b0.Foo();
            b0.FooNew();
            b0.FooVirtualOverride(); // F12 - перейдёт на virtual

            //D0 d0 = (B0)b0;
            //d0.Foo(); // F12 - перейдёт на override

            b0 = new D0(); // когда-то в коде в рантайме может потребоваться поменять ссылку на другой объект. вызовется override метод сам собой
            b0.Foo();
            b0.FooNew();
            b0.FooVirtualOverride(); // в дизайн тайме VS не понимает, что ссылка теперь указывает на BO. F12 - перейдёт на override. в рантайме - virtual
            
            var d0 = new D0();
            d0.Foo();
            d0.FooNew();
            #endregion

            #region
            //var a1 = new A1();
            //a1 = new B1();
            //a1 = new C1();

            var a1 = new B1();
            var b1 = new D1();
            #endregion
        }
    }
}