using UnityEngine;

namespace Game.Components.Settings
{
    public class SettingsSceneButton : MonoBehaviour
    {
        public void Open()
        {
            SettingsSceneController.LoadScene();
        }
    }
}