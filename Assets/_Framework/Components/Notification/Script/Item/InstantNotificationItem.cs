using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Notification
{
    public class InstantNotificationItem : MonoBehaviour, IItem<InstantNotification>
    {
        const float showDuration = 1;
        const float fadeSpeed = 4f;

        public CanvasGroup CanvasGroup;
        public Image Image;
        public Text Title;
        public Text Description;

        public int Id { get; private set; }
        public bool Finished { get; private set; }

        public void Initialize(InstantNotification notification)
        {
            Id = notification.Id;

            Finished = false;
            transform.localScale = new Vector3(1, 1, 1);

            CanvasGroup.alpha = 0;

            if (notification.Sprite != null)
            {
                Image.gameObject.SetActive(true);
                Image.sprite = notification.Sprite;
            }
            else
                Image.gameObject.SetActive(false);

            if (notification.Title != string.Empty)
            {
                Title.gameObject.SetActive(true);
                Title.text = notification.Title;
            }
            else
                Title.gameObject.SetActive(false);

            if (notification.Description != string.Empty)
            {
                Description.gameObject.SetActive(true);
                Description.text = notification.Description;
            }
            else
                Description.gameObject.SetActive(false);

            CanvasGroup.alpha = 1;
            StartCoroutine(ShowNotification());
        }

        IEnumerator ShowNotification()
        {
            float timer = Time.unscaledTime + showDuration;

            while (Time.unscaledTime < timer)
                yield return null;

            while (CanvasGroup.alpha > 0)
            {
                CanvasGroup.alpha -= Time.unscaledDeltaTime * fadeSpeed;
                yield return null;
            }

            Finished = true;
        }
    }
}