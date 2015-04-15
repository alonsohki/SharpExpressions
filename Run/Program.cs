using Antlr.Runtime;
using SharpExpressions.parser;
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
                    ANTLRStringStream stream = new ANTLRStringStream(line);
                    SharpExpressionsLexer lexer = new SharpExpressionsLexer(stream);
                    CommonTokenStream tokens = new CommonTokenStream(lexer);
                    SharpExpressionsParser parser = new SharpExpressionsParser(tokens);
                    SharpExpressions.Queue stack = parser.eval();
                    foreach (var entry in stack)
                    {
                        Console.WriteLine(entry.value);
                    }
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
