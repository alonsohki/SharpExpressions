using System.Collections.Generic;

namespace SharpExpressions.Compiler
{
    delegate void convert(Value value);
    delegate void execute(Value[] values, ref Value result);

    static class Types
    {
        public struct TypeDefinition
        {
            public convert convert;
            public execute add;
            public execute sub;
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
                }
                return msTypes;
            }
        }
    }
}
