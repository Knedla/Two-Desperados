// Decompiled with JetBrains decompiler
// Type: Priority_Queue.GenericPriorityQueueNode`1
// Assembly: Priority Queue, Version=4.1.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 29FD9995-576F-4A2F-B829-F550561A160B
// Assembly location: D:\Unity Projects\PathfindingBasics-master\Assets\Plugins\Priority Queue.dll

namespace Priority_Queue
{
    public class GenericPriorityQueueNode<TPriority>
    {
        public TPriority Priority { get; protected internal set; }

        public int QueueIndex { get; internal set; }

        public long InsertionIndex { get; internal set; }
    }
}
