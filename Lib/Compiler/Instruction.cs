namespace SharpExpressions.Compiler
{
    class Instruction
    {
        public int numOperands;
        public execute execute;
        public convert[] converters;
    }
}
