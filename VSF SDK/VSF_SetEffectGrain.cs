using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectGrain : MonoBehaviour, IEffectOverride
    {
        [Header("Grain")]
        public bool enabledGrain = false;
        public bool grainFixAlpha = false;
        public bool grainColored = false;
        [Range(0f,1f)]
        public float grainIntensity = 1f;
        [Range(0.3f,3f)]
        public float grainSize = 1f;
        [Range(0f,1f)]
        public float grainLuminanceContribution = 0.8f;
        [Range(0.5f,16f)]
        public float grainAlpha = 4f;
        
        public void SetEnabled(bool v) {
            enabledGrain = v;
        }
        public void SetFixAlpha(bool v) {
            grainFixAlpha = v;
        }
        public void SetColored(bool v) {
            grainColored = v;
        }
        public void SetIntensity(float v) {
            grainIntensity = v;
        }
        public void SetSize(float v) {
            grainSize = v;
        }
        public void SetLuminanceContribution(float v) {
            grainLuminanceContribution = v;
        }
        public void SetAlpha(float v) {
            grainAlpha = v;
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