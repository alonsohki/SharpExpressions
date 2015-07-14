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

using System.Collections.Generic;

namespace SharpExpressions.LambdaCompiler
{
    static class Types
    {
        public struct TypeDefinition
        {
            public convert convert;
            public execute add;
            public execute sub;
            public execute mul;
            public execute div;
            public execute pow;
            public execute negate;
            public execute lessThan;
            public execute lessOrEqual;
            public execute greaterThan;
            public execute greaterOrEqual;
            public execute equals;
            public execute notEquals;
            public execute and;
            public execute or;
            public execute not;
            public execute ternary;
        }

        private static Dictionary<Parser.Entry.Type, TypeDefinition> msTypes;
        public static Dictionary<Parser.Entry.Type, TypeDefinition> types
        {
            get
            {
                if (msTypes == null)
                {
                    msTypes = new Dictionary<Parser.Entry.Type, TypeDefinition>();
                    msTypes[Parser.Entry.Type.Double] = TypeDouble.definition;
                    msTypes[Parser.Entry.Type.Boolean] = TypeBool.definition;
                    msTypes[Parser.Entry.Type.String] = TypeString.definition;
                    msTypes[Parser.Entry.Type.Object] = TypeObject.definition;
                    msTypes[Parser.Entry.Type.Type] = TypeType.definition;
                }
                return msTypes;
            }
        }
    }
}
