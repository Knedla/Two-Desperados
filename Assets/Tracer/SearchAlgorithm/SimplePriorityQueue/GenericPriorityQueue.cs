// Decompiled with JetBrains decompiler
// Type: Priority_Queue.GenericPriorityQueue`2
// Assembly: Priority Queue, Version=4.1.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 29FD9995-576F-4A2F-B829-F550561A160B
// Assembly location: D:\Unity Projects\PathfindingBasics-master\Assets\Plugins\Priority Queue.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Priority_Queue
{
    public sealed class GenericPriorityQueue<TItem, TPriority> :
      IFixedSizePriorityQueue<TItem, TPriority>,
      IPriorityQueue<TItem, TPriority>,
      IEnumerable<TItem>,
      IEnumerable
      where TItem : GenericPriorityQueueNode<TPriority>
      where TPriority : IComparable<TPriority>
    {
        private int _numNodes;
        private TItem[] _nodes;
        private long _numNodesEverEnqueued;
        private readonly Comparison<TPriority> _comparer;

        public GenericPriorityQueue(int maxNodes)
          : this(maxNodes, (IComparer<TPriority>)Comparer<TPriority>.Default)
        {
        }

        public GenericPriorityQueue(int maxNodes, IComparer<TPriority> comparer)
          : this(maxNodes, new Comparison<TPriority>(comparer.Compare))
        {
        }

        public GenericPriorityQueue(int maxNodes, Comparison<TPriority> comparer)
        {
            this._numNodes = 0;
            this._nodes = new TItem[maxNodes + 1];
            this._numNodesEverEnqueued = 0L;
            this._comparer = comparer;
        }

        public int Count => this._numNodes;

        public int MaxSize => this._nodes.Length - 1;

        public void Clear()
        {
            Array.Clear((Array)this._nodes, 1, this._numNodes);
            this._numNodes = 0;
        }

        public bool Contains(TItem node) => (object)this._nodes[node.QueueIndex] == (object)node;

        public void Enqueue(TItem node, TPriority priority)
        {
            node.Priority = priority;
            ++this._numNodes;
            this._nodes[this._numNodes] = node;
            node.QueueIndex = this._numNodes;
            node.InsertionIndex = this._numNodesEverEnqueued++;
            this.CascadeUp(this._nodes[this._numNodes]);
        }

        private void Swap(TItem node1, TItem node2)
        {
            this._nodes[node1.QueueIndex] = node2;
            this._nodes[node2.QueueIndex] = node1;
            int queueIndex = node1.QueueIndex;
            node1.QueueIndex = node2.QueueIndex;
            node2.QueueIndex = queueIndex;
        }

        private void CascadeUp(TItem node)
        {
            for (int index = node.QueueIndex / 2; index >= 1; index = node.QueueIndex / 2)
            {
                TItem node1 = this._nodes[index];
                if (this.HasHigherPriority(node1, node))
                    break;
                this.Swap(node, node1);
            }
        }

        private void CascadeDown(TItem node)
        {
            int index1 = node.QueueIndex;
            while (true)
            {
                TItem lower = node;
                int index2 = 2 * index1;
                if (index2 <= this._numNodes)
                {
                    TItem node1 = this._nodes[index2];
                    if (this.HasHigherPriority(node1, lower))
                        lower = node1;
                    int index3 = index2 + 1;
                    if (index3 <= this._numNodes)
                    {
                        TItem node2 = this._nodes[index3];
                        if (this.HasHigherPriority(node2, lower))
                            lower = node2;
                    }
                    if ((object)lower != (object)node)
                    {
                        this._nodes[index1] = lower;
                        int queueIndex = lower.QueueIndex;
                        lower.QueueIndex = index1;
                        index1 = queueIndex;
                    }
                    else
                        goto label_10;
                }
                else
                    break;
            }
            node.QueueIndex = index1;
            this._nodes[index1] = node;
            return;
            label_10:
            node.QueueIndex = index1;
            this._nodes[index1] = node;
        }

        private bool HasHigherPriority(TItem higher, TItem lower)
        {
            int num = this._comparer(higher.Priority, lower.Priority);
            if (num < 0)
                return true;
            return num == 0 && higher.InsertionIndex < lower.InsertionIndex;
        }

        public TItem Dequeue()
        {
            TItem node = this._nodes[1];
            this.Remove(node);
            return node;
        }

        public void Resize(int maxNodes)
        {
            TItem[] objArray = new TItem[maxNodes + 1];
            int num = Math.Min(maxNodes, this._numNodes);
            for (int index = 1; index <= num; ++index)
                objArray[index] = this._nodes[index];
            this._nodes = objArray;
        }

        public TItem First => this._nodes[1];

        public void UpdatePriority(TItem node, TPriority priority)
        {
            node.Priority = priority;
            this.OnNodeUpdated(node);
        }

        private void OnNodeUpdated(TItem node)
        {
            int index = node.QueueIndex / 2;
            TItem node1 = this._nodes[index];
            if (index > 0 && this.HasHigherPriority(node, node1))
                this.CascadeUp(node);
            else
                this.CascadeDown(node);
        }

        public void Remove(TItem node)
        {
            if (node.QueueIndex == this._numNodes)
            {
                this._nodes[this._numNodes] = default(TItem);
                --this._numNodes;
            }
            else
            {
                TItem node1 = this._nodes[this._numNodes];
                this.Swap(node, node1);
                this._nodes[this._numNodes] = default(TItem);
                --this._numNodes;
                this.OnNodeUpdated(node1);
            }
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            for (int i = 1; i <= this._numNodes; ++i)
                yield return this._nodes[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)this.GetEnumerator();

        public bool IsValidQueue()
        {
            for (int index1 = 1; index1 < this._nodes.Length; ++index1)
            {
                if ((object)this._nodes[index1] != null)
                {
                    int index2 = 2 * index1;
                    if (index2 < this._nodes.Length && (object)this._nodes[index2] != null && this.HasHigherPriority(this._nodes[index2], this._nodes[index1]))
                        return false;
                    int index3 = index2 + 1;
                    if (index3 < this._nodes.Length && (object)this._nodes[index3] != null && this.HasHigherPriority(this._nodes[index3], this._nodes[index1]))
                        return false;
                }
            }
            return true;
        }
    }
}
