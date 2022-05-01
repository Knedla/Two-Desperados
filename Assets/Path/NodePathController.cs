using UnityEngine;

public class NodePathController : MonoBehaviour
{
    [SerializeField] private PathController PlayerPathController;
    [SerializeField] private PathController AIPathController;

    NodeController nodeControllerLeft;
    NodeController nodeControllerRight;

    public void SetData(NodeController nodeControllerFirst, NodeController nodeControllerSecond)
    {
        CalculatePosition(nodeControllerFirst, nodeControllerSecond);
        SetPositionOnScreen();

        nodeControllerFirst.AddNeighbor(nodeControllerSecond, this);
        nodeControllerSecond.AddNeighbor(nodeControllerFirst, this);
    }

    void CalculatePosition(NodeController nodeControllerFirst, NodeController nodeControllerSecond)
    {
        if (nodeControllerFirst.transform.position.x < nodeControllerSecond.transform.position.x)
            SetControllers(nodeControllerFirst, nodeControllerSecond);
        else if (nodeControllerFirst.transform.position.x > nodeControllerSecond.transform.position.x)
            SetControllers(nodeControllerSecond, nodeControllerFirst);
        else
        {
            if (nodeControllerFirst.transform.position.y < nodeControllerSecond.transform.position.y)
                SetControllers(nodeControllerFirst, nodeControllerSecond);
            else //if (nodeControllerFirst.transform.position.y > nodeControllerSecond.transform.position.y)
                SetControllers(nodeControllerSecond, nodeControllerFirst);
        }
    }

    void SetPositionOnScreen()
    {
        transform.position = (nodeControllerLeft.transform.position + nodeControllerRight.transform.position) / 2;
        transform.eulerAngles = new Vector3(0, 0, GetAngle());
        transform.localScale = new Vector3(Vector3.Distance(nodeControllerLeft.transform.position, nodeControllerRight.transform.position) - 1.2f/*- 2 puta po pola noda da bi crta isla do ivice*/, 1, 1); // podrazumeva da je pixel per unit 100 - ako se to promeni nece biti prcizno ///////////////////////////////////////////////////////// - uzeti velicinu noda pa oduzeti dinamicki, a ne ovako zakucano...
    }

    float GetAngle() // mozda ima pametnije ovo da se uradi...
    {
        float angle;

        if (nodeControllerLeft.transform.position.x > nodeControllerRight.transform.position.x) // podsetnik: mora da bude ovaj uslov prvi da bi usao u else ako su isti x-ovi
        {
            if (nodeControllerLeft.transform.position.y < nodeControllerRight.transform.position.y)
                angle = GetAngle(nodeControllerLeft.transform.position, nodeControllerRight.transform.position, Vector3.left) - 180;
            else
                angle = GetAngle(nodeControllerLeft.transform.position, nodeControllerRight.transform.position, Vector3.right);
        }
        else
        {
            if (nodeControllerRight.transform.position.y < nodeControllerLeft.transform.position.y)
                angle = GetAngle(nodeControllerRight.transform.position, nodeControllerLeft.transform.position, Vector3.left) - 180;
            else
                angle = GetAngle(nodeControllerRight.transform.position, nodeControllerLeft.transform.position, Vector3.right);
        }

        return angle;
    }

    float GetAngle(Vector3 first, Vector3 second, Vector3 to)
    {
        return Vector3.Angle(first - second, to);
    }

    void SetControllers(NodeController nodeControllerFirst, NodeController nodeControllerSecond)
    {
        nodeControllerLeft = nodeControllerFirst;
        nodeControllerRight = nodeControllerSecond;
    }

    public PathController GetPathController(NodeController nodeController, IUser user)
    {
        bool leftToRight = nodeController != nodeControllerLeft;

        if (user == Player.Instance)
        {
            PlayerPathController.Prepare(leftToRight);
            return PlayerPathController;
        }
        else
        {
            AIPathController.Prepare(leftToRight);
            return AIPathController;
        }
    }

    public void Lock()
    {
        PlayerPathController.Lock();
        AIPathController.Lock();
    }

    public void Unlock(NodeController nodeController)
    {
        PlayerPathController.Unlock();
        AIPathController.Unlock();
    }

    public void SetEndState(IUser user)
    {
        if (user == Player.Instance)
            PlayerPathController.SetEndState();
        else
            AIPathController.SetEndState();
    }
}
