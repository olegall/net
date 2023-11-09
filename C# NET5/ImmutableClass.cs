using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__NET5
{
    public sealed class ImmutableClass
    {
        public ImmutableClass(int value)
        {
            Value = value;
        }

        public int Value { get; }

        private bool Equals(ImmutableClass other)
            => Value == other.Value;

        public override bool Equals(object obj)
            => ReferenceEquals(this, obj) || obj is ImmutableClass other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
