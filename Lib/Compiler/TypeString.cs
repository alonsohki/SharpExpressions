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
