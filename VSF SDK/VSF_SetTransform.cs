using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component allows setting a game object's transform. Settings can be adjusted through Unity animations.
    public class VSF_SetTransform : MonoBehaviour
    {
        [Header("Target settings")]
        [Tooltip("This is the position that this game object's transform will be set to.")]
        public Vector3 position = Vector3.zero;
        [Tooltip("This is the rotation that this game object's transform will be set to.")]
        public Vector3 rotation = Vector3.zero;
        [Tooltip("This is the scale that this game object's transform will be set to.")]
        public Vector3 scale = Vector3.one;
        
        [Header("Update settings")]
        [Tooltip("When enabled, the position is set every frame.")]
        public bool autoSetPosition = false;
        [Tooltip("When enabled, the rotation is set every frame.")]
        public bool autoSetRotation = false;
        [Tooltip("When enabled, the scale is set every frame.")]
        public bool autoSetScale = false;
        [Tooltip("When enabled, coordinates are interpreted as local instead of world coordinates. Setting scale is only supported when this option is enabled.")]
        public bool coordinatesAreLocal = true;

        public void SetPosition() {
            if (coordinatesAreLocal)
                transform.localPosition = position;
            else
                transform.position = position;
        }
        
        public void SetRotation() {
            if (coordinatesAreLocal)
                transform.localEulerAngles = rotation;
            else
                transform.eulerAngles = rotation;
        }
        
        public void SetScale() {
            if (coordinatesAreLocal)
                transform.localScale = scale;
        }
        
        public void SetAll() {
            SetPosition();
            SetRotation();
            SetScale();
        }
        
        void Update() {
            if (autoSetPosition)
                SetPosition();
            if (autoSetRotation)
                SetRotation();
            if (autoSetScale)
                SetScale();
        }
    }
}