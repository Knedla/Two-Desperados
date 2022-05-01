using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.Timer
{
    public class WaitForSecondsList : CustomYieldInstruction
    {
        float time;
        float timePass;
        List<TimerView> timerViews;

        public override bool keepWaiting
        {
            get
            {
                if (Time.timeScale > 0)
                {
                    timePass += Time.deltaTime;
                    timerViews.ForEach(s => s.UpdateView(timePass));
                }

                return time > timePass;
            }
        }

        public WaitForSecondsList(float time, List<TimerView> timerViews)
        {
            this.time = time;
            this.timerViews = timerViews;
            timePass = 0;
            foreach (TimerView item in timerViews)
                item.SetData(time);
        }
    }
}
