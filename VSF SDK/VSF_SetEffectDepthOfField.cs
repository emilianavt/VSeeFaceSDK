using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectDepthOfField : MonoBehaviour, IEffectOverride
    {
        [Header("Lens distortion")]
        public bool enabledDepthOfField = false;
        [Min(0.1f)]
        public float dofFocusDistance = 1.95f;
        [Range(0.05f, 32f)]
        public float dofAperture = 3.5f;
        [Range(1f, 300f)]
        public float dofFocalLength = 145f;
        [Range(0,3)]
        public int dofMaxBlurSize = 3;
        [Header("Dynamic focus")]
        [Tooltip("When enabled, the focus distance will be set dynamically by calculating the distance between the active main camera and the target transform.")]
        public bool dofEnableDynamicFocus = false;
        public Transform dofDynamicTarget;
        
        public void SetEnabled(bool v) {
            enabledDepthOfField = v;
        }
        public void SetFocusDistance(float v) {
            dofFocusDistance = v;
        }
        public void SetAperture(float v) {
            dofAperture = v;
        }
        public void SetFocalLength(float v) {
            dofFocalLength = v;
        }
        public void SetMaxBlurSize(int v) {
            dofMaxBlurSize = v;
        }
        public void SetEnableDynamicFocus(bool v) {
            dofEnableDynamicFocus = v;
        }
        public void SetDynamicTarget(Transform v) {
            dofDynamicTarget = v;
        }

        
        private int id = -1;
        private IEffectApplier applier = null;
        
        public void Register(IEffectApplier applier, int id) {
            this.applier = applier;
            this.id = id;
        }
        
        public void Update() {
            if (applier != null) {
                if (dofEnableDynamicFocus && dofDynamicTarget != null) {
                    Camera mainCam = Camera.main;
                    if (mainCam != null) {
                        dofFocusDistance = Vector3.Distance(dofDynamicTarget.position, mainCam.transform.position);
                    }
                }
                applier.Apply(id);
            }
        }
    }
}