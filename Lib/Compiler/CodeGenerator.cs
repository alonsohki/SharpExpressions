using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpExpressions.Compiler
{
    class CodeGenerator
    {
        public static CompiledExpression generate(Parser.Queue queue, Registry registry)
        {
            var types = Types.types;
            Stack<Entry> work = new Stack<Entry>();
            Queue<Instruction> instructions = new Queue<Instruction>();

            foreach (var entry in queue)
            {
                switch (entry.type)
                {
                    case Entry.Type.Double:
                    {
                        Instruction instruction = new Instruction();
                        double value = (double)entry.value;
                        work.Push(new Entry { type = Entry.Type.Double });
                        instruction.execute = (Value[] _, ref Value result) => result.doubleValue = value;
                        instructions.Enqueue(instruction);
                        break;
                    }

                    case Entry.Type.Boolean:
                    {
                        Instruction instruction = new Instruction();
                        bool value = (bool)entry.value;
                        work.Push(new Entry { type = Entry.Type.Boolean });
                        instruction.execute = (Value[] _, ref Value result) => result.boolValue = value;
                        instructions.Enqueue(instruction);
                        break;
                    }

                    case Entry.Type.String:
                    {
                        Instruction instruction = new Instruction();
                        string value = (string)entry.value;
                        work.Push(new Entry { type = Entry.Type.String });
                        instruction.execute = (Value[] _, ref Value result) => result.stringValue = value;
                        instructions.Enqueue(instruction);
                        break;
                    }

                    case Entry.Type.Identifier:
                    {
                        string identifier = (string)entry.value;
                        work.Push(new Entry { type = Entry.Type.Identifier, value = identifier });
                        break;
                    }

                    case Entry.Type.Operator:
                    {
                        bool applied = false;
                        string op = "";
                        Entry.Type targetType = Entry.Type.Unknown;

                        switch ((Parser.Operator)entry.value)
                        {
                            case Parser.Operator.Add:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "add";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].add, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Sub:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "substract";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].sub, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Mul:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "multiply";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].mul, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Div:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "divide";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].div, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Pow:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "power";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].pow, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Negate:
                            {
                                Entry param0 = work.Pop();
                                op = "negate";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 1, targetType, types[targetType].negate, types[targetType].convert, param0);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.LessThan:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "less than";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].lessThan, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.LessOrEqual:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "less or equal";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].lessOrEqual, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.GreaterThan:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "greater than";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].greaterThan, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.GreaterOrEqual:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "greater or equal";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].greaterOrEqual, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.Equals:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "equals";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].equals, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.NotEquals:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "not equals";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].notEquals, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.And:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "and";
                                targetType = Entry.Type.Boolean;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].and, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.Or:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                op = "or";
                                targetType = Entry.Type.Boolean;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].or, types[targetType].convert, param0, param1);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.Not:
                            {
                                Entry param0 = work.Pop();
                                op = "not";
                                targetType = Entry.Type.Boolean;
                                applied = setInstruction(instructions, 1, targetType, types[targetType].not, types[targetType].convert, param0);
                                work.Push(new Entry { type = Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.MemberAccess:
                            {
                                Entry param1 = work.Pop();
                                Entry param0 = work.Pop();
                                Entry result;
                                applied = memberAccess(instructions, registry, param0, param1, out result);
                                work.Push(result);
                                break;
                            }
                        }

                        if (!applied)
                        {
                            throw new CompilerException("I don't know how to operate '" + op + "' on type " + targetType);
                        }
                        break;
                    }
                }
            }

            return new CompiledExpression()
            {
                instructions = instructions.ToArray()
            };
        }

        private static bool setInstruction(Queue<Instruction> instructions, int operandCount, Entry.Type expectedType, execute executor, convert converter, params Entry[] operands)
        {
            if (executor != null)
            {
                Instruction instruction = new Instruction();
                instruction.numOperands = operandCount;
                instruction.execute = executor;

                convert[] converters = null;
                for (int i = 0; i < operandCount; ++i)
                {
                    if (operands[i].type != expectedType)
                    {
                        if (converters == null)
                        {
                            converters = new convert[operandCount];
                        }
                        converters[i] = converter;
                    }
                }

                instruction.converters = converters;
                instructions.Enqueue(instruction);

                return true;
            }
            return false;
        }

        private static bool memberAccess(Queue<Instruction> instructions, Registry registry, Entry param0, Entry param1, out Entry result)
        {
            if (param1.type != Entry.Type.Identifier)
            {
                throw new CompilerException("Trying to access a field without an identifier");
            }

            string fieldName = (string)param1.value;

            if (param0.type == Entry.Type.Identifier)
            {
                object accessed;
                Type type;
                string accessedName = (string)param0.value;

                // Solve the first param from the registry
                if (registry.identifiers.TryGetValue(accessedName, out accessed))
                {
                    Instruction instruction = new Instruction();
                    instruction.execute = (Value[] v, ref Value res) => res.objectValue = accessed;
                    instructions.Enqueue(instruction);

                    accessObject(instructions, accessed, fieldName, out result);
                }
                else if (registry.types.TryGetValue(accessedName, out type))
                {
                    accessType(instructions, type, fieldName, out result);
                }
                else
                {
                    throw new CompilerException("Cannot find identifier " + (string)param0.value + " in the registry");
                }
            }
            else if (param0.type == Entry.Type.Object)
            {
                accessObject(instructions, param0.value, fieldName, out result);
            }
            else
            {
                throw new CompilerException("Trying to access a field from a non-object instance");
            }

            return true;
        }

        private static void accessObject(Queue<Instruction> instructions, object accessed, string fieldName, out Entry result)
        {
            result = new Entry();

            Type type = accessed.GetType();
            PropertyInfo propInfo = type.GetProperty(fieldName);
            if (propInfo == null)
            {
                FieldInfo fieldInfo = type.GetField(fieldName);
                if (fieldInfo == null)
                {
                    throw new CompilerException("Cannot field the field '" + fieldName + "' in the object");
                }

                if (fieldInfo.FieldType == typeof(string))
                {
                    Instruction instruction = new Instruction();
                    instruction.execute = (Value[] v, ref Value res) =>
                    {
                        res.stringValue = fieldInfo.GetValue(v[0].objectValue) as string;
                    };
                    instruction.numOperands = 1;
                    instructions.Enqueue(instruction);

                    result = new Entry { type = Entry.Type.String };
                }
            }
            else
            {
                if (propInfo.PropertyType == typeof(string))
                {
                    Instruction instruction = new Instruction();
                    instruction.execute = (Value[] v, ref Value res) =>
                    {
                        res.stringValue = propInfo.GetGetMethod().Invoke(v[0].objectValue, null) as string;
                    };
                    instruction.numOperands = 1;
                    instructions.Enqueue(instruction);

                    result = new Entry { type = Entry.Type.String };
                }
            }
        }

        private static void accessType(Queue<Instruction> instructions, Type type, string fieldName, out Entry result)
        {
            result = new Entry();
        }
    }
}
