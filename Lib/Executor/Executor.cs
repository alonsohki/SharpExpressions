using System.Collections.Generic;

namespace SharpExpressions.Executor
{
    class Executor
    {
        public static object execute(Compiler.CompiledExpression expr, Registry registry)
        {
            Stack<Value> stack = new Stack<Value>();
            Value[] operands = new Value[16];
            Value result = new Value();

            for (int i = 0; i < expr.instructions.Length; ++i)
            {
                var instruction = expr.instructions[i];

                // Get the instruction operands
                if (instruction.numOperands > 0)
                {
                    for (int o = instruction.numOperands-1; o >= 0; --o)
                    {
                        operands[o] = stack.Pop();
                    }

                    if (instruction.converters != null)
                    {
                        for (int o = 0; o < instruction.numOperands; ++o)
                        {
                            var converter = instruction.converters[o];
                            if (converter != null)
                            {
                                converter(ref operands[o]);
                            }
                        }
                    }
                }

                // Execute it
                instruction.execute(operands, ref result);
                stack.Push(result);
            }

            return stack.Pop().value;
        }
    }
}
