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

        bool active;


        bool CanTalk() =>
            Vector3.Distance(transform.position,
                PlayerController.Instance.transform.position) < talkDistance;

        public void Talk()
        {
            if (CanTalk())
            {
                StartCoroutine(InRange());
            }

            IEnumerator InRange()
            {
                var p = PlayerController.Instance;
                button.SetActive(false);
                yield return new WaitWhile(CanTalk);
                button.SetActive(true);
            }
        }
    }
}
