using SharpExpressions;
using System;

namespace SharpExpressions
{
    public class Class1
    {
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
                    expr.addSymbol("var", 16);
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
