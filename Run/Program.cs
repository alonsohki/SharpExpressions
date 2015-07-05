using SharpExpressions;
using System;

namespace SharpExpressions
{
    public class Class1
    {
        private class Test
        {
            public double fn(float v)
            {
                return 1.0 + v;
            }
        }

        public static void Main(string[] args)
        {
            do
            {
                Console.Write("> ");
                string line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }

                try
                {
                    Expression expr = new Expression(line);
                    expr.addSymbol("vars", new { a = 10, b = 20, c = "Hello, world!", d = new { x = 0f, y = "Good bye!", z = -10.0 }, test = new Test() });
                    expr.addSymbol("k", 10.0);
                    expr.addType("Math", typeof(Math));
                    Console.WriteLine(expr.eval());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            while (true);
        }
    }
}
