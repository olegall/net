namespace C__NET5
{
    internal class VirtualOverride
    {
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
    }
}