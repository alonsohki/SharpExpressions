using System;

namespace SharpExpressions.Compiler
{
    public class CompilerException : Exception
    {
        public CompilerException(string reason)
            : base(reason)
        {
        }
    }
}
