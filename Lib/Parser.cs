using Antlr.Runtime;
using System;
using System.Globalization;

using SharpExpressions;

namespace SharpExpressions.parser
{
    partial class SharpExpressionsParser
    {
        public override void ReportError(RecognitionException e)
        {
            base.ReportError(e);
        }

        private void clear_stack(Queue queue)
        {
            queue.Clear();
        }

        private Queue new_queue()
        {
            return new Queue();
        }

        private Queue push_literal(Queue queue, string value)
        {
            double d = double.Parse(value, CultureInfo.InvariantCulture);
            queue.Enqueue(new Entry { type = Entry.Type.Value, value = d });
            return queue;
        }

        private Queue push_operator(Queue queue, Operator op)
        {
            queue.Enqueue(new Entry { type = Entry.Type.Operator, value = op });
            return queue;
        }

        private Queue push_operation(Queue queue, Operator op, Queue operand)
        {
            append_queue(queue, operand);
            return push_operator(queue, op);
        }

        private Queue push_identifier(Queue queue, string identifier)
        {
            queue.Enqueue(new Entry { type = Entry.Type.Identifier, value = identifier });
            return queue;
        }

        private Queue append_queue(Queue to, Queue from)
        {
            foreach (var entry in from)
            {
                to.Enqueue(entry);
            }
            return to;
        }
    }
}
