using AYellowpaper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class TooltipTarget : MonoBehaviour
    {
        
        public static TooltipTarget Current { get; private set; }

        [SerializeField]
        InterfaceReference<IItemDisplay> itemDisplay;

        public Item Item => itemDisplay.Value.Item;

        public bool Active { get; set; } = true;

        public void PointerEntered()
        {
            if (Active)
                Current = this;
        }
        public void PointerExit()
        {
            Current = null;
        }

    }

    public interface IItemDisplay
    {
        public Item Item { get; }
    }
}
