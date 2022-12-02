using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class NPC : MonoBehaviour
    {


        [SerializeField]
        GameObject button;

        [SerializeField]
        float talkDistance = 5;

        [SerializeField]
        DialoguePool pool;

        [SerializeField]
        Shop shop;

        bool active;

        bool CanTalk() =>
            Vector3.Distance(transform.position,
                PlayerController.Instance.transform.position) < talkDistance;

        public void Talk()
        {
            if (CanTalk() && DialoguePlayer.Exists(out var p))
            {
                StartCoroutine(Cor(p));
            }

            IEnumerator Cor(DialoguePlayer p)
            {
                p.DisplayLine(pool.GetRandomLine());
                p.ClearOptions();
                active = true;
                p.CreateOption("Back", () => active = false);
                if (shop)
                {
                    p.CreateOption("Shop", () =>
                        PlayerInventory.Instance.OpenShop(shop)
                    );
                }

                button.SetActive(false);
                yield return new WaitWhile(() => CanTalk() && p.DisplayActive && active);
                active = false;
                button.SetActive(true);
                p.Close();
            }
        }
    }
}
