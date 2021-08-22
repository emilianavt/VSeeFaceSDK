using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component is not necessary anymore and should not be used.
    public class VSF_IKFollower : MonoBehaviour
    {
        private Transform target;
        private Vector3 pos;
        private Quaternion rot;
        private Vector3 scale;
        private Vector3 ikPos;
        private Quaternion ikRot;
        private Transform baseTarget = null;
        
        void Start() {
            baseTarget = GetComponentInParent<VRM.VRMMeta>().transform;
            target = transform.parent;
            pos = transform.localPosition;
            rot = transform.localRotation;
            scale = transform.localScale;
        }
        
        void Update() {
            if (baseTarget != null)
                transform.SetParent(baseTarget, true);
            transform.position = ikPos;
            transform.rotation = ikRot;
        }
        
        void LateUpdate() {
            if (target == null) {
                Destroy(this.gameObject);
                return;
            }
            transform.SetParent(target, true);
            transform.localPosition = pos;
            transform.localRotation = rot;
            transform.localScale = scale;
            ikPos = transform.position;
            ikRot = transform.rotation;
        }
    }
}