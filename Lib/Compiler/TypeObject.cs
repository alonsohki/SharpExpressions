namespace SharpExpressions.Compiler
{
    static class TypeObject
    {
        public static Types.TypeDefinition definition = new Types.TypeDefinition()
        {
            convert = (ref Value v) =>
            {
                v.objectValue = v.value;
            },
        };
    }
}
