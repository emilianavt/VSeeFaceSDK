using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

namespace VSeeFace {
    // This component can be used to override a VRM blend shape clip's value. It will also trigger animations linked with it through a VSF_Animations component. The value can also be controlled through Unity animations.
    public class VSF_SetBlendShapeClip : MonoBehaviour
    {
        [Tooltip("This is the name of the VRM blend shape clip that will be set while this component is active.")]
        public string blendShapeClipName = "Smile";
        
        [Tooltip("This is the value the VRM blend shape clip will be set to. This value can also be controlled through Unity animations.")]
        public float blendShapeClipValue = 1f;
        
        public void SetName(string v) {
            blendShapeClipName = v;
        }
        public void SetValue(float v) {
            blendShapeClipValue = v;
        }
        
        private VRMBlendShapeProxy proxy = null;
        private BlendShapeKey blendShapeKey;
        private bool found = false;
        
        public void Update() {
            if (proxy == null) {
                proxy = gameObject.GetComponentInParent<VRMBlendShapeProxy>();
                foreach (var clip in proxy.BlendShapeAvatar.Clips) {
                    BlendShapeKey key = BlendShapeKey.CreateFromClip(clip);
                    if (key.Name.ToUpper() == blendShapeClipName.ToUpper()) {
                        found = true;
                        blendShapeKey = key;
                        break;
                    }
                }
                if (!found) {
                    Debug.Log("VRM blend shape clip not found: " + blendShapeClipName);
                }
            }
            if (found)
                proxy.ImmediatelySetValue(blendShapeKey, blendShapeClipValue);
        }
    }
}