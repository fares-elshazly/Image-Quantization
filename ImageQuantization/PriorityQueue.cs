using System;
using System.Collections.Generic;
using static ImageQuantization.Graph;

namespace ImageQuantization
{
    class PriorityQueue
    {
        public List<Edge> list;
        public int Count { get { return list.Count; } }

        public PriorityQueue()
        {
            list = new List<Edge>();
        }

        public void Enqueue(Edge x)
        {
            list.Add(x);
            int i = Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (list[p].Weight.CompareTo(x.Weight) <= 0) break;

                list[i] = list[p];
                i = p;
            }

            if (Count > 0) list[i] = x;
        }

        public Edge Dequeue()
        {
            Edge target = Peek();
            Edge root = list[Count - 1];
            list.RemoveAt(Count - 1);

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < Count && list[b].Weight.CompareTo(list[a].Weight) < 0 ? b : a;

                if (list[c].Weight.CompareTo(root.Weight) >= 0) break;
                list[i] = list[c];
                i = c;
            }

            if (Count > 0) list[i] = root;
            return target;
        }

        public Edge Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");
            return list[0];
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}
