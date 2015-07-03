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
                                var expectedType = param0.type;

                                instruction.numOperands = 2;
                                instruction.execute = types[param0.type].add;

                                if (param1.type != expectedType)
                                {
                                    var converters = new convert[2];
                                    converters[0] = null;
                                    converters[1] = param1.type == expectedType ? null : types[param0.type].convert;
                                    instruction.converters = converters;
                                }
                                
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
    }
}
