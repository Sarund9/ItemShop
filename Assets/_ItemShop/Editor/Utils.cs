using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ItemShop;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ItemShopEditor
{
    public static class Utils
    {

        #region Create Menu
        const string MENU_ID = "Assets/Create/ItemShop/Sequenced Sprite Animations";
        static Object[] SelectedAssetObjects()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            return AssetDatabase.LoadAllAssetsAtPath(path);
        }

        static readonly Regex find_index = new(@"_\d+$");

        [MenuItem(MENU_ID, priority = 0)]
        static void CreateCharacterAnimation()
        {
            var sprites = SelectedAssetObjects()
                .Where(o => o is Sprite)
                .Cast<Sprite>()
                // Perform match, store result as Tuple
                .Select(o => (sprite: o, match: find_index.Match(o.name)))
                // Filter for invalids
                .Where(t => t.match != null)
                // Order
                .OrderBy(s => {
                    var n = s.match.Value[1..];
                    
                    return int.Parse(n);
                })
                .Select(t => t.sprite)
                .ToArray();

            /*
            Sorting is required because unity will not give the sprites in Numbered or Imported order
            Sorting by name will produce wrong results due to digid diferences (eg _24 will be before _6)

             */


            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            var name = Path.GetFileNameWithoutExtension(path);


            //var sb = new StringBuilder("SPRITES:\n");
            //foreach (var sprite in sprites)
            //{
            //    sb.AppendLine(sprite.name);
            //}
            //Debug.Log(sb.ToString());
            //return;

            if (sprites.Length != 36)
                return;

            path = Path.GetDirectoryName(path) + "/Animations/";

            CreateAnimation(sprites[0..8], $"{path}{name}_up.asset");
            CreateAnimation(sprites[9..17], $"{path}{name}_left.asset");
            CreateAnimation(sprites[18..26], $"{path}{name}_down.asset");
            CreateAnimation(sprites[27..35], $"{path}{name}_right.asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            static void CreateAnimation(Sprite[] sprites, string path)
            {
                var newAnim = ScriptableObject.CreateInstance<SpriteAnimation>();

                //Debug.Log($"Create asset: {path}");

                newAnim.Keys.AddRange(sprites);
                newAnim.Delay = .2f;

                AssetDatabase.CreateAsset(newAnim, path);
            }
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

