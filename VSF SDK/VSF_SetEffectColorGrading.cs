using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectColorGrading : MonoBehaviour, IEffectOverride
    {
        [Header("Color grading")]
        public bool enabledColorGrading = false;
        [Range(-100f,100f)]
        public float colorGradingTemperature = 0f;
        [Range(-100f,100f)]
        public float colorGradingTint = 0f;
        [Range(-180f,180f)]
        public float colorGradingHue = 0f;
        [Range(-100f,100f)]
        public float colorGradingSaturation = 0f;
        [Range(-100f,100f)]
        public float colorGradingBrightness = 0f;
        [Range(-100f,100f)]
        public float colorGradingContrast = 0f;

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