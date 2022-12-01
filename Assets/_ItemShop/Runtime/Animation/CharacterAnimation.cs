using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    //[CreateAssetMenu(menuName = "")]
    public class CharacterAnimation : ScriptableObject
    {
        [field: SerializeField]
        public List<Sprite> Keys { get; private set; } = new();

        //[field: SerializeField]
        //public int FramesPerSecond { get; set; }

        [field: SerializeField]
        public float Delay { get; set; }
    }
}
