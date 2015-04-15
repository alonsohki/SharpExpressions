using System.Collections.Generic;

namespace SharpExpressions
{
    class Executor
    {
        public static object execute(Queue queue)
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
                                b = solveValue(args.Pop());
                                a = solveValue(args.Pop());
                                args.Push(a + b);
                                break;

                            case Operator.Sub:
                                b = solveValue(args.Pop());
                                a = solveValue(args.Pop());
                                args.Push(a - b);
                                break;

                            case Operator.Mul:
                                b = solveValue(args.Pop());
                                a = solveValue(args.Pop());
                                args.Push(a * b);
                                break;

                            case Operator.Div:
                                b = solveValue(args.Pop());
                                a = solveValue(args.Pop());
                                args.Push(a / b);
                                break;

                        }
                        break;
                    }
                }
            }

            return args.Pop();
        }

        private static float solveValue(object obj)
        {
            if (obj.GetType() == typeof(float))
            {
                return (float)obj;
            }
            else throw new System.Exception();
        }
    }
}
