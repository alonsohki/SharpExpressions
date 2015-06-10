using Antlr.Runtime;
using System;
using System.Globalization;

using SharpExpressions;

namespace SharpExpressions.parser
{
    partial class SharpExpressionsParser
    {
        private System.Collections.Generic.Stack<Queue> mQueues = new System.Collections.Generic.Stack<Queue>();

        public override void ReportError(RecognitionException e)
        {
            base.ReportError(e);
        }

        private void allocate_queues(int capacity)
        {
            for (int i = 0; i < capacity; ++i)
            {
                mQueues.Push(new Queue());
            }
        }

        private Queue empty_queue()
        {
            if (mQueues.Count> 0)
            {
                return mQueues.Pop();
            }
            return new Queue();
        }

        private Queue push_literal(Queue queue, string value)
        {
            double d = double.Parse(value, CultureInfo.InvariantCulture);
            queue.Enqueue(new Entry { type = Entry.Type.Value, value = d });
            return queue;
        }

        private Queue push_string(Queue queue, string value)
        {
            value = value.Substring(1, value.Length - 2);
            queue.Enqueue(new Entry { type = Entry.Type.String, value = value });
            return queue;
        }

        private Queue push_boolean(Queue queue, string value)
        {
            bool b = bool.Parse(value);
            return push_boolean(queue, b);
        }

        private Queue push_boolean(Queue queue, bool b)
        {
            queue.Enqueue(new Entry { type = Entry.Type.Boolean, value = b });
            return queue;
        }

        private Queue push_operator(Queue queue, Operator op)
        {
            if (op == Operator.Negate && queue.Peek().type == Entry.Type.Value)
            {
                // Optimize negating literal values
                var lastElement = queue.Dequeue();
                lastElement.value = -(double)lastElement.value;
                queue.Enqueue(lastElement);
            }
            else
            {
                queue.Enqueue(new Entry { type = Entry.Type.Operator, value = op });
            }
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

            // Invalidate the queue to copy from and add it to be re-used
            from.Clear();
            mQueues.Push(from);

            return to;
        }
    }
}
