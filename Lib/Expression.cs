using Antlr.Runtime;
using SharpExpressions.parser;

namespace SharpExpressions
{
    public class Expression
    {
        SharpExpressionsParser mParser;
        private Queue mCompiled;

        public Expression()
        {
        }

        public Expression(string expr)
        {
            create(expr);
        }

        public void create(string expr)
        {
            ANTLRStringStream stream = new ANTLRStringStream(expr);
            SharpExpressionsLexer lexer = new SharpExpressionsLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            mParser = new SharpExpressionsParser(tokens);
        }

        public void compile()
        {
            if (mParser != null)
            {
                mCompiled = mParser.eval();
            }
        }

        public object eval()
        {
            if (mCompiled == null)
            {
                compile();
            }
            if (mCompiled != null)
            {
                return Executor.execute(mCompiled);
            }
            return null;
        }
    }
}
