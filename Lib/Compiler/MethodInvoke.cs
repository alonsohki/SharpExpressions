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
    static class MethodInvoke
    {
        public static bool invoke(Queue<Instruction> instructions, MethodInfo methodInfo, object target, Entry[] parameters, out Entry result)
        {
            var types = Types.types;
            bool isStatic = target == null;
            int numParams = parameters.Length;
            int targetOffset = isStatic ? 0 : 1;

            Instruction instruction = new Instruction();
            instruction.numOperands = parameters.Length + targetOffset;
            ParameterInfo[] paramInfo = methodInfo.GetParameters();

            convert[] converters = null;
            for (int i = 0; i < numParams; ++i)
            {
                var paramType = paramInfo[i].ParameterType;
                var expectedType = Entry.fromSystemType(paramType);

                if (expectedType == Entry.Type.Unknown)
                {
                    throw new CompilerException("I don't know how to convert to type " + paramType.FullName + " for parameter " + i + " of method " + methodInfo.Name);
                }

                if (expectedType != parameters[i].type)
                {
                    if (converters == null)
                    {
                        converters = new convert[numParams + targetOffset];
                    }
                    converters[numParams - i - 1] = types[expectedType].convert;
                }
            }
            instruction.converters = converters;

            object[] lambdaParams = new object[numParams];
            Entry.Type returnType = Entry.fromSystemType(methodInfo.ReturnType);

            if (returnType == Entry.Type.Unknown)
            {
                throw new CompilerException("Unknown return type " + methodInfo.ReturnType.FullName + " for method " + methodInfo.Name);
            }

            if (isStatic)
            {
                switch (returnType)
                {
                    case Entry.Type.Boolean:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.boolValue = (bool)methodInfo.Invoke(null, lambdaParams);
                        };
                        break;
                    case Entry.Type.Double:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.doubleValue = Convert.ToDouble(methodInfo.Invoke(null, lambdaParams));
                        };
                        break;
                    case Entry.Type.String:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.stringValue = methodInfo.Invoke(null, lambdaParams) as string;
                        };
                        break;
                    case Entry.Type.Object:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.objectValue = methodInfo.Invoke(null, lambdaParams);
                        };
                        break;
                }
            }
            else
            {
                switch (returnType)
                {
                    case Entry.Type.Boolean:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            object obj = v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.boolValue = (bool)methodInfo.Invoke(obj, lambdaParams);
                        };
                        break;
                    case Entry.Type.Double:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            object obj = v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.doubleValue = Convert.ToDouble(methodInfo.Invoke(obj, lambdaParams));
                        };
                        break;
                    case Entry.Type.String:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            object obj = v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.stringValue = methodInfo.Invoke(obj, lambdaParams) as string;
                        };
                        break;
                    case Entry.Type.Object:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            object obj = v[numParams].objectValue;
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = convert(v[i].value, paramInfo[j].ParameterType);
                            res.objectValue = methodInfo.Invoke(obj, lambdaParams);
                        };
                        break;
                }
            }

            instructions.Enqueue(instruction);
            result = new Entry { type = returnType };
            return true;
        }


        private static object convert(object obj, Type targetType)
        {
            if (targetType == typeof(bool))
                return Convert.ToBoolean(obj);
            if (targetType == typeof(short))
                return Convert.ToInt16(obj);
            if (targetType == typeof(int))
                return Convert.ToInt32(obj);
            if (targetType == typeof(long))
                return Convert.ToInt64(obj);
            if (targetType == typeof(float))
                return Convert.ToSingle(obj);
            if (targetType == typeof(double))
                return Convert.ToDouble(obj);
            if (targetType == typeof(string))
                return Convert.ToString(obj);
            return obj;
        }
    }
}
