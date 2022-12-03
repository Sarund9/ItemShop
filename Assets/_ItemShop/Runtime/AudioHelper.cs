using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class AudioHelper : MonoBehaviour
    {
        Coroutine blocking;

        public void PlayGlobal(AudioClip clip)
        {
            GM.RunGlobalCoroutine(Cor(clip));

            // 1 Frame Delay exists to allow blocking
            IEnumerator Cor(AudioClip clip)
            {
                yield return null;
                if (blocking == null)
                    GM.PlaySound(clip);
            }
        }
        public void Block(float duration)
        {
            if (blocking != null)
                GM.StopGlobalCoroutine(blocking);
            blocking = GM.RunGlobalCoroutine(Cor(duration));

            IEnumerator Cor(float duration)
            {
                yield return new WaitForSeconds(duration);
                blocking = null;
            }
        }

    }
}
