using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    [CreateAssetMenu(menuName = "ItemShop/Item/Item")]
    public class Item : ScriptableObject
    {

        [field: SerializeField]
        public string ItemName { get; set; }

        [field: SerializeField]
        public int Price { get; private set; } = 20;

        [field: SerializeField]
        public Sprite Sprite { get; set; }

        [field: SerializeField]
        public float SpriteScale { get; set; } = 1f;

        [field: SerializeField]
        public Vector2 SpriteOffset { get; set; }

        [field: SerializeField]
        public Color Color { get; set; } = Color.white;

        [field: SerializeField]
        public ItemCategory Category { get; set; }
        
        [field: SerializeField]
        public string Description { get; private set; }

        [field: SerializeField]
        public int OrderInLayer { get; private set; }

        // Animations for the Character
        public SpriteAnimation UpAnim, DownAnim, LeftAnim, RightAnim;


        public override string ToString()
        {
            return $"Item '{ItemName}'";
        }

    }
}
