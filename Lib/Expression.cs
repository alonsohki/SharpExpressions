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
using SharpExpressions.LambdaCompiler;

namespace SharpExpressions
{
    public class Expression
    {
        public string expression { get; private set; }
        private ICompiler mCompiler;
        private CompiledExpression mCompiled;
        private Registry mRegistry = new Registry();

        public Expression()
        {
            init();
        }

        public Expression(string expr)
        {
            init();
            create(expr);
        }

        private void init()
        {
            mCompiler = new LambdaCompiler.Compiler();
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
            expression = expr;
        }

        public void compile()
        {
            if (!string.IsNullOrEmpty(expression))
            {
                mCompiled = mCompiler.compile(expression, mRegistry);
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
                return mCompiled(mRegistry);
            }
            return null;
        }
    }
}
