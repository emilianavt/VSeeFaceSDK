using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectLensDistortion : MonoBehaviour, IEffectOverride
    {
        [Header("Lens distortion")]
        public bool enabledLensDistortion = false;
        [Range(-100f,100f)]
        public float lensDistortionIntensity = 71f;
        [Range(0f,1f)]
        public float lensDistortionXMultiplier = 1;
        [Range(0f,1f)]
        public float lensDistortionYMultiplier = 1;
        [Range(-1f,1f)]
        public float lensDistortionCenterX = 0;
        [Range(-1f,1f)]
        public float lensDistortionCenterY = 0;
        [Range(0.01f,5f)]
        public float lensDistortionScale = 1;
        
        public void SetEnabled(bool v) {
            enabledLensDistortion = v;
        }
        public void SetIntensity(float v) {
            lensDistortionIntensity = v;
        }
        public void SetXMultiplier(float v) {
            lensDistortionXMultiplier = v;
        }
        public void SetYMultiplier(float v) {
            lensDistortionYMultiplier = v;
        }
        public void SetCenterX(float v) {
            lensDistortionCenterX = v;
        }
        public void SetCenterY(float v) {
            lensDistortionCenterY = v;
        }
        public void SetScale(float v) {
            lensDistortionScale = v;
        }

        private int id = -1;
        private IEffectApplier applier = null;
        
        public void Register(IEffectApplier applier, int id) {
            this.applier = applier;
            this.id = id;
        }
        
        public void Update() {
            if (applier != null)
                applier.Apply(id);
        }
    }
}