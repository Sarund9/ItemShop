using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemShop
{
    [Serializable]
    public struct SceneRef
    {
        [SerializeField]
        string path;
        

        Scene _scene;

        public Scene Scene
        {
            get
            {
                if (!_scene.IsValid())
                    _scene = SceneManager.GetSceneByPath(path);
                return _scene;
            }
        }

        public bool IsLoaded => Scene.isLoaded;

        public int BIndex => Scene.buildIndex;

        //public void Load() =>
        //    SceneManager.LoadScene(path, LoadSceneMode.Additive);

        public AsyncOperation LoadAsync(bool allowRepeatLoad = false)
        {
            var loaded = SceneManager.GetSceneByPath(path).IsValid();
            if (allowRepeatLoad || !loaded)
                return SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);
            return null;
        }

        public AsyncOperation UnloadAsync() =>
            SceneManager.UnloadSceneAsync(path);
    }

}

