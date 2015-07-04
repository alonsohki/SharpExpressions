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
                        string op = "";
                        Value.Type targetType = Value.Type.None;

                        switch ((Parser.Operator)entry.value)
                        {
                            case Parser.Operator.Add:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "add";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].add, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Sub:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "substract";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].sub, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Mul:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "multiply";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].mul, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Div:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "divide";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].div, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Pow:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "power";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].pow, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Negate:
                            {
                                Value param0 = work.Pop();
                                op = "negate";
                                targetType = param0.type;
                                setInstruction(instruction, 1, targetType, types[targetType].negate, types[targetType].convert, param0);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.LessThan:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "less than";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].lessThan, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.LessOrEqual:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "less or equal";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].lessOrEqual, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.GreaterThan:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "greater than";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].greaterThan, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.GreaterOrEqual:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "greater or equal";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].greaterOrEqual, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.Equals:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "equals";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].equals, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.NotEquals:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "not equals";
                                targetType = param0.type;
                                setInstruction(instruction, 2, targetType, types[targetType].notEquals, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.And:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "and";
                                targetType = Value.Type.Boolean;
                                setInstruction(instruction, 2, targetType, types[targetType].and, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.Or:
                            {
                                Value param1 = work.Pop();
                                Value param0 = work.Pop();
                                op = "or";
                                targetType = Value.Type.Boolean;
                                setInstruction(instruction, 2, targetType, types[targetType].or, types[targetType].convert, param0, param1);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                            case Parser.Operator.Not:
                            {
                                Value param0 = work.Pop();
                                op = "not";
                                targetType = Value.Type.Boolean;
                                setInstruction(instruction, 1, targetType, types[targetType].not, types[targetType].convert, param0);
                                work.Push(new Value { boolValue = false });
                                break;
                            }
                        }

                        if (instruction.execute == null)
                        {
                            throw new CompilerException("I don't know how to operate '" + op + "' on type " + targetType);
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
