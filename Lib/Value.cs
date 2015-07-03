namespace SharpExpressions
{
    public class Value
    {
        public enum Type
        {
            Double,
            String,
            Boolean,
        }

        public bool boolValue;
        public double doubleValue;
        public string stringValue;
        public Type type;
    }
}
