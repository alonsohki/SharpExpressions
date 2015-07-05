namespace SharpExpressions.Compiler
{
    class Entry
    {
        public enum Type
        {
            None,
            Double,
            String,
            Boolean,
            Identifier,
        }
        public Type type;
    }
}
