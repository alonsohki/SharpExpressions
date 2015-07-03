namespace SharpExpressions.Parser
{
    public class Entry
    {
        public enum Type
        {
            Value,
            String,
            Boolean,
            Identifier,
            Operator,
        }

        public Type type { get; set; }
        public object value { get; set; }
    }
}
