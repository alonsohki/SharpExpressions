using System;
using System.Collections.Generic;

namespace SharpExpressions
{
    class Executor
    {
        public static object execute(Queue queue, Registry registry)
        {
            float a;
            float b;
            Stack<object> args = new Stack<object>();

            foreach (var entry in queue)
            {
                switch (entry.type)
                {
                    case Entry.Type.Value:
                    case Entry.Type.Identifier:
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

                        }
                        break;
                    }
                }
            }

            return solveValue(args.Pop(), registry);
        }

        private static float solveValue(object obj, Registry registry)
        {
            if (obj.GetType() == typeof(float))
            {
                return (float)obj;
            }
            else if (obj.GetType() == typeof(string))
            {
                object value = null;

                // Check if it's an identifier
                registry.identifiers.TryGetValue(obj.ToString(), out value);
                if (value == null || !IsNumericType(value))
                {
                    throw new System.Exception();
                }
                return (float)Convert.ToDouble(value);
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
