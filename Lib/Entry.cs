namespace SharpExpressions
{
    public struct Entry
    {
        public enum Type
        {
            Unknown,
            Double,
            String,
            Boolean,
            Object,
            Identifier,
            Operator,
        }

        public Type type { get; set; }
        public object value { get; set; }
    }
}
