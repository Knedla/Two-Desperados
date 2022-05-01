// Decompiled with JetBrains decompiler
// Type: Priority_Queue.IPriorityQueue`2
// Assembly: Priority Queue, Version=4.1.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 29FD9995-576F-4A2F-B829-F550561A160B
// Assembly location: D:\Unity Projects\PathfindingBasics-master\Assets\Plugins\Priority Queue.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Priority_Queue
{
    public interface IPriorityQueue<TItem, in TPriority> : IEnumerable<TItem>, IEnumerable
      where TPriority : IComparable<TPriority>
    {
        void Enqueue(TItem node, TPriority priority);

        TItem Dequeue();

        void Clear();

        bool Contains(TItem node);

        void Remove(TItem node);

        void UpdatePriority(TItem node, TPriority priority);

        TItem First { get; }

        int Count { get; }
    }
}
