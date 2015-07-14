//--------------------------------------------------------------------------------------
// Copyright 2015 - Alberto Alonso
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

namespace SharpExpressions.LambdaCompiler
{
    struct Value
    {
        public enum Type
        {
            None,
            Double,
            String,
            Boolean,
            Object,
        }

        private Type mType;
        private string mStringValue;
        private object mObjectValue;
        private bool mBoolValue;
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
