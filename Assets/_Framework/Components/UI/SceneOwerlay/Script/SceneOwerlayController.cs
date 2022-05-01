using Game.System.Event;
using UnityEngine;

public class SceneOwerlayController : MonoBehaviour
{
    public void AnimationEnded()
    {
        Framework.EventManager.TriggerEvent(SystemListener.OnSceneOwerlayAnimationEnded);
    }
}
