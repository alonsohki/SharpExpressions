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
            Method,
            Identifier,
            Operator,
        }

        public Type type { get; set; }
        public object value { get; set; }
        public bool isConstant { get; set; }
    }
}
