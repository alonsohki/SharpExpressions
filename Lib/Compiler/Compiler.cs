using System;
using SharpExpressions.Parser;
using Antlr.Runtime;

namespace SharpExpressions.Compiler
{
    class Compiler
    {
        public static CompiledExpression compile(string expr, Registry registry)
        {
            ANTLRStringStream stream = new ANTLRStringStream(expr);
            SharpExpressionsLexer lexer = new SharpExpressionsLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            SharpExpressionsParser parser = new SharpExpressionsParser(tokens);
            return CodeGenerator.generate(parser.eval(), registry);
        }
    }
}
