using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class MainMenu : MonoBehaviour
    {


        [SerializeField]
        SceneRef menu, world;


        public void Btn_Play()
        {
            // Use this so that the Routine keeps running after this object is unloaded
            GM.RunGlobalCoroutine(Cor());
            
            IEnumerator Cor()
            {
                yield return menu.UnloadAsync();
                yield return world.LoadAsync();
            }
        }


        public void Btn_Settings()
        {

        }

        public void Btn_Quit()
        {
            Application.Quit();
        }
    }
}
