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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpExpressions.LambdaCompiler
{
    class CodeGenerator
    {
        public static Instruction[] generate(Parser.Queue queue, Registry registry)
        {
            var types = Types.types;
            Stack<Parser.Entry> work = new Stack<Parser.Entry>();
            Queue<Instruction> instructions = new Queue<Instruction>();

            for (var node = queue.First; node != null; node = node.Next)
            {
                var entry = node.Value;
                switch (entry.type)
                {
                    case Parser.Entry.Type.Double:
                    {
                        Instruction instruction = new Instruction();
                        double value = (double)entry.value;
                        work.Push(new Parser.Entry { type = Parser.Entry.Type.Double, value = value, isConstant = true });
                        instruction.execute = (Value[] _, ref Value result) => result.doubleValue = value;
                        instructions.Enqueue(instruction);
                        break;
                    }

                    case Parser.Entry.Type.Boolean:
                    {
                        Instruction instruction = new Instruction();
                        bool value = (bool)entry.value;
                        work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean, value = value, isConstant = true });
                        instruction.execute = (Value[] _, ref Value result) => result.boolValue = value;
                        instructions.Enqueue(instruction);
                        break;
                    }

                    case Parser.Entry.Type.String:
                    {
                        Instruction instruction = new Instruction();
                        string value = (string)entry.value;
                        work.Push(new Parser.Entry { type = Parser.Entry.Type.String, value = value, isConstant = true });
                        instruction.execute = (Value[] _, ref Value result) => result.stringValue = value;
                        instructions.Enqueue(instruction);
                        break;
                    }

                    case Parser.Entry.Type.Object:
                    {
                        Instruction instruction = new Instruction();
                        object value = entry.value;
                        work.Push(new Parser.Entry { type = Parser.Entry.Type.Object, value = value, isConstant = true });
                        instruction.execute = (Value[] _, ref Value result) => result.objectValue = value;
                        instructions.Enqueue(instruction);
                        break;
                    }

                    case Parser.Entry.Type.Identifier:
                    {
                        Parser.Entry result;
                        string identifier = (string)entry.value;
                        var next = node.Next;
                        if (next != null && next.Value.type == Parser.Entry.Type.Operator && (Parser.Operator)next.Value.value == Parser.Operator.MemberAccess)
                        {
                            result = new Parser.Entry { type = Parser.Entry.Type.Identifier, value = identifier };
                        }
                        else
                        {
                            MemberAccess.identifierAccess(instructions, registry, identifier, out result);
                        }
                        work.Push(result);
                        break;
                    }

                    case Parser.Entry.Type.Operator:
                    {
                        bool applied = false;
                        string op = "";
                        Parser.Entry.Type targetType = Parser.Entry.Type.Unknown;

                        switch ((Parser.Operator)entry.value)
                        {
                            case Parser.Operator.Add:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "add";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].add, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Sub:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "substract";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].sub, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Mul:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "multiply";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].mul, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Div:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "divide";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].div, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Pow:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "power";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].pow, types[targetType].convert, param0, param1);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.Negate:
                            {
                                Parser.Entry param0 = work.Pop();
                                op = "negate";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 1, targetType, types[targetType].negate, types[targetType].convert, param0);
                                work.Push(param0);
                                break;
                            }
                            case Parser.Operator.LessThan:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "less than";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].lessThan, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.LessOrEqual:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "less or equal";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].lessOrEqual, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.GreaterThan:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "greater than";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].greaterThan, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.GreaterOrEqual:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "greater or equal";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].greaterOrEqual, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.Equals:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "equals";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].equals, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.NotEquals:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "not equals";
                                targetType = param0.type;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].notEquals, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.And:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "and";
                                targetType = Parser.Entry.Type.Boolean;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].and, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.Or:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                op = "or";
                                targetType = Parser.Entry.Type.Boolean;
                                applied = setInstruction(instructions, 2, targetType, types[targetType].or, types[targetType].convert, param0, param1);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.Not:
                            {
                                Parser.Entry param0 = work.Pop();
                                op = "not";
                                targetType = Parser.Entry.Type.Boolean;
                                applied = setInstruction(instructions, 1, targetType, types[targetType].not, types[targetType].convert, param0);
                                work.Push(new Parser.Entry { type = Parser.Entry.Type.Boolean });
                                break;
                            }
                            case Parser.Operator.Ternary:
                            {
                                Parser.Entry condition = work.Pop();
                                Parser.Entry ifFalse = work.Pop();
                                Parser.Entry ifTrue = work.Pop();
                                op = "ternary operator";
                                targetType = ifTrue.type;
                                convert[] converters = null;
                                if (ifFalse.type != targetType)
                                {
                                    converters = new convert[3];
                                    converters[1] = types[targetType].convert;
                                }
                                if (condition.type != Parser.Entry.Type.Boolean)
                                {
                                    if (converters == null)
                                    {
                                        converters = new convert[3];
                                    }
                                    converters[2] = types[Parser.Entry.Type.Boolean].convert;
                                }
                                Instruction instruction = new Instruction()
                                {
                                    numOperands = 3,
                                    converters = converters,
                                    execute = types[targetType].ternary,
                                };
                                instructions.Enqueue(instruction);
                                applied = true;
                                work.Push(new Parser.Entry { type = targetType });
                                break;
                            }
                            case Parser.Operator.MemberAccess:
                            {
                                Parser.Entry param1 = work.Pop();
                                Parser.Entry param0 = work.Pop();
                                Parser.Entry result;
                                bool keepObject;
                                op = "memberaccess";
                                applied = MemberAccess.memberAccess(instructions, registry, param0, param1, out keepObject, out result);
                                targetType = result.type;
                                if (keepObject)
                                {
                                    work.Push(param0);
                                }
                                work.Push(result);
                                break;
                            }
                            case Parser.Operator.Call:
                            {
                                Parser.Entry method = work.Pop();
                                if (method.type != Parser.Entry.Type.Method)
                                {
                                    throw new CompilerException("Expected method to call, but got " + method.type + " instead");
                                }

                                MethodInfo methodInfo = method.value as MethodInfo;
                                if (!methodInfo.IsStatic)
                                {
                                    Parser.Entry targetEntry = work.Pop();
                                    if (targetEntry.type != Parser.Entry.Type.Type)
                                    {
                                        throw new CompilerException("Expected an object to the left of the method invoke " + methodInfo.Name);
                                    }
                                }

                                Parser.Entry result;
                                var parameters = makeParameters(work, methodInfo.GetParameters().Length);
                                applied = MethodInvoke.invoke(instructions, methodInfo, methodInfo.IsStatic, parameters, out result);
                                targetType = result.type;
                                op = "call";
                                work.Push(result);
                                break;
                            }
                            case Parser.Operator.ArrayAccess:
                            {
                                Parser.Entry target = work.Pop();
                                if (target.type != Parser.Entry.Type.Type || target.isStatic)
                                {
                                    throw new CompilerException("Expected object for array access, but got " + target.type + " instead");
                                }

                                op = "array access";
                                Parser.Entry result;
                                applied = ArrayAccess.access(instructions, work, (Type)target.value, out result);
                                targetType = result.type;
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

            if (work.Count != 1)
            {
                throw new CompilerException("Bad expression: Mismatched operands");
            }
            else if (work.Peek().type == Parser.Entry.Type.Type)
            {
                var entry = work.Pop();
                Type type = entry.value as Type;
                if (entry.isStatic)
                {
                    instructions.Enqueue(new Instruction
                    {
                        execute = (Value[] v, ref Value res) => res.objectValue = type,
                    });
                }
            }

            return instructions.ToArray();
        }

        private static bool setInstruction(Queue<Instruction> instructions, int operandCount, Parser.Entry.Type expectedType, execute executor, convert converter, params Parser.Entry[] operands)
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

        private static Parser.Entry[] makeParameters(Stack<Parser.Entry> work, int count)
        {
            Parser.Entry[] parameters = new Parser.Entry[count];
            for (int i = 0; i < count; ++i)
            {
                parameters[i] = work.Pop();
            }
            return parameters;
        }
    }
}
