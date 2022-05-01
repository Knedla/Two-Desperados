using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Init();
        }
        else
            Destroy(gameObject);
    }

    void Init()
    {
        PlayInit();
    }

    public void PlayInit()
    {
        Framework.AudioManager.Pause(false);
    }
}