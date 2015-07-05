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
            public execute lessThan;
            public execute lessOrEqual;
            public execute greaterThan;
            public execute greaterOrEqual;
            public execute equals;
            public execute notEquals;
            public execute and;
            public execute or;
            public execute not;
        }

        private static Dictionary<Entry.Type, TypeDefinition> msTypes;
        public static Dictionary<Entry.Type, TypeDefinition> types
        {
            get
            {
                if (msTypes == null)
                {
                    msTypes = new Dictionary<Entry.Type, TypeDefinition>();
                    msTypes[Entry.Type.Double] = TypeDouble.definition;
                    msTypes[Entry.Type.Boolean] = TypeBool.definition;
                    msTypes[Entry.Type.String] = TypeString.definition;
                }
                return msTypes;
            }
        }
    }
}
