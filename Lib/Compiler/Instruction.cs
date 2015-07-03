using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpExpressions.Compiler
{
    delegate void convert(Value value);
    delegate void execute(Value[] values);

    class Instruction
    {
        public convert[] converters;
        public execute execute;
    }
}
