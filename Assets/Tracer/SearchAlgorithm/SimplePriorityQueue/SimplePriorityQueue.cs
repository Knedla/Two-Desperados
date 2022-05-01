// Decompiled with JetBrains decompiler
// Type: Priority_Queue.SimplePriorityQueue`2
// Assembly: Priority Queue, Version=4.1.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 29FD9995-576F-4A2F-B829-F550561A160B
// Assembly location: D:\Unity Projects\PathfindingBasics-master\Assets\Plugins\Priority Queue.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Priority_Queue
{
    public class SimplePriorityQueue<TItem, TPriority> :
      IPriorityQueue<TItem, TPriority>,
      IEnumerable<TItem>,
      IEnumerable
      where TPriority : IComparable<TPriority>
    {
        private const int INITIAL_QUEUE_SIZE = 10;
        private readonly GenericPriorityQueue<SimplePriorityQueue<TItem, TPriority>.SimpleNode, TPriority> _queue;
        private readonly Dictionary<TItem, IList<SimplePriorityQueue<TItem, TPriority>.SimpleNode>> _itemToNodesCache;
        private readonly IList<SimplePriorityQueue<TItem, TPriority>.SimpleNode> _nullNodesCache;

        public SimplePriorityQueue()
          : this((IComparer<TPriority>)Comparer<TPriority>.Default)
        {
        }

        public SimplePriorityQueue(IComparer<TPriority> comparer)
          : this(new Comparison<TPriority>(comparer.Compare))
        {
        }

        public SimplePriorityQueue(Comparison<TPriority> comparer)
        {
            this._queue = new GenericPriorityQueue<SimplePriorityQueue<TItem, TPriority>.SimpleNode, TPriority>(10, comparer);
            this._itemToNodesCache = new Dictionary<TItem, IList<SimplePriorityQueue<TItem, TPriority>.SimpleNode>>();
            this._nullNodesCache = (IList<SimplePriorityQueue<TItem, TPriority>.SimpleNode>)new List<SimplePriorityQueue<TItem, TPriority>.SimpleNode>();
        }

        private SimplePriorityQueue<TItem, TPriority>.SimpleNode GetExistingNode(
          TItem item)
        {
            if ((object)item == null)
                return this._nullNodesCache.Count <= 0 ? (SimplePriorityQueue<TItem, TPriority>.SimpleNode)null : this._nullNodesCache[0];
            if (!this._itemToNodesCache.ContainsKey(item))
                return (SimplePriorityQueue<TItem, TPriority>.SimpleNode)null;
            IList<SimplePriorityQueue<TItem, TPriority>.SimpleNode> simpleNodeList = this._itemToNodesCache[item];
            return simpleNodeList.Count <= 0 ? (SimplePriorityQueue<TItem, TPriority>.SimpleNode)null : simpleNodeList[0];
        }

        private void AddToNodeCache(
          SimplePriorityQueue<TItem, TPriority>.SimpleNode node)
        {
            if ((object)node.Data == null)
            {
                this._nullNodesCache.Add(node);
            }
            else
            {
                if (!this._itemToNodesCache.ContainsKey(node.Data))
                    this._itemToNodesCache[node.Data] = (IList<SimplePriorityQueue<TItem, TPriority>.SimpleNode>)new List<SimplePriorityQueue<TItem, TPriority>.SimpleNode>();
                this._itemToNodesCache[node.Data].Add(node);
            }
        }

        private void RemoveFromNodeCache(
          SimplePriorityQueue<TItem, TPriority>.SimpleNode node)
        {
            if ((object)node.Data == null)
            {
                this._nullNodesCache.Remove(node);
            }
            else
            {
                if (!this._itemToNodesCache.ContainsKey(node.Data))
                    return;
                this._itemToNodesCache[node.Data].Remove(node);
                if (this._itemToNodesCache[node.Data].Count != 0)
                    return;
                this._itemToNodesCache.Remove(node.Data);
            }
        }

        public int Count
        {
            get
            {
                lock (this._queue)
                    return this._queue.Count;
            }
        }

        public TItem First
        {
            get
            {
                lock (this._queue)
                {
                    if (this._queue.Count <= 0)
                        throw new InvalidOperationException("Cannot call .First on an empty queue");
                    return this._queue.First.Data;
                }
            }
        }

        public void Clear()
        {
            lock (this._queue)
            {
                this._queue.Clear();
                this._itemToNodesCache.Clear();
                this._nullNodesCache.Clear();
            }
        }

        public bool Contains(TItem item)
        {
            lock (this._queue)
                return this.GetExistingNode(item) != null;
        }

        public TItem Dequeue()
        {
            lock (this._queue)
            {
                SimplePriorityQueue<TItem, TPriority>.SimpleNode node = this._queue.Count > 0 ? this._queue.Dequeue() : throw new InvalidOperationException("Cannot call Dequeue() on an empty queue");
                this.RemoveFromNodeCache(node);
                return node.Data;
            }
        }

        private void EnqueueNoLock(TItem item, TPriority priority)
        {
            SimplePriorityQueue<TItem, TPriority>.SimpleNode node = new SimplePriorityQueue<TItem, TPriority>.SimpleNode(item);
            if (this._queue.Count == this._queue.MaxSize)
                this._queue.Resize(this._queue.MaxSize * 2 + 1);
            this._queue.Enqueue(node, priority);
            this.AddToNodeCache(node);
        }

        public void Enqueue(TItem item, TPriority priority)
        {
            lock (this._queue)
                this.EnqueueNoLock(item, priority);
        }

        public bool EnqueueWithoutDuplicates(TItem item, TPriority priority)
        {
            lock (this._queue)
            {
                if (this.Contains(item))
                    return false;
                this.EnqueueNoLock(item, priority);
                return true;
            }
        }

        public void Remove(TItem item)
        {
            lock (this._queue)
            {
                SimplePriorityQueue<TItem, TPriority>.SimpleNode existingNode = this.GetExistingNode(item);
                if (existingNode == null)
                    throw new InvalidOperationException("Cannot call Remove() on a node which is not enqueued: " + (object)item);
                this._queue.Remove(existingNode);
                this.RemoveFromNodeCache(existingNode);
            }
        }

        public void UpdatePriority(TItem item, TPriority priority)
        {
            lock (this._queue)
                this._queue.UpdatePriority(this.GetExistingNode(item) ?? throw new InvalidOperationException("Cannot call UpdatePriority() on a node which is not enqueued: " + (object)item), priority);
        }

        public TPriority GetPriority(TItem item)
        {
            lock (this._queue)
                return (this.GetExistingNode(item) ?? throw new InvalidOperationException("Cannot call GetPriority() on a node which is not enqueued: " + (object)item)).Priority;
        }

        public bool TryFirst(out TItem first)
        {
            lock (this._queue)
            {
                if (this._queue.Count <= 0)
                {
                    first = default(TItem);
                    return false;
                }
                first = this._queue.First.Data;
                return true;
            }
        }

        public bool TryDequeue(out TItem first)
        {
            lock (this._queue)
            {
                if (this._queue.Count <= 0)
                {
                    first = default(TItem);
                    return false;
                }
                SimplePriorityQueue<TItem, TPriority>.SimpleNode node = this._queue.Dequeue();
                first = node.Data;
                this.RemoveFromNodeCache(node);
                return true;
            }
        }

        public bool TryRemove(TItem item)
        {
            lock (this._queue)
            {
                SimplePriorityQueue<TItem, TPriority>.SimpleNode existingNode = this.GetExistingNode(item);
                if (existingNode == null)
                    return false;
                this._queue.Remove(existingNode);
                this.RemoveFromNodeCache(existingNode);
                return true;
            }
        }

        public bool TryUpdatePriority(TItem item, TPriority priority)
        {
            lock (this._queue)
            {
                SimplePriorityQueue<TItem, TPriority>.SimpleNode existingNode = this.GetExistingNode(item);
                if (existingNode == null)
                    return false;
                this._queue.UpdatePriority(existingNode, priority);
                return true;
            }
        }

        public bool TryGetPriority(TItem item, out TPriority priority)
        {
            lock (this._queue)
            {
                SimplePriorityQueue<TItem, TPriority>.SimpleNode existingNode = this.GetExistingNode(item);
                if (existingNode == null)
                {
                    priority = default(TPriority);
                    return false;
                }
                priority = existingNode.Priority;
                return true;
            }
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            List<TItem> objList = new List<TItem>();
            lock (this._queue)
            {
                foreach (SimplePriorityQueue<TItem, TPriority>.SimpleNode simpleNode in this._queue)
                    objList.Add(simpleNode.Data);
            }
            return (IEnumerator<TItem>)objList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)this.GetEnumerator();

        public bool IsValidQueue()
        {
            lock (this._queue)
            {
                foreach (IEnumerable<SimplePriorityQueue<TItem, TPriority>.SimpleNode> simpleNodes in this._itemToNodesCache.Values)
                {
                    foreach (SimplePriorityQueue<TItem, TPriority>.SimpleNode node in simpleNodes)
                    {
                        if (!this._queue.Contains(node))
                            return false;
                    }
                }
                foreach (SimplePriorityQueue<TItem, TPriority>.SimpleNode simpleNode in this._queue)
                {
                    if (this.GetExistingNode(simpleNode.Data) == null)
                        return false;
                }
                return this._queue.IsValidQueue();
            }
        }

        private class SimpleNode : GenericPriorityQueueNode<TPriority>
        {
            public TItem Data { get; private set; }

            public SimpleNode(TItem data) => this.Data = data;
        }
    }
}
