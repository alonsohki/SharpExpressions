using System.Collections.Generic;

namespace SharpExpressions.Parser
{
    public class Queue : LinkedList<Entry>
    {
        public void Enqueue(Entry entry)
        {
            this.AddLast(entry);
        }

        public Entry Dequeue()
        {
            Entry entry = this.First.Value;
            this.RemoveFirst();
            return entry;
        }
    }
}
