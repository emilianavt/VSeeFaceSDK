using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component will temporarily override effect settings while active. Parameters can be controlled through Unity animations.
    public class VSF_SetEffectHalftone : MonoBehaviour, IEffectOverride
    {
        [Header("Dot effect")]
        public bool enabledDot = false;
        public Color dotColor = Color.white;
        [Range(0f, 1f), Tooltip("Any pixels below this threshold will be passed through as black.")]
        public float dotThresholdLow = 0.1f;
        [Range(0f, 1f), Tooltip("Any pixels above this threshold will be passed through as white.")]
        public float dotThresholdHigh = 0.9f;
        [Range(3f, 60f), Tooltip("The size of a single color dot area.")]
        public float dotAreaSize = 4f;
        [Range(0.3f, 4f), Tooltip("A size factor applied to the dot within this area, allowing it to grow bigger or smaller.")]
        public float dotDotSize = 1.75f;
        [Tooltip("If set to true, transparency will be copied from the input, otherwise dots will become opaque while the background becomes transparent.")]
        public bool dotPreserveAlpha = true;
        
        public void SetEnabled(bool v) {
            enabledDot = v;
        }
        public void SetColor(Color v) {
            dotColor = v;
        }
        public void SetThresholdLow(float v) {
            dotThresholdLow = v;
        }
        public void SetThresholdHigh(float v) {
            dotThresholdHigh = v;
        }
        public void SetAreaSize(float v) {
            dotAreaSize = v;
        }
        public void SetDotSize(float v) {
            dotDotSize = v;
        }
        public void SetPreserveAlpha(bool v) {
            dotPreserveAlpha = v;
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