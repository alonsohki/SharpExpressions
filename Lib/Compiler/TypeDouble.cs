using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpExpressions.Compiler
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
        };
    }
}
