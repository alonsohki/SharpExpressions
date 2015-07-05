using System.Runtime.InteropServices;

namespace SharpExpressions
{
    [StructLayout(LayoutKind.Explicit)]
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

        [FieldOffset(0)]
        private Type mType;
        [FieldOffset(4)]
        private string mStringValue;
        [FieldOffset(4)]
        private object mObjectValue;
        [FieldOffset(8)]
        private bool mBoolValue;
        [FieldOffset(8)]
        private double mDoubleValue;

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

        public Type type
        {
            get
            {
                return mType;
            }
            private set
            {
                mType = value;
            }
        }

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
