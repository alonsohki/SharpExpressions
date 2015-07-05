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
                var expectedType = getEntryType(paramType);

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
                    converters[i + targetOffset] = types[expectedType].convert;
                }
            }
            instruction.converters = converters;

            object[] lambdaParams = new object[numParams];
            Entry.Type returnType = getEntryType(methodInfo.ReturnType);

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
                                lambdaParams[j] = v[i].value;
                            res.boolValue = (bool)methodInfo.Invoke(null, lambdaParams);
                        };
                        break;
                    case Entry.Type.Double:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = v[i].value;
                            res.doubleValue = Convert.ToDouble(methodInfo.Invoke(null, lambdaParams));
                        };
                        break;
                    case Entry.Type.String:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = v[i].value;
                            res.stringValue = methodInfo.Invoke(null, lambdaParams) as string;
                        };
                        break;
                    case Entry.Type.Object:
                        instruction.execute = (Value[] v, ref Value res) =>
                        {
                            for (int i = 0, j = numParams - 1; i < numParams; ++i, --j)
                                lambdaParams[j] = v[i].value;
                            res.objectValue = methodInfo.Invoke(null, lambdaParams);
                        };
                        break;
                }
            }
            else
            {
                instruction.execute = (Value[] v, ref Value res) =>
                {
                    object obj = v[0];
                };
            }

            instructions.Enqueue(instruction);
            result = new Entry { type = returnType };
            return true;
        }


        private static Entry.Type getEntryType(Type systemType)
        {
            if (systemType == typeof(bool))
            {
                return Entry.Type.Boolean;
            }
            else if (systemType == typeof(string))
            {
                return Entry.Type.String;
            }
            else if (systemType == typeof(short) || systemType == typeof(int) || systemType == typeof(long) || systemType == typeof(float) || systemType == typeof(double))
            {
                return Entry.Type.Double;
            }
            else
            {
                return Entry.Type.Object;
            }
        }
    }
}
