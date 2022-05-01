using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Timer
{
    public class PercentageCooldownView : TimerView
    {
        const string defualtText = "{0}%";

        [SerializeField] private Image Image;
        [SerializeField] private Text Text;

        float time;
        int curentPercentage;

        public override void SetData(float time)
        {
            this.time = time;
            Image.fillAmount = 0;
            Text.text = string.Format(defualtText, 0);
        }

        public override void UpdateView(float timePass)
        {
            float value = timePass / time;
            Image.fillAmount = value;

            int tmpPercentage = (int)(value * 100);

            if (curentPercentage < tmpPercentage)
            {
                curentPercentage = tmpPercentage;
                Text.text = string.Format(defualtText, curentPercentage.ToString());
            }
        }
    }
}
