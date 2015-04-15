namespace SharpExpressions
{
    public class Entry
    {
        public enum Type
        {
            Value,
            Identifier,
            Operator,
        }

        public Type type { get; set; }
        public object value { get; set; }
    }
}
