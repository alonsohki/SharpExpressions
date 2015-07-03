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

        /*public static object execute(Queue queue, Registry registry)
        {
            double a;
            double b;
            bool l;
            bool r;
            MethodInfo m;
            ParameterInfo[] parameters;
            int i;
            Stack<object> args = new Stack<object>();

            foreach (var entry in queue)
            {
                switch (entry.type)
                {
                    case Entry.Type.Value:
                    case Entry.Type.Identifier:
                    case Entry.Type.Boolean:
                    case Entry.Type.String:
                        args.Push(entry.value);
                        break;

                    case Entry.Type.Operator:
                    {
                        switch ((Operator)entry.value)
                        {
                            case Operator.Add:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a + b);
                                break;

                            case Operator.Sub:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a - b);
                                break;

                            case Operator.Mul:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a * b);
                                break;

                            case Operator.Div:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a / b);
                                break;

                            case Operator.Pow:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(Math.Pow(a, b));
                                break;

                            case Operator.Negate:
                                a = solveValue(args.Pop(), registry);
                                args.Push(-a);
                                break;

                            case Operator.MemberAccess:
                                object field = args.Pop();
                                object obj = args.Pop();
                                object member = accessMember(obj, field, registry);
                                args.Push(member);
                                break;

                            case Operator.And:
                                r = solveBoolean(args.Pop(), registry);
                                l = solveBoolean(args.Pop(), registry);
                                args.Push(l && r);
                                break;

                            case Operator.Or:
                                r = solveBoolean(args.Pop(), registry);
                                l = solveBoolean(args.Pop(), registry);
                                args.Push(l || r);
                                break;

                            case Operator.GreaterOrEqual:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a >= b);
                                break;

                            case Operator.GreaterThan:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a > b);
                                break;

                            case Operator.LessOrEqual:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a <= b);
                                break;

                            case Operator.LessThan:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a < b);
                                break;

                            case Operator.Equals:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a == b);
                                break;

                            case Operator.NotEquals:
                                b = solveValue(args.Pop(), registry);
                                a = solveValue(args.Pop(), registry);
                                args.Push(a != b);
                                break;

                            case Operator.Not:
                                l = solveBoolean(args.Pop(), registry);
                                args.Push(!l);
                                break;

                            case Operator.Call:
                                m = solveMethod(args.Pop(), registry);
                                parameters = m.GetParameters();
                                if (args.Count < parameters.Length)
                                {
                                    throw new System.Exception();
                                }
                                else
                                {
                                    object[] values = new object[parameters.Length];
                                    for (i = 0; i < parameters.Length; ++i)
                                    {
                                        values[i] = args.Pop();
                                    }
                                    args.Push(m.Invoke(null, values));
                                }

                                break;
                        }
                        break;
                    }
                }
            }

            if (args.Count != 1)
            {
                throw new System.Exception();
            }

            return args.Pop();
        }

        private static MethodInfo solveMethod(object obj, Registry registry)
        {
            MethodInfo info = obj as MethodInfo;
            if (info == null)
                throw new System.Exception();
            return info;
        }

        private static object accessMember(object obj, object field, Registry registry)
        {
            // Solve the identifier
            if (obj.GetType() != typeof(string) || field.GetType() != typeof(string))
            {
                throw new System.Exception();
            }
            else
            {
                object value = null;
                System.Type type;

                if (registry.identifiers.TryGetValue((string)obj, out value))
                {
                    // Find the property with that name
                    string fieldName = (string)field;
                    PropertyInfo prop = value.GetType().GetProperty(fieldName);
                    if (prop == null)
                    {
                        FieldInfo fieldInfo = value.GetType().GetField(fieldName);
                        if (fieldInfo == null)
                        {
                            throw new System.Exception();
                        }
                        return fieldInfo.GetValue(value);
                    }
                    else
                    {
                        return prop.GetGetMethod().Invoke(value, null);
                    }
                }
                else if (registry.types.TryGetValue((string)obj, out type))
                {
                    // Check if the given member is a static property or field
                    var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                    foreach (var currentField in fields)
                    {
                        if (currentField.Name.Equals(field))
                        {
                            return currentField.GetValue(null);
                        }
                    }

                    var properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                    foreach (var currentProperty in properties)
                    {
                        if (currentProperty.Name.Equals(field))
                        {
                            return currentProperty.GetValue(null, null);
                        }
                    }

                    // Find a method in the type with that name
                    var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                    foreach (var currentMethod in methods)
                    {
                        if (currentMethod.Name.Equals(field))
                        {
                            return currentMethod;
                        }
                    }

                    throw new System.Exception();
                }
                else
                {
                    throw new System.Exception();
                }
            }
        }

        private static double solveValue(object obj, Registry registry)
        {
            if (IsNumericType(obj))
            {
                return Convert.ToDouble(obj);
            }
            else if (obj.GetType() == typeof(string))
            {
                object value = null;

                // Check if it's an identifier
                registry.identifiers.TryGetValue((string)obj, out value);
                if (value == null || !IsNumericType(value))
                {
                    throw new System.Exception();
                }
                return Convert.ToDouble(value);
            }
            else if (obj.GetType() == typeof(bool))
            {
                return (bool)obj ? 1 : 0;
            }
            else
            {
                throw new System.Exception();
            }
        }

        private static bool solveBoolean(object obj, Registry registry)
        {
            if (obj.GetType() == typeof(bool))
            {
                return (bool)obj;
            }
            else if (IsNumericType(obj))
            {
                return Convert.ToBoolean(obj);
            }
            else if (obj.GetType() == typeof(string))
            {
                object value = null;

                // Check if it's an identifier
                registry.identifiers.TryGetValue((string)obj, out value);
                if (value == null || !IsNumericType(value))
                {
                    throw new System.Exception();
                }
                return Convert.ToBoolean(value);
            }
            else
            {
                throw new System.Exception();
            }
        }

        private static bool IsNumericType(object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }*/
    }
}
