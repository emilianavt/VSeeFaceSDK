using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component can be used to set int type parameters of custom animators. The parameter value can be modified through Unity animations.
    public class VSF_SetAnimatorInt : MonoBehaviour
    {
        [Tooltip("This is the custom animator for which the int value should be set.")]
        public Animator targetAnimator = null;

        [Tooltip("This is the name of the parameter that should be set.")]
        public string parameterName = "Int parameter";

        [Tooltip("This is the value for the parameter that should be set. It can be modified through Unity animations.")]
        public int parameterValue = 0;
        
        public void SetName(string v) {
            parameterName = v;
        }
        public void SetValue(int v) {
            parameterValue = v;
        }

        public void Update() {
            if (targetAnimator == null)
                targetAnimator = gameObject.GetComponent<Animator>();
            targetAnimator.SetInteger(parameterName, parameterValue);
        }
    }
}