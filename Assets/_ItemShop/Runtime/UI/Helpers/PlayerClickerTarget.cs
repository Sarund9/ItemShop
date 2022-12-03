using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class PlayerClickerTarget : MonoBehaviour, IItemDisplay
    {
        public Item Item => clothes[slot];

        [SerializeField]
        PlayerClothes clothes;

        [SerializeField]
        int slot;

    }
}
