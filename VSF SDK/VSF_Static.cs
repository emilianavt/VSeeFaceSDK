using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    public class VSF_Static : MonoBehaviour
    {
        private Vector3 pos = Vector3.zero;
        private Quaternion rot = Quaternion.identity;
        
        private bool initialized = false;
        
        public void Start() {
            if (initialized)
                return;
            pos = transform.position;
            rot = transform.rotation;
            initialized = true;
        }
        
        void LateUpdate() {
            transform.position = pos;
            transform.rotation = rot;
        }
    }
}