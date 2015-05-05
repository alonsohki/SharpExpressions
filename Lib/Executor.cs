using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpExpressions
{
    class Executor
    {
        public static object execute(Queue queue, Registry registry)
        {
            double a;
            double b;
            bool l;
            bool r;
            Stack<object> args = new Stack<object>();

            foreach (var entry in queue)
            {
                switch (entry.type)
                {
                    case Entry.Type.Value:
                    case Entry.Type.Identifier:
                    case Entry.Type.Boolean:
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
                if (!registry.identifiers.TryGetValue((string)obj, out value))
                {
                    throw new System.Exception();
                }

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
        }
    }
}
