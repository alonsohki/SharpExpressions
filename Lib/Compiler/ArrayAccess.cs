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

namespace SharpExpressions.Compiler
{
    static class ArrayAccess
    {
        public static bool access(Queue<Instruction> instructions, Stack<Entry> work, Type targetType, out Entry result)
        {
            PropertyInfo itemProperty = null;
            MethodInfo itemGetMethod = null;
            int numParams;

            if (targetType.IsArray)
            {
                numParams = targetType.GetArrayRank();
            }
            else if ((itemProperty = targetType.GetProperty("Item")) != null)
            {
                itemGetMethod = itemProperty.GetGetMethod();
                numParams = itemGetMethod.GetParameters().Length;
            }
            else
            {
                throw new CompilerException("Trying to access " + targetType.FullName + " which is not an array or doesn't have the [] operator overloaded");
            }

            Entry[] parameters = makeParameters(work, numParams);
            if (itemGetMethod != null)
            {
                // Treat it as a function call
                return MethodInvoke.invoke(instructions, itemGetMethod, false, parameters, out result);
            }
            else
            {
                // Treat it as an array
                Instruction instruction = new Instruction();
                instruction.numOperands = numParams + 1;

                convert[] converters = new convert[numParams + 1];
                for (int i = 0; i < numParams; ++i)
                {
                    converters[numParams - i - 1] = (ref Value v) => v.objectValue = Convert.ToInt32(v.value);
                }
                instruction.converters = converters;

                int[] lambdaIndices = new int[numParams];
                Type arrayElementType = targetType.GetElementType();
                Entry.Type resultType = Entry.fromSystemType(arrayElementType);

                if (resultType == Entry.Type.Unknown)
                {
                    throw new CompilerException("Unknown element type for array " + targetType.FullName);
                }

                switch (resultType)
                {
                    case Entry.Type.Boolean:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            Array target = (Array)v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaIndices[j] = (int)v[i].objectValue;
                            res.boolValue = (bool)target.GetValue(lambdaIndices);
                        };
                        break;

                    case Entry.Type.Double:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            Array target = (Array)v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaIndices[j] = (int)v[i].objectValue;
                            res.doubleValue = Convert.ToDouble(target.GetValue(lambdaIndices));
                        };
                        break;

                    case Entry.Type.String:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            Array target = (Array)v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaIndices[j] = (int)v[i].objectValue;
                            res.stringValue = target.GetValue(lambdaIndices) as string;
                        };
                        break;

                    case Entry.Type.Object:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            Array target = (Array)v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaIndices[j] = (int)v[i].objectValue;
                            res.objectValue = target.GetValue(lambdaIndices);
                        };
                        break;
                }

                instructions.Enqueue(instruction);
            }

            result = new Entry();
            return true;
        }

        private static Entry[] makeParameters(Stack<Entry> work, int count)
        {
            Entry[] parameters = new Entry[count];
            for (int i = 0; i < count; ++i)
            {
                parameters[i] = work.Pop();
            }
            return parameters;
        }
    }
}
