using Game.System.Action;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : Game.Components.IO.PlayerInput.EscapeKey.Item
{
    const string difficultyText = "Difficulty: "; // generalno bi za stringove napravio resource file-ove iz kojih bi dovlacio lokalizovanu vrednost po kljucu, ne bi zakucavao svuda po kodu...
    const string triggerChance = "Detection: {0}%";

    public static ActionPanelController Instance { get; private set; }

    [SerializeField] private BasicActionController HackActionController;
    [SerializeField] private ItemActionController NukeActionController;
    [SerializeField] private ItemActionController TrapActionController;

    [SerializeField] private Text DifficultyText;
    [SerializeField] private Text TriggerChanceText;

    [SerializeField] private Canvas Canvas;

    bool subscibed;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Hide();
        Canvas.worldCamera = Camera.main;
    }
    
    public void Open(IUser user, NodeController nodeController)
    {
        HackActionController.SetData(new HackAction(user, nodeController));
        HackActionController.Button.onClick.AddListener(Hide);
        NukeActionController.SetData(new NukeAction(user, nodeController));
        NukeActionController.Button.onClick.AddListener(Hide);
        TrapActionController.SetData(new TrapAction(user, nodeController));
        TrapActionController.Button.onClick.AddListener(Hide);

        DifficultyText.text = difficultyText + nodeController.Difficulty.ToString();
        TriggerChanceText.text = string.Format(triggerChance, nodeController.TriggeringPercent.ToString());

        transform.position = nodeController.transform.position;

        gameObject.SetActive(true);

        //Subscribe(); // ovo realno nema potrebe osim ako se ne omoguci da se selektuje lokovan node - selektovan nod ne bi imao enableovane dugmice. ako je otvoren, posle haka, ActionPanel refresovao bi validaciju i enableovao dugmice.
    }

    void Subscribe()
    {
        if (subscibed)
            return;

        subscibed = true;
        Framework.EventManager.StartListening(Game.System.Event.CustomListener.NodeHacked, Revalidate);
    }

    void Revalidate()
    {
        HackActionController.Validate();
        NukeActionController.Validate();
        TrapActionController.Validate();
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void EscapeKeyPressed()
    {
        Hide();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        //Unsubscribe();
    }

    void Unsubscribe()
    {
        if (!subscibed)
            return;

        subscibed = false;
        Framework.EventManager.StopListening(Game.System.Event.CustomListener.NodeHacked, Revalidate);
    }
}
