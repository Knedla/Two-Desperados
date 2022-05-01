using Game.System.Entity.Definition;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public Item ItemDefinition;

    public Image Image;
    public Text Text;

    public void OnEnable()
    {
        Framework.EventManager.StartListening(ItemDefinition.OnValueChangedTriggerName, SetText);
    }

    private void OnDisable()
    {
        Framework.EventManager.StopListening(ItemDefinition.OnValueChangedTriggerName, SetText);
    }

    void SetText()
    {
        Text.text = Framework.PlayerData.Inventory.GetQuantity(ItemDefinition.Id).ToString();
    }

    private void Awake()
    {
        Image.sprite = ItemDefinition.IconSprite;
        SetText();
    }
}
