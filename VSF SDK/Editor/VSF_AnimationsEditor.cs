using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEngine.UIElements;
using UnityEditorInternal;
using VSeeFace;

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
            int nameIndex = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (bsname.stringValue.Equals(options[i], System.StringComparison.OrdinalIgnoreCase))
                {
                    nameIndex = i;
                    break;
                }
            }
            nameIndex = EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "Blendshape", nameIndex, options);
            if (nameIndex >= options.Length)
                nameIndex = 0;
            bsname.stringValue = options[nameIndex];

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("animation"), new GUIContent("Animation"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("disableIK"), new GUIContent("IK disabled when active"));
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
