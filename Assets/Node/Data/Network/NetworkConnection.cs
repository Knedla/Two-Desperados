public class NetworkConnection
{
    public Node NodeFirst { get; set; }
    public Node NodeSecond { get; set; }

    public NetworkConnection(Node nodeFirst, Node nodeSecond)
    {
        NodeFirst = nodeFirst;
        NodeSecond = nodeSecond;

        NodeFirst.AddNeighbor(NodeSecond);
        NodeSecond.AddNeighbor(NodeFirst);
    }
}
