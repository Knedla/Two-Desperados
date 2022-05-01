using Game.Components.Notification;
using Game.Components.UI.Window;
using Game.System.PersistentData;
using Game.System.Validation;
using UnityEngine;

public class GameplayConfigController : MonoBehaviour
{
    public Controller UIWindow;

    public MinMaxInputField NodeCountInputField;
    public MinMaxInputField TreasureNodeCountInputField;
    public MinMaxInputField FirewallNodeCountInputField;
    public MinMaxInputField SpamNodeCountInputField;
    public MinMaxInputField SpamNodeDecreaseInputField;
    public MinMaxInputField TrapDelayTimeInputField;

    private void OnEnable()
    {
        UIWindow.IsValidEvent += UIWindow_IsValidEvent;
    }

    private void OnDisable()
    {
        UIWindow.IsValidEvent -= UIWindow_IsValidEvent;
    }

    private bool UIWindow_IsValidEvent()
    {
        ValidationResult validationResult = new Validator<PlayerPreferenceData>()
            .AddRule(new PlayerPreferenceDataValidationRule()).Validate(Framework.PlayerPreferenceData);

        if (!validationResult.IsValid)
            Framework.NotificationManager.Notify(new InstantNotification(null, string.Format("Error code: {0}", validationResult.Code.ToString()), "Ukupan broj nodova mora biti veci od zbira treasure, firewall i spam", (int)validationResult.Code.Value)); // ovo iz nekih resursa da se vuce, kao i svaki text zbog lokalizacije
        else if (!TreasureNodeCountInputField.Validate().IsValid
            || !FirewallNodeCountInputField.Validate().IsValid
            || !SpamNodeCountInputField.Validate().IsValid
            || !SpamNodeDecreaseInputField.Validate().IsValid
            || !TrapDelayTimeInputField.Validate().IsValid)
            return false;
        else
            Framework.PlayerPreferenceData.LocalSave();

        return validationResult.IsValid;
    }
}
