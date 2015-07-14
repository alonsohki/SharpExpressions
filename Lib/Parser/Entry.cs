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

namespace SharpExpressions.Parser
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
        public bool isStatic { get; set; }

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
