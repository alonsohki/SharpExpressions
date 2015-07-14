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

using Antlr.Runtime;
using System;
using System.Globalization;

using SharpExpressions;

namespace SharpExpressions.Parser
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
            queue.Enqueue(new Entry { type = Entry.Type.Double, value = d });
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

        private Queue push_null(Queue queue)
        {
            queue.Enqueue(new Entry { type = Entry.Type.Object, value = null });
            return queue;
        }

        private Queue push_operator(Queue queue, Operator op)
        {
            var lastElement = queue.Last.Value;
            if (op == Operator.Negate && lastElement.type == Entry.Type.Double)
            {
                // Optimize negating literal values
                queue.RemoveLast();
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
