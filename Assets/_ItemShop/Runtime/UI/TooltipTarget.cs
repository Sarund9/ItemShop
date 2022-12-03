using AYellowpaper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ItemShop
{
    public class TooltipTarget : MonoBehaviour
    {
        
        public static TooltipTarget Current { get; private set; }

        [SerializeField]
        InterfaceReference<IItemDisplay> itemDisplay;

        [SerializeField]
        Button button;
        
        public Item Item => itemDisplay.Value.Item;

        bool _active = true;

        public bool Active
        {
            get => _active;
            set {
                _active = value;
                button.interactable = value;
            }
        }

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
