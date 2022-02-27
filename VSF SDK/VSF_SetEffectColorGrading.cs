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
        
        public void SetEnabled(bool v) {
            enabledColorGrading = v;
        }
        public void SetTemperature(float v) {
            colorGradingTemperature = v;
        }
        public void SetTint(float v) {
            colorGradingTint = v;
        }
        public void SetHue(float v) {
            colorGradingHue = v;
        }
        public void SetSaturation(float v) {
            colorGradingSaturation = v;
        }
        public void SetBrightness(float v) {
            colorGradingBrightness = v;
        }
        public void SetContrast(float v) {
            colorGradingContrast = v;
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