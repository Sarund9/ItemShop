using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ItemShop;
using System.Linq;
using System.IO;

namespace ItemShopEditor
{
    public static class Utils
    {

        #region Create Menu
        const string MENU_ID = "Assets/Create/ItemShop/SpriteAnimation";
        static Object[] SelectedAssetObjects()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            return AssetDatabase.LoadAllAssetsAtPath(path);
        }

        [MenuItem(MENU_ID, priority = 0)]
        static void CreateCharacterAnimation()
        {
            var objs = SelectedAssetObjects()
                .Where(o => o is Sprite)
                .Cast<Sprite>()
                .ToArray();

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            CharacterAnimationCreateWindow.OpenFrom(objs, path);
        }
        [MenuItem(MENU_ID, validate = true)]
        static bool CreateCharacterAnimation_Validate() =>
            SelectedAssetObjects()
            .Any(o => o is Sprite);
        #endregion



    }

    public class CharacterAnimationCreateWindow : EditorWindow
    {

        public static void OpenFrom(Sprite[] sprites, string path)
        {
            if (sprites == null || sprites.Length == 0)
                return;
            var win = GetWindow<CharacterAnimationCreateWindow>();
            win.titleContent = new GUIContent("Create new Character Animation");
            win.sprites = sprites;
            win.path = path;
        }

        Sprite[] sprites;
        int splitInto = 4;
        string path;

        private void OnGUI()
        {
            EditorGUILayout.PrefixLabel("Split Into");
            splitInto = EditorGUILayout.IntSlider(splitInto, 1, 20);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create"))
            {
                int step = sprites.Length / splitInto;
                for (int i = 0; i < splitInto; i++)
                {
                    int from = i * step;
                    int to = (i + 1) * step;
                    CreateAnimation(from, to, i);
                }
            }
        }

        void CreateAnimation(int from, int to, int index)
        {
            var newAnim = CreateInstance<SpriteAnimation>();

            newAnim.Keys.AddRange(sprites[from..to]);
            newAnim.Delay = .2f;

            path = path[..path.LastIndexOf('.')];
            path += $"_{index}.asset";

            AssetDatabase.CreateAsset(newAnim, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

