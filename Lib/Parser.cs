using Antlr.Runtime;
using System;
using System.Globalization;

namespace SharpExpressions.parser
{
    partial class SharpExpressionsParser
    {
        public override void ReportError(RecognitionException e)
        {
            base.ReportError(e);
        }

        private void push_literal(string value)
        {
            float f = float.Parse(value, CultureInfo.InvariantCulture);
            Console.WriteLine("Pushing literal: " + f);
        }

        private void push_operator(string op)
        {
            Console.WriteLine("Pushing operator: " + op);
        }

        private void push_identifier(string identifier)
        {
            Console.WriteLine("Pushing identifier: " + identifier);
        }
    }
}
