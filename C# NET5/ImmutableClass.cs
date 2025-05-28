namespace C__NET5
{
    public sealed class ImmutableClass
    {
        public ImmutableClass(int value)
        {
            Value = value;
        }
        
        public int Value { get; }

        // почему нет предупреждения? в классе Object есть Equals. не указали override, new
        private bool Equals(ImmutableClass other) => Value == other.Value;

        //  можно вызвать equals object-а, хоть и виртуальный
        public override bool Equals(object obj)
            => ReferenceEquals(this, obj) || obj is ImmutableClass other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();
    }
}
