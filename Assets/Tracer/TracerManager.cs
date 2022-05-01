using Game.System.Action;
using System.Collections.Generic;
using UnityEngine;

public class TracerManager : MonoBehaviour
{
    static Dictionary<NodeController, TracerHackAction> nodeControllers; // zbudzeno! trebalo mi je da bude van TracerController zbog reseta liste - vamo ima mnogo instanci, a ovo je jedna...
    public static TracerHackAction GetAction(NodeController nodeController)
    {
        TracerHackAction action;
        if (!nodeControllers.TryGetValue(nodeController, out action))
        {
            action = new TracerHackAction(Tracer.Instance, nodeController);
            nodeControllers.Add(nodeController, action);
        }

        return action;
    }

    bool subscabed;

    private void Awake()
    {
        nodeControllers = new Dictionary<NodeController, TracerHackAction>();
        Framework.EventManager.StartListening(Game.System.Event.CustomListener.NodeHacked, TryActivate);
    }

    void TryActivate()
    {
        ITriggeringPercent triggeringPercent = Framework.EventManager.TriggeringObject as ITriggeringPercent;

        if (triggeringPercent == null || !RandomHelper.RandomChance(triggeringPercent.TriggeringPercent))
            return;

        Subscabe();
    }

    void Subscabe()
    {
        if (subscabed)
            return;

        subscabed = true;
        Framework.EventManager.TriggerEvent(Game.System.Event.CustomListener.ActivateTracers);
    }

    private void OnDestroy()
    {
        nodeControllers = null;
        Unubscabe();
    }

    void Unubscabe()
    {
        if (!subscabed)
            return;

        subscabed = true;
        Framework.EventManager.StopListening(Game.System.Event.CustomListener.NodeHacked, TryActivate);
    }
}
