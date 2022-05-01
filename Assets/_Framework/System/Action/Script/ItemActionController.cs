using UnityEngine;
using UnityEngine.UI;

namespace Game.System.Action
{
    public class ItemActionController : ActionController<ItemAction>
    {
        [SerializeField] private Image Image;

        public override void SetData(ItemAction action)
        {
            base.SetData(action);
            Image.sprite = action.Item.ButtonSprite;
        }
    }
}
