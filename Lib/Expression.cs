using SharpExpressions.parser;

namespace SharpExpressions
{
    public class Expression
    {
        private string mExpression;
        private CompiledExpression mCompiled;
        private Registry mRegistry = new Registry();

        public Expression()
        {
        }

        public Expression(string expr)
        {
            create(expr);
        }

        public void addSymbol(string key, object value)
        {
            mRegistry.identifiers[key] = value;
        }

        public void addType(string key, System.Type type)
        {
            mRegistry.types[key] = type;
        }

        public void create(string expr)
        {
            mExpression = expr;
        }

        public void compile()
        {
            if (!string.IsNullOrEmpty(mExpression))
            {
                mCompiled = Compiler.compile(mExpression, mRegistry);
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
                return Executor.execute(mCompiled, mRegistry);
            }
            return null;
        }
    }
}
