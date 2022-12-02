using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    [CreateAssetMenu(menuName = "ItemShop/Item/Selection")]
    public class ShopSelection : ScriptableObject
    {

        [field: SerializeField]
        public List<Item> Items { get; private set; }

        public Item this[int i]
        {
            get => Items[i];
            set => Items[i] = value;
        }

        public int Count => Items.Count;
    }
}
