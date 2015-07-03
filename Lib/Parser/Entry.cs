namespace SharpExpressions.Parser
{
    public class Entry
    {
        public enum Type
        {
            Double,
            String,
            Boolean,
            Identifier,
            Operator,
        }

        public Type type { get; set; }
        public object value { get; set; }
    }
}
