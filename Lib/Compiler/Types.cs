using System.Collections.Generic;

namespace SharpExpressions.Compiler
{
    delegate void convert(ref Value value);
    delegate void execute(Value[] values, ref Value result);

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
        }

        private static Dictionary<Value.Type, TypeDefinition> msTypes;
        public static Dictionary<Value.Type, TypeDefinition> types
        {
            get
            {
                if (msTypes == null)
                {
                    msTypes = new Dictionary<Value.Type, TypeDefinition>();
                    msTypes[Value.Type.Double] = TypeDouble.definition;
                    msTypes[Value.Type.Boolean] = TypeBool.definition;
                }
                return msTypes;
            }
        }
    }
}
