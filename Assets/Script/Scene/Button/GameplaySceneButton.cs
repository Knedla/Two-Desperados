using UnityEngine;

public class GameplaySceneButton : MonoBehaviour
{
    public void Open()
    {
        GameplaySceneController.LoadScene();
    }
}
