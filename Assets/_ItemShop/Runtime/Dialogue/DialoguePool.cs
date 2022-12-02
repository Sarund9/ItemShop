using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    [CreateAssetMenu(menuName = "ItemShop/Dialogue Line Pool")]
    public class DialoguePool : ScriptableObject
    {
        
        [field: SerializeField]
        public List<string> Lines { get; private set; } = new();

        int current = 0;


        public string GetRandomLine()
        {
            if (Lines.Count == 0)
                return "<Err: No Lines>";
            //return Lines[Random.Range(0, Lines.Count)];

            current += Random.Range(2, Lines.Count - 1);
            current %= Lines.Count;
            return Lines[current];
        }
    }
}
