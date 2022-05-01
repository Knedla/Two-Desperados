using Game.System.Action;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TracerController : MonoBehaviour
{
    [SerializeField] protected NodeController HomeNode;

    bool activated;

    private void Awake()
    {
        Framework.EventManager.StartListening(Game.System.Event.CustomListener.ActivateTracers, Activate);
    }

    private void Start()
    {
        StartCoroutine(HomeNode.TriggerState());
    }

    //jel tracer svestan postojanja spam noda i njegove funkcionalnosti?
    //da bi isao najbrzom rutom znaci da tracer pre kretanja vec mora da ima zacrtanu putanju
    //ako je svestan spam noda onda posle svakog hakovanja mora da preracuna putanju ponovo za svaki slucaj
    //u krajnjem slucaju ako postoje neka ogranicenja, recimo da tracer zna koliko ima spam nodova i zna da ih je igrac aktivirao sve, onda ne mora da preracunava putanju posle svakog hakovanja
    public void Activate()
    {
        StopActivateListener();
        Framework.StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        ISearchAlgorithm searchAlgorithm = new AStar(HomeNode.NetworkController.NodeControllers, HomeNode, HomeNode.NetworkController.StartNodeController);
        List<NodeController> reverseList = searchAlgorithm.GetRoute().ToList();
        reverseList.Reverse();
        foreach (NodeController item in reverseList) // moze i varijanta da posle svakog noda opet pita za rutu za slucaj da se tezina mreze promenila
            yield return TriggerAction(item);
    }

    IEnumerator TriggerAction(NodeController nodeController)
    {
        TracerHackAction action = TracerManager.GetAction(nodeController);

        if (action.State == TracerHackAction.ActionState.Executed)
        {
            action.SetTimerViewsEndState();
            yield break;
        }
        else if (action.State == TracerHackAction.ActionState.InProgress) // ovde moze da se desi ako jedan tracer hakuje ka trapu, a drugi ceka da se zavrsi hak, prvi ce da okine trap, a drugi ce takodje da ceka da se taj trap zavrsi - dal je ovo ocekivani scenario?
            yield return action.WaitToFinish();
        else
            yield return action.Execute();
    }

    void StopActivateListener()
    {
        if (activated)
            return;

        activated = true;
        Framework.EventManager.StopListening(Game.System.Event.CustomListener.ActivateTracers, Activate);
    }

    private void OnDestroy()
    {
        StopActivateListener();
    }
}
