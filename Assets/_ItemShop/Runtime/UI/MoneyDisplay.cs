using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ItemShop
{
    public class MoneyDisplay : MonoBehaviour
    {


        [SerializeField]
        TMP_Text text;


        private void Start()
        {
            var inv = PlayerInventory.Instance;

            inv.OnMoneyChanged += MoneyChanged;
            MoneyChanged(inv.Money);
        }

        private void MoneyChanged(int ammount)
        {
            text.text = $"{ammount}";
        }
    }
}
