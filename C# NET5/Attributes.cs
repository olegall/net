using System;
using System.Collections.Generic;
using System.Threading;

namespace C__NET5
{
    internal class Attributes
    {
        class SomeAttribute : Attribute
        {
            public SomeAttribute(String name, Object o, Type[] types) // ctor не зависит от базавого по кол-ву параметров
            {
            }
        }

        [Flags]
        internal enum Color
        {
            Red
        }

        [Some("Jeff", Color.Red, new Type[] { typeof(Math), typeof(Console) })]
        class SomeType
        {
        }

        public void Main_()
        {
        }
    }
}
