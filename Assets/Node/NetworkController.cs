using Game.System.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    static NetworkData networkData;
    public static NetworkData NetworkData { get; private set; }

    [SerializeField] private DataNodeController DataNodeControllerPrefab;
    [SerializeField] private FirewallNodeController FirewallNodeControllerPrefab;
    [SerializeField] private SpamNodeController SpamNodeControllerPrefab;
    [SerializeField] private StartNodeController StartNodeControllerPrefab;
    [SerializeField] private TreasureNodeController TreasureNodeControllerPrefab;
    [SerializeField] private NodePathController NodePathControllerPrefab;

    Dictionary<NodeController, IUser> networkDifficulty;
    Dictionary<Node, NodeController> nodeControllers;

    public IEnumerable<NodeController> NodeControllers => nodeControllers.Values;
    public NodeController StartNodeController { get; private set; }

    private void Awake()
    {
        networkDifficulty = new Dictionary<NodeController, IUser>();
        nodeControllers = new Dictionary<Node, NodeController>();
        
        SetNodeData();
        SetView();

        foreach (NodeController item in nodeControllers.Values)
            StartCoroutine(new LockedState(Player.Instance, item).SetState());

        StartCoroutine(new StartState(Player.Instance, StartNodeController).SetState());
        Framework.EventManager.StartListening(Game.System.Event.SystemListener.PreserveDataBetweenReload, PreserveData);

        transform.position = new Vector3(-NodeController.maxX / 2, -NodeController.maxY / 2);
    }

    void SetNodeData()
    {
        if (networkData != null)
        {
            NetworkData = networkData;
            networkData = null;
        }
        else
            NetworkData = new NetworkData();
    }

    void SetView()
    {
        foreach (NetworkConnection item in NetworkData.NetworkConnections)
        {
            NodePathController nodePathController = Instantiate(NodePathControllerPrefab, transform);
            nodePathController.SetData(GetNodeController(item.NodeFirst), GetNodeController(item.NodeSecond));
        }
    }

    NodeController GetNodeController(Node node)
    {
        NodeController nodeController;

        if (nodeControllers.TryGetValue(node, out nodeController))
            return nodeController;

        if (node.Type == typeof(DataNode))
            nodeController = Instantiate(DataNodeControllerPrefab, transform);
        else if (node.Type == typeof(FirewallNode))
            nodeController = Instantiate(FirewallNodeControllerPrefab, transform);
        else if (node.Type == typeof(SpamNode))
            nodeController = Instantiate(SpamNodeControllerPrefab, transform);
        else if (node.Type == typeof(StartNode))
        {
            nodeController = Instantiate(StartNodeControllerPrefab, transform);
            StartNodeController = nodeController;
        }
        else if (node.Type == typeof(TreasureNode))
            nodeController = Instantiate(TreasureNodeControllerPrefab, transform);

        if (nodeController != null)
            nodeController.SetData(this, node);

        nodeControllers.Add(node, nodeController);

        return nodeController;
    }

    public void AddNetworkDifficulty(NodeController nodeController, IUser byUser)
    {
        networkDifficulty.Add(nodeController, byUser);
    }

    public int GetNetworkDifficulty(IUser forUser)
    {
        return networkDifficulty.Where(s => s.Value != forUser).Count();
    }

    void PreserveData()
    {
        networkData = NetworkData;
    }

    private void OnDestroy()
    {
        Framework.EventManager.StopListening(Game.System.Event.SystemListener.PreserveDataBetweenReload, PreserveData);
    }
}
