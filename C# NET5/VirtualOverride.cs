namespace C__NET5
{
    internal class VirtualOverride
    {
        class A0 
        {
            public virtual void Foo() 
            { 
            }
        }
        
        class B0 : A0
        {
            public override void Foo() 
            { 
            }
        }

        #region virtual new new
        public class A1
        {
            public virtual int Value1 { get; set; } = 1;

            public virtual int Value2 { get; set; } = 1;

            public virtual int Value3 { get; set; } = 1;

            public int Value4 { get; set; } = 1;
        }

        public class B1 : A1 
        {
            //public override int Value1 { get; private set; } = 1; // set нельзя, если Value у базового класса private set
            //public int Value1 { get; private set; } = 1;
            public new int Value1 { get; set; } = 2;

            public int Value2 { get; set; } = 2; // как с new, только предупреждение

            public override int Value3 { get; set; } = 2;

            public int Value4 { get; set; } = 2; // override нельзя

            public int baseValue { get { return base.Value1 + 1; } }
        }

        public class C1 : B1
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
            var a0 = new A0();
            // в дизайн тайме VS различает virtual и override
            a0.Foo(); // F12 - перейдёт на virtual
            
            //B0 b0 = (B0)a0;
            //b0.Foo(); // F12 - перейдёт на override

            a0 = new B0(); // когда-то в коде в рантайме может потребоваться поменять ссылку на другой объект. вызовется override метод сам собой
            a0.Foo(); // в дизайн тайме VS не понимает, что ссылка теперь указывает на BO. F12 - перейдёт на override. в рантайме - virtual
        }
    }
}