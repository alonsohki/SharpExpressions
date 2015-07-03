using System.Collections.Generic;

namespace SharpExpressions.Compiler
{
    class CodeGenerator
    {
        public static CompiledExpression generate(Parser.Queue queue, Registry registry)
        {
            var types = Types.types;
            Stack<Value> work = new Stack<Value>();
            Queue<Instruction> instructions = new Queue<Instruction>();

            foreach (var entry in queue)
            {
                Instruction instruction = new Instruction();

                switch (entry.type)
                {
                    case Parser.Entry.Type.Double:
                    {
                        double value = (double)entry.value;
                        work.Push(new Value { doubleValue = value });
                        instruction.execute = (Value[] _, ref Value result) => result.doubleValue = value;
                        break;
                    }

                    case Parser.Entry.Type.Boolean:
                    {
                        bool value = (bool)entry.value;
                        work.Push(new Value { boolValue = value });
                        instruction.execute = (Value[] _, ref Value result) => result.boolValue = value;
                        break;
                    }

                    case Parser.Entry.Type.String:
                    {
                        string value = (string)entry.value;
                        work.Push(new Value { stringValue = value });
                        instruction.execute = (Value[] _, ref Value result) => result.stringValue = value;
                        break;
                    }

                    case Parser.Entry.Type.Operator:
                    {
                        switch ((Parser.Operator)entry.value)
                        {
                            case Parser.Operator.Add:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                setInstruction(instruction, 2, param0.type, types[param0.type].add, types[param0.type].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                        }
                        break;
                    }
                }

                instructions.Enqueue(instruction);
            }

            return new CompiledExpression()
            {
                instructions = instructions.ToArray()
            };
        }

        private static void setInstruction(Instruction instruction, int operandCount, Value.Type expectedType, execute executor, convert converter, params Value[] operands)
        {
            if (executor != null)
            {
                instruction.numOperands = operandCount;
                instruction.execute = executor;

                bool any = false;
                var converters = new convert[operandCount];
                for (int i = 0; i < operandCount; ++i)
                {
                    if (operands[i].type != expectedType)
                    {
                        converters[i] = converter;
                        any = true;
                    }
                    else
                    {
                        converters[i] = null;
                    }
                }

                if (any)
                {
                    instruction.converters = converters;
                }
            }
        }
    }
}
