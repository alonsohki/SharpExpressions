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

namespace SharpExpressions.LambdaCompiler
{
    static class TypeDouble
    {
        public static Types.TypeDefinition definition = new Types.TypeDefinition()
        {
            convert = (ref Value v) =>
            {
                switch (v.type)
                {
                    case Value.Type.Boolean:
                        v.doubleValue = v.boolValue ? 1f : 0f;
                        break;
                    case Value.Type.String:
                        v.doubleValue = Double.Parse(v.stringValue);
                        break;
                    case Value.Type.Double:
                        break;
                    case Value.Type.Object:
                        v.doubleValue = (double)v.objectValue;
                        break;
                }
            },


            add = (Value[] v, ref Value res) =>
            {
                res.doubleValue = v[0].doubleValue + v[1].doubleValue;
            },


            sub = (Value[] v, ref Value res) =>
            {
                res.doubleValue = v[0].doubleValue - v[1].doubleValue;
            },


            mul = (Value[] v, ref Value res) =>
            {
                res.doubleValue = v[0].doubleValue * v[1].doubleValue;
            },


            div = (Value[] v, ref Value res) =>
            {
                res.doubleValue = v[0].doubleValue / v[1].doubleValue;
            },


            pow = (Value[] v, ref Value res) =>
            {
                res.doubleValue = Math.Pow(v[0].doubleValue, v[1].doubleValue);
            },


            negate = (Value[] v, ref Value res) =>
            {
                res.doubleValue = -v[0].doubleValue;
            },


            lessThan = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].doubleValue < v[1].doubleValue;
            },


            lessOrEqual = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].doubleValue <= v[1].doubleValue;
            },


            greaterThan = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].doubleValue > v[1].doubleValue;
            },


            greaterOrEqual = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].doubleValue >= v[1].doubleValue;
            },


            equals = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].doubleValue == v[1].doubleValue;
            },


            notEquals = (Value[] v, ref Value res) =>
            {
                res.boolValue = v[0].doubleValue != v[1].doubleValue;
            },


            ternary = (Value[] v, ref Value res) =>
            {
                res.doubleValue = v[2].boolValue ? v[0].doubleValue : v[1].doubleValue;
            },
        };
    }
}
