namespace SharpExpressions
{
    public class Value
    {
        public enum Type
        {
            Double,
            String,
            Boolean,
            Identifier,
        }

        private bool mBoolValue;
        private double mDoubleValue;
        private string mStringValue;
        private object mIdentifierValue;

        public bool boolValue
        {
            get { return mBoolValue; }
            set
            {
                mBoolValue = value;
                type = Type.Boolean;
            }
        }

        public double doubleValue
        {
            get { return mDoubleValue; }
            set
            {
                mDoubleValue = value;
                type = Type.Double;
            }
        }

        public string stringValue
        {
            get { return mStringValue; }
            set
            {
                mStringValue = value;
                type = Type.String;
            }
        }

        public object identifierValue
        {
            get { return mIdentifierValue; }
            set
            {
                mIdentifierValue = value;
                type = Type.Identifier;
            }
        }

        public Type type { get; private set; }
    }
}
