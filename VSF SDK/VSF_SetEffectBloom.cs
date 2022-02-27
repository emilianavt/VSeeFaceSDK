using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectBloom : MonoBehaviour, IEffectOverride
    {
        [Header("Bloom")]
        public bool enabledBloom = false;
        public bool bloomFixAlpha = true;
        [Range(0f,2f)]
        public float bloomIntensity = 0.5f;
        [Range(0f,2f)]
        public float bloomThreshold = 1f;
        [Range(0f,10f)]
        public float bloomDiffusion = 8f;
        public Color bloomColor = new Color(0.57f, 0.33f, 0.33f);
        [Range(0f,16f)]
        public float bloomColorIntensity = 3f;
        [Range(0.5f,4f)]
        public float bloomAlpha = 1f;
        
        public void SetEnabled(bool v) {
            enabledBloom = v;
        }
        public void SetFixAlpha(bool v) {
            bloomFixAlpha = v;
        }
        public void SetIntensity(float v) {
            bloomIntensity = v;
        }
        public void SetThreshold(float v) {
            bloomThreshold = v;
        }
        public void SetDiffusion(float v) {
            bloomDiffusion = v;
        }
        public void SetColor(Color v) {
            bloomColor = v;
        }
        public void SetColorIntensity(float v) {
            bloomColorIntensity = v;
        }
        public void SetAlpha(float v) {
            bloomAlpha = v;
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