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
            convert = (Value v) =>
            {
            },

            add = (Value[] v, out Value res) =>
            {
                res = null;
            },

            sub = (Value[] v, out Value res) =>
            {
                res = null;
            }
        };
    }
}
