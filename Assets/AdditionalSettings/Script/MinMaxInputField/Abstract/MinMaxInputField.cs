using Game.Components.Notification;
using Game.System.Validation;
using UnityEngine;
using UnityEngine.UI;

public abstract class MinMaxInputField : MonoBehaviour
{
    public InputField InputField;

    protected abstract int? MinValue { get; }
    protected abstract int? MaxValue { get; }

    void Awake()
    {
        InputField.text = GetValue().ToString();
        InputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    protected abstract int GetValue();
    protected abstract void SetValue(int value);

    /// <summary>
    /// - da MinMaxInputField ne bude abstract i da nasledi InputField - pa da se iz nekog drugog skripta popune MinValue i MaxValue i da se hvata value
    /// - ove min i max vrednosti sto sam pozakucavao u configu mogu drugacije da se proslede, recimo da se izloze MinValue i MaxValue vrednosti editoru, pa tu da se ukuca, mada ja to volim da drzim centralizovano, bez da se setuju vrednosti svuda po editoru, al ako treba moze i to...
    /// - mozda da se ucitaju iz nekog xml configa, mada mi ne deluje kao stvar koja bi trebalo toliko slobode da ima
    /// </summary>
    protected virtual void ValueChangeCheck()
    {
        int value;

        if (!int.TryParse(InputField.text, out value))
            return;

        if (MinValue != null && MinValue <= value && MaxValue != null && value <= MaxValue.Value)
            SetValue(value);
    }

    public ValidationResult Validate() // ovo treba da vrati vrednos gde god, pa taj ko je zvao validaviju da okida notifikaciju - trenutno stavljeno ovde da skratim proces pisanja... veck sam u skripcu sa vremenom...
    {
        int value;

        ValidationResult validationResult;

        if (!int.TryParse(InputField.text, out value))
        {
            validationResult = new ValidationResult(new ValidationError(ErrorCode.CastNumber_00001));
            Framework.NotificationManager.Notify(new InstantNotification(null, GetTitle(validationResult.Code), "Mora biti broj!", (int)validationResult.Code.Value)); // ovo iz nekih resursa da se vuce, kao i svaki text zbog lokalizacije
        }
        else
        {
            validationResult = new Validator<int>().AddRule(new IsInRangeValidationRule(MinValue, MaxValue)).Validate(value);

            if (!validationResult.IsValid)
            {
                if (MinValue == null)
                    Framework.NotificationManager.Notify(new InstantNotification(null, GetTitle(validationResult.Code), string.Format("Max ne sme biti veci od {0}", MaxValue.Value), (int)validationResult.Code.Value));
                else if (MaxValue == int.MaxValue)
                    Framework.NotificationManager.Notify(new InstantNotification(null, GetTitle(validationResult.Code), string.Format("Min ne sme biti manji od {0}", MinValue.Value), (int)validationResult.Code.Value));
                else
                    Framework.NotificationManager.Notify(new InstantNotification(null, GetTitle(validationResult.Code), string.Format("Mora biti izmadju {0} i {1}", MinValue.Value, MaxValue.Value), (int)validationResult.Code.Value));
            }
        }

        return validationResult;
    }

    string GetTitle(ErrorCode? errorCode)
    {
        return string.Format("Error code: {0} {1}", errorCode.ToString(), InputField.name);
    }
}
