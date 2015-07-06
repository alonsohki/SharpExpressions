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

namespace SharpExpressions.Compiler
{
    static class TypeBool
    {
        public static Types.TypeDefinition definition = new Types.TypeDefinition()
        {
            convert = (ref Value v) =>
            {
                switch (v.type)
                {
                    case Value.Type.Boolean:
                        break;
                    case Value.Type.String:
                        v.boolValue = Boolean.Parse(v.stringValue);
                        break;
                    case Value.Type.Double:
                        v.boolValue = v.doubleValue == 0.0 ? false : true;
                        break;
                    case Value.Type.Object:
                        v.boolValue = (bool)v.objectValue;
                        break;
                }
            },

            pow = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].boolValue ^ v[1].boolValue;
            },


            equals = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].boolValue == v[1].boolValue;
            },


            notEquals = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].boolValue != v[1].boolValue;
            },


            and = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].boolValue && v[1].boolValue;
            },


            or = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].boolValue || v[1].boolValue;
            },


            not = (Value[] v, ref Value res) =>
            {
                res.boolValue = !v[0].boolValue;
            },


            ternary = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[2].boolValue ? v[0].boolValue : v[1].boolValue;
            },
        };
    }
}
