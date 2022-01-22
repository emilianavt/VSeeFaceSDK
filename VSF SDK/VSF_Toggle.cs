using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component can be used to toggle objects on/off depending on their current state. It is designed to be called from a VSF_Trigger or Unity UI Button component.
    public class VSF_Toggle : MonoBehaviour
    {
        [Tooltip("This object will be toggled when the Toggle() function is called. If no object is set, the object this component is on will be toggled.")]
        public GameObject targetObject = null;
        
        public void Toggle() {
            if (targetObject == null)
                targetObject = gameObject;
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
