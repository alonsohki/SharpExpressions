//--------------------------------------------------------------------------------------
// Copyright 2015 - Alberto Alonso
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using SharpExpressions.Parser;
using SharpExpressions.Compiler;
using SharpExpressions.Executor;

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
                mCompiled = Compiler.Compiler.compile(mExpression, mRegistry);
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
                return Executor.Executor.execute(mCompiled, mRegistry);
            }
            return null;
        }
    }
}
