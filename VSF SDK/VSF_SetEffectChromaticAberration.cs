using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectChromaticAberration : MonoBehaviour, IEffectOverride
    {
        [Header("Chromatic aberration")]
        public bool enabledChromaticAberration = false;
        [Range(0f,1f)]
        public float chromaticAberrationIntensity = 0.609f;
        
        public void SetEnabled(bool v) {
            enabledChromaticAberration = v;
        }
        public void SetAberrationIntensity(float v) {
            chromaticAberrationIntensity = v;
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