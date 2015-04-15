using Antlr.Runtime;
using System;
using System.Globalization;

using SharpExpressions;

namespace SharpExpressions.parser
{
    partial class SharpExpressionsParser
    {
        private Queue mQueue;

        public override void ReportError(RecognitionException e)
        {
            base.ReportError(e);
        }

        private void clear_stack()
        {
            mQueue = new Queue();
        }

        private void push_literal(string value)
        {
            double d = double.Parse(value, CultureInfo.InvariantCulture);
            mQueue.Enqueue(new Entry { type = Entry.Type.Value, value = d });
        }

        private void push_operator(Operator op)
        {
            mQueue.Enqueue(new Entry { type = Entry.Type.Operator, value = op });
        }

        private void push_identifier(string identifier)
        {
            mQueue.Enqueue(new Entry { type = Entry.Type.Identifier, value = identifier });
        }

        private void push_stack(Queue from)
        {
            foreach (var entry in from)
            {
                mQueue.Enqueue(entry);
            }
        }
    }
}
