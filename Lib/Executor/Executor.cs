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

using System.Collections.Generic;

namespace SharpExpressions.Executor
{
    class Executor
    {
        public static object execute(IEnumerable<Instruction> instructions, Registry registry)
        {
            Stack<Value> stack = new Stack<Value>();
            Value[] operands = new Value[16];
            Value result = new Value();

            foreach (Instruction instruction in instructions)
            {
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
