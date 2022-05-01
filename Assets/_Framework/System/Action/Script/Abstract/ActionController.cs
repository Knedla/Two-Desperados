using UnityEngine;
using UnityEngine.UI;

namespace Game.System.Action
{
    public abstract class ActionController<T> : MonoBehaviour where T : IAction
    {
        public Button Button;

        T action;

        Material material;

        public virtual void SetData(T action)
        {
            this.action = action;
            Validate();
        }

        public void Validate()
        {
            if (material == null)
            {
                material = Instantiate(Button.image.material); // iz nekog razloga "material = SpriteRenderer.material;" odma napravi novu instancu materijala, ali za "material = Button.image.material;" nece...
                Button.image.material = material;
            }

            if (action.IsValid())
            {
                Button.enabled = true;
                material.SetFloat(Global.GrayscaleProp, 0);
            }
            else
            {
                Button.enabled = false;
                material.SetFloat(Global.GrayscaleProp, 1);
            }
        }

        public virtual void Execute()
        {
            Framework.StartCoroutine(action.Execute());
            Validate();
        }

        private void OnDestroy()
        {
            if (material != null)
                Destroy(Button.image.material);
        }
    }
}
