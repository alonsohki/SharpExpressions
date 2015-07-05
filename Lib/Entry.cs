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
            Type,
        }

        public Type type { get; set; }
        public object value { get; set; }
        public bool isConstant { get; set; }

        public static Type fromSystemType(System.Type systemType)
        {
            if (systemType == typeof(bool))
            {
                return Entry.Type.Boolean;
            }
            else if (systemType == typeof(string))
            {
                return Entry.Type.String;
            }
            else if (systemType == typeof(short) || systemType == typeof(int) || systemType == typeof(long) || systemType == typeof(float) || systemType == typeof(double))
            {
                return Entry.Type.Double;
            }
            else
            {
                return Entry.Type.Object;
            }
        }
    }
}
