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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpExpressions.Compiler
{
    static class TypeString
    {
        public static Types.TypeDefinition definition = new Types.TypeDefinition()
        {
            convert = (ref Value v) =>
            {
                switch (v.type)
                {
                    case Value.Type.Boolean:
                        v.stringValue = v.boolValue ? "True" : "False";
                        break;
                    case Value.Type.String:
                        break;
                    case Value.Type.Double:
                        v.stringValue = v.doubleValue.ToString();
                        break;
                    case Value.Type.Object:
                        v.stringValue = v.objectValue.ToString();
                        break;
                }
            },


            add = (Value[] v, ref Value res) =>
            {
                res.stringValue = v[0].stringValue + v[1].stringValue;
            },


            equals = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].stringValue == v[1].stringValue;
            },


            notEquals = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].stringValue != v[1].stringValue;
            },
        };
    }
}
