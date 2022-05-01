using UnityEngine;

namespace Game.Components.Timer
{
    public class WaitForSeconds : CustomYieldInstruction
    {
        float time;
        float timePass;
        TimerView timerView;

        public override bool keepWaiting
        {
            get
            {
                if (Time.timeScale > 0)
                {
                    timePass += Time.deltaTime;
                    timerView.UpdateView(timePass);
                }

                return time > timePass;
            }
        }

        public WaitForSeconds(float time, TimerView timerView)
        {
            this.time = time;
            this.timerView = timerView;
            timePass = 0;
            timerView.SetData(time);
        }
    }
}
