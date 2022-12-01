using ItemShop;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace ItemShopEditor
{
    [CustomPropertyDrawer(typeof(SceneRef))]
    public class SceneRefDrawer : PropertyDrawer
    {


        SceneAsset asset;

        public override void OnGUI(Rect position,
            SerializedProperty property, GUIContent label)
        {
            var scenePathProperty = property.FindPropertyRelative("path");

            Debug.Assert(scenePathProperty != null);
            Debug.Assert(scenePathProperty.type == "string");

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            if (!asset)
            {
                asset = AssetDatabase.LoadAssetAtPath<SceneAsset>
                    (scenePathProperty.stringValue);
            }

            var sceneName = Path.GetFileNameWithoutExtension(scenePathProperty.stringValue);

            var newValue = EditorGUI
                .ObjectField(position,
                    new GUIContent(sceneName, scenePathProperty.stringValue),
                    asset, typeof(SceneAsset), false
                ) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                if (!newValue)
                {
                    scenePathProperty.stringValue = "";
                    asset = null;
                }
                else
                {
                    scenePathProperty.stringValue = AssetDatabase.GetAssetPath(newValue);
                }
            }

            EditorGUI.EndProperty();
        }

    }
}

