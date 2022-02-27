using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component can be used to set float type parameters of custom animators. The parameter value can be modified through Unity animations.
    public class VSF_SetAnimatorFloat : MonoBehaviour
    {
        [Tooltip("This is the custom animator for which the float value should be set.")]
        public Animator targetAnimator = null;

        [Tooltip("This is the name of the parameter that should be set.")]
        public string parameterName = "Float parameter";

        [Tooltip("This is the value for the parameter that should be set. It can be modified through Unity animations.")]
        public float parameterValue = 0f;
        
        public void SetName(string v) {
            parameterName = v;
        }
        public void SetValue(float v) {
            parameterValue = v;
        }
        
        public void Update() {
            if (targetAnimator == null)
                targetAnimator = gameObject.GetComponent<Animator>();
            targetAnimator.SetFloat(parameterName, parameterValue);
        }
    }
}