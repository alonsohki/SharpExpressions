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
