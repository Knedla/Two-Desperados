using System;
using System.Collections.Generic;

public abstract class Node 
{
    public int Difficulty { get; private set; }
    public int TriggerTracerChance { get; private set; }
    
    public Cell Cell { get; private set; }
    public List<Node> Neighbors { get; private set; }

    Type type;
    public Type Type
    {
        get
        {
            if (type == null)
                type = GetType();

            return type;
        }
    }

    public Node()
    {
        Difficulty = UnityEngine.Random.Range(Config.MinDifficulty, Config.MaxDifficulty);
        TriggerTracerChance = Config.TriggerTracerChanceMultiplier * Difficulty;
        Neighbors = new List<Node>();
    }

    public void SetCell(Cell cell)
    {
        Cell = cell;
    }

    public void AddNeighbor(Node neighbor)
    {
        Neighbors.Add(neighbor);
    }
}
