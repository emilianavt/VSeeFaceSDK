using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEngine.UIElements;
using UnityEditorInternal;
using VSeeFace;
using System;

[CustomEditor(typeof(VSF_Animations))]
public class VSF_AnimationsEditor : Editor
{
    ReorderableList animationList;

    private void OnEnable()
    {
        var animationProp = serializedObject.FindProperty("animations");
        var bsavatar = (target as VSF_Animations).GetComponent<VRM.VRMBlendShapeProxy>().BlendShapeAvatar;
        string[] options = new string[bsavatar.Clips.Count];
        for (int i = 0; i < options.Length; i++)
        {
            if (bsavatar.Clips[i] != null)
                options[i] = bsavatar.Clips[i].Key.ToString();
        }

        animationList = new ReorderableList(serializedObject, animationProp);

        animationList.elementHeight = EditorGUIUtility.singleLineHeight * 3;

        animationList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = animationProp.GetArrayElementAtIndex(index);
            var bsname = element.FindPropertyRelative("blendshapeName");

            List<string> restrictedOptions = new List<string>();
            HashSet<string> bsToRemove = new HashSet<string>();
            int idx = 0;
            foreach (SerializedProperty prop in animationProp)
            {
                if (idx != index)
                {
                    bsToRemove.Add(prop.FindPropertyRelative("blendshapeName").stringValue);
                }
                idx++;
            }

            int nameIndex = -1;
            string bs = bsname.stringValue;
            for (int i = 0; i < options.Length; i++)
            {
                if (!bsToRemove.Contains(options[i]))
                {
                    restrictedOptions.Add(options[i]);

                    if (nameIndex == -1 && bs.Equals(options[i], System.StringComparison.OrdinalIgnoreCase))
                    {
                        nameIndex = restrictedOptions.Count - 1;
                    }
                }
            }

            if (nameIndex == -1)
                nameIndex = 0;
            nameIndex = EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "Blendshape", nameIndex, restrictedOptions.ToArray());
            if (nameIndex >= restrictedOptions.Count)
                nameIndex = 0;
            bsname.stringValue = restrictedOptions[nameIndex];

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("animation"), new GUIContent("Animation"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("disableIK"), new GUIContent("IK disabled when active"));
        };

        animationList.onAddCallback = (list) =>
        {
            var arrayProperty = list.serializedProperty;
            arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
            var newProperty = arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1);
            newProperty.FindPropertyRelative("blendshapeName").stringValue = "";
            newProperty.FindPropertyRelative("animation").objectReferenceValue = null;
        };

        animationList.onCanAddCallback = (list) =>
        {
            return list.serializedProperty.arraySize < options.Length;
        };
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        (target as VSF_Animations).enablePreview = EditorGUILayout.Toggle("Enable preview", (target as VSF_Animations).enablePreview);
        serializedObject.UpdateIfRequiredOrScript();

        if (animationList != null)
            animationList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
        EditorGUI.EndChangeCheck();
    }
}
