using System;
using UnityEngine;

namespace Game.Components.UI.Window
{
    public class Controller : IO.PlayerInput.EscapeKey.Item
    {
        const string openTriggerName = "Open";
        const string closeTriggerName = "Close";
        readonly int openTriggerHash = Animator.StringToHash(openTriggerName);
        readonly int closeTriggerHash = Animator.StringToHash(closeTriggerName);

        public delegate bool IsValid();
        public event IsValid IsValidEvent;

        public Action OpenAnimationEndedAction;
        public Action CloseAnimationEndedAction;

        public GameObject Center;
        public Animator Animator;
        public CustomButton CloseButton;

        public bool InTransition { get; private set; }

        void Awake()
        {
            gameObject.SetActive(false);
            CloseButton.onClick.AddListener(Close);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            Center.SetActive(false);

            Animator.Play(openTriggerHash);

            InTransition = true;
        }

        public void Close()
        {
            if (IsValidEvent != null)
                foreach (IsValid item in IsValidEvent.GetInvocationList())
                    if (!item.Invoke())
                        return;

            Animator.Play(closeTriggerHash);
            InTransition = true;
        }

        void OpenAnimationEnded()
        {
            InTransition = false;

            if (OpenAnimationEndedAction != null)
                OpenAnimationEndedAction.Invoke();
        }

        void CloseAnimationEnded()
        {
            InTransition = false;

            if (CloseAnimationEndedAction != null)
                CloseAnimationEndedAction.Invoke();

            gameObject.SetActive(false);
        }

        public override void EscapeKeyPressed()
        {
            Close();
        }
    }
}