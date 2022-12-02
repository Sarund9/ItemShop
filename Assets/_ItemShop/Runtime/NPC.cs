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

        bool active;


        bool CanTalk() =>
            Vector3.Distance(transform.position,
                PlayerController.Instance.transform.position) < talkDistance;

        public void Talk()
        {
            if (CanTalk() && DialoguePlayer.Exists(out var p))
            {
                StartCoroutine(InRange(p));
            }

            IEnumerator InRange(DialoguePlayer p)
            {
                p.DisplayLine(pool.GetRandomLine());
                button.SetActive(false);
                yield return new WaitWhile(CanTalk);
                button.SetActive(true);
                p.Close();
            }
        }
    }
}
