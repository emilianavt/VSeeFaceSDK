using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectAmbientOcclusion : MonoBehaviour, IEffectOverride
    {
        [Header("Ambient occlusion")]
        public bool enabledAmbientOcclusion = false;
        [Range(0f,4f)]
        public float ambientOcclusionIntensity = 0.1f;
        [Range(0f,0.5f)]
        public float ambientOcclusionRadius = 0.25f;
        [Range(0,4)]
        public int ambientOcclusionQuality = 3;
        public Color ambientOcclusionColor = Color.black;
        
        public void SetEnabled(bool v) {
            enabledAmbientOcclusion = v;
        }
        public void SetIntensity(float v) {
            ambientOcclusionIntensity = v;
        }
        public void SetRadius(float v) {
            ambientOcclusionRadius = v;
        }
        public void SetQuality(int v) {
            ambientOcclusionQuality = v;
        }
        public void SetColor(Color v) {
            ambientOcclusionColor = v;
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