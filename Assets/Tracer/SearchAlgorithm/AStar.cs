using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

public class AStar : ISearchAlgorithm
{
    IDictionary<NodeController, NodeController> nodeParents;
    IEnumerable<NodeController> nodes;
    NodeController startNode;
    NodeController goalNode;
    string heuristic;

    public AStar(IEnumerable<NodeController> nodes, NodeController startNode, NodeController goalNode)
    {
        nodeParents = new Dictionary<NodeController, NodeController>();
        this.nodes = nodes;
        this.startNode = startNode;
        this.goalNode = goalNode;
        heuristic = "manhattan";
    }

    public IEnumerable<NodeController> GetRoute()
    {
        List<NodeController> path = new List<NodeController>();
        NodeController goal = FindShortestPathAStar();

        NodeController curr = goal;
        while (curr.transform.localPosition != startNode.transform.localPosition)
        {
            path.Add(curr);
            curr = nodeParents[curr];
        }

        return path;
    }

    NodeController FindShortestPathAStar()
    {
        uint nodeVisitCount = 0;

        // A* tries to minimize f(x) = g(x) + h(x), where g(x) is the distance from the start to node "x" and
        //    h(x) is some heuristic that must be admissible, meaning it never overestimates the cost to the next node.
        //    There are formal logical proofs you can look up that determine how heuristics are and are not admissible.

        // Represents h(x) or the score from whatever heuristic we're using
        IDictionary<NodeController, int> heuristicScore = new Dictionary<NodeController, int>();

        // Represents g(x) or the distance from start to node "x" (Same meaning as in Dijkstra's "distances")
        IDictionary<NodeController, int> distanceFromStart = new Dictionary<NodeController, int>();

        foreach (NodeController vertex in nodes)
        {
            heuristicScore.Add(new KeyValuePair<NodeController, int>(vertex, int.MaxValue));
            distanceFromStart.Add(new KeyValuePair<NodeController, int>(vertex, int.MaxValue));
        }

        heuristicScore[startNode] = HeuristicCostEstimate(startNode.transform.localPosition, goalNode.transform.localPosition, heuristic); // STAVIO transform.position NE ZNAM NI ZASTO /////////////////////////////////////
        distanceFromStart[startNode] = 0;

        // The item dequeued from a priority queue will always be the one with the lowest int value
        //    In this case we will input nodes with their calculated distances from the start g(x),
        //    so we will always take the node with the lowest distance from the queue.
        SimplePriorityQueue<NodeController, int> priorityQueue = new SimplePriorityQueue<NodeController, int>();
        priorityQueue.Enqueue(startNode, heuristicScore[startNode]);

        while (priorityQueue.Count > 0)
        {
            // Get the node with the least distance from the start
            NodeController curr = priorityQueue.Dequeue();
            nodeVisitCount++;

            // If our current node is the goal then stop
            if (curr == goalNode)
                return goalNode;

            IEnumerable<NodeController> neighbors = curr.Neighbors.Keys; // PROMENJENO SA IList //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            foreach (NodeController node in neighbors)
            {
                // Get the distance so far, add it to the distance to the neighbor
                int currScore = distanceFromStart[curr] + node.Difficulty;

                // If our distance to this neighbor is LESS than another calculated shortest path
                //    to this neighbor, set a new node parent and update the scores as our current
                //    best for the path so far.
                if (currScore < distanceFromStart[node])
                {
                    nodeParents[node] = curr;
                    distanceFromStart[node] = currScore;

                    int hScore = distanceFromStart[node] + HeuristicCostEstimate(node.transform.localPosition, goalNode.transform.localPosition, heuristic); // STAVIO transform.position NE ZNAM NI ZASTO ///////////////////
                    heuristicScore[node] = hScore;

                    // If this node isn't already in the queue, make sure to add it. Since the
                    //    algorithm is always looking for the smallest distance, any existing entry
                    //    would have a higher priority anyway.
                    if (!priorityQueue.Contains(node))
                        priorityQueue.Enqueue(node, hScore);
                }
            }
        }

        return startNode;
    }

    int HeuristicCostEstimate(Vector3 node, Vector3 goal, string heuristic)
    {
        switch (heuristic)
        {
            case "euclidean":
                return EuclideanEstimate(node, goal);
            case "manhattan":
                return ManhattanEstimate(node, goal);
        }

        return -1;
    }

    int EuclideanEstimate(Vector3 node, Vector3 goal)
    {
        return (int)Mathf.Sqrt(Mathf.Pow(node.x - goal.x, 2) +
            Mathf.Pow(node.y - goal.y, 2) +
            Mathf.Pow(node.z - goal.z, 2));
    }

    int ManhattanEstimate(Vector3 node, Vector3 goal)
    {
        return (int)(Mathf.Abs(node.x - goal.x) +
            Mathf.Abs(node.y - goal.y) +
            Mathf.Abs(node.z - goal.z));
    }
}
