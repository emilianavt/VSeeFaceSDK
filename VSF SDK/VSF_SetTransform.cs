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
        [Tooltip("When enabled, automatic transform updates are performed in LateUpdate.")]
        public bool updateLate = false;
        [Tooltip("When enabled, coordinates are interpreted as local instead of world coordinates. Setting scale is only supported when this option is enabled.")]
        public bool coordinatesAreLocal = true;
        
        [Header("Transform source")]
        [Tooltip("When set, the position, rotation and scale fields are ignored and the set transform is used as the source for vectors that are applied. The position, rotation and scale fields get updated accordingly.")]
        public Transform sourceTransform = null;
        [Tooltip("When enabled, the global position and rotation are used instead of local ones. Scale is always only ever applied and sourced as a local scale.")]
        public bool sourceGlobalVectors = false;

        public void SetPosition() {
            if (sourceTransform != null) {
                if (sourceGlobalVectors)
                    position = sourceTransform.position;
                else
                    position = sourceTransform.localPosition;
            }
            if (coordinatesAreLocal)
                transform.localPosition = position;
            else
                transform.position = position;
        }
        
        public void SetRotation() {
            if (sourceTransform != null) {
                if (sourceGlobalVectors)
                    rotation = sourceTransform.eulerAngles;
                else
                    rotation = sourceTransform.localEulerAngles;
            }
            if (coordinatesAreLocal)
                transform.localEulerAngles = rotation;
            else
                transform.eulerAngles = rotation;
        }
        
        public void SetScale() {
            if (sourceTransform != null) {
                if (!sourceGlobalVectors)
                    scale = sourceTransform.localScale;
            }
            if (coordinatesAreLocal)
                transform.localScale = scale;
        }
        
        public void SetPositionValue(Vector3 v) {
            this.position = v;
        }
        public void SetRotationValue(Vector3 v) {
            this.rotation = v;
        }
        public void SetScaleValue(Vector3 v) {
            this.scale = v;
        }
        public void SetAutoSetPosition(bool v) {
            autoSetPosition = v;
        }
        public void SetAutoSetRotation(bool v) {
            autoSetRotation = v;
        }
        public void SetAutoSetScale(bool v) {
            autoSetScale = v;
        }
        public void SetUpdateLate(bool v) {
            updateLate = v;
        }
        public void SetCoordinatesAreLocal(bool v) {
            coordinatesAreLocal = v;
        }
        public void SetSourceTransform(Transform v) {
            sourceTransform = v;
        }
        public void SetSourceGlobalVectors(bool v) {
            sourceGlobalVectors = v;
        }
        
        public void SetAll() {
            SetPosition();
            SetRotation();
            SetScale();
        }
        
        void RunUpdates() {
            if (autoSetPosition)
                SetPosition();
            if (autoSetRotation)
                SetRotation();
            if (autoSetScale)
                SetScale();
        }
        
        void Update() {
            if (!updateLate)
                RunUpdates();
        }
        
        void LateUpdate() {
            if (updateLate)
                RunUpdates();
        }
    }
}