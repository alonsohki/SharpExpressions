namespace SharpExpressions
{
    public struct Value
    {
        public enum Type
        {
            None,
            Double,
            String,
            Boolean,
            Object,
        }

        private bool mBoolValue;
        private double mDoubleValue;
        private string mStringValue;
        private object mObjectValue;

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

        public object objectValue
        {
            get { return mObjectValue; }
            set
            {
                mObjectValue = value;
                type = Type.Object;
            }
        }

        public Type type { get; private set; }
        public object value
        {
            get
            {
                switch (type)
                {
                    case Type.Double:
                        return doubleValue;
                    case Type.Boolean:
                        return boolValue;
                    case Type.String:
                        return stringValue;
                    case Type.Object:
                        return objectValue;
                }
                return null;
            }
        }
    }
}
