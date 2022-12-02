using UnityEngine;

namespace ItemShop
{
    [CreateAssetMenu(menuName = "ItemShop/Item/Category")]
    public class ItemCategory : ScriptableObject
    {
        [field: SerializeField]
        public string CategoryName { get; set; }


        public override string ToString()
        {
            return $"Category '{CategoryName}'";
        }
    }
}
