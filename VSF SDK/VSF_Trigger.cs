using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VSeeFace {
    // This component is used to execute certain actions when certain events occur. To get dynamic toggle functionality instead of fixed enabling/disabling, this can be combined with a VSF_Toggle component.
    public class VSF_Trigger : MonoBehaviour
    {
        [Header("General events")]

        [Tooltip("Tick this to trigger the actions below when this component is first enabled.")]
        public bool onEnable = false;

        [Tooltip("Tick this to trigger the actions below on every frame when this component is enabled except the first. Combining it with onEnable will cause the actions to be executed on every frame when the component is enabled.")]
        public bool onUpdateSkipOne = false;

        [Tooltip("Tick this to trigger the actions below when this component is first disabled.")]
        public bool onDisable = false;

        [Header("Timer events")]
        [Tooltip("Tick this to trigger the actions below after onTimerTime seconds once the component is enabled. Disabling the component will reset the timer.")]
        public bool onTimer = false;

        [Tooltip("Tick this keep triggering the timer event every onTimerTime seconds. Repeating timers reset to on when enabling the component.")]
        public bool onTimerRepeat = false;
        
        [Tooltip("The time used for timed triggers.")]
        public float timerTime = 0f;

        [Header("Collision events (see Unity documentation)")]
        [Tooltip("Tick this to trigger the actions below when the start of a collision is detected.")]
        public bool onCollisionEnter = false;
        [Tooltip("Tick this to trigger the actions below when the end of a collision is detected.")]
        public bool onCollisionExit = false;
        [Tooltip("Tick this to trigger the actions below when a continuing collision is detected.")]
        public bool onCollisionStay = false;
        [Tooltip("Tick this to trigger the actions below when a particle collision is detected.")]
        public bool onParticleCollision = false;

        [Tooltip("Tick this to trigger the actions below when the start of a trigger collision is detected.")]
        public bool onTriggerEnter = false;
        [Tooltip("Tick this to trigger the actions below when the end of a trigger collision is detected.")]
        public bool onTriggerExit = false;
        [Tooltip("Tick this to trigger the actions below when a continuing trigger collision is detected.")]
        public bool onTriggerStay = false;
        [Tooltip("Tick this to trigger the actions below when a particle trigger collision is detected. (Name check not supported.)")]
        public bool onParticleTrigger = false;
        
        [Tooltip("When ticked, collisions and triggers will only trigger the actions below when the name of the object causing it matches the name below.")]
        public bool nameCheck = false;
        [Tooltip("The name of the object has to match this string exactly, including capitalization.")]
        public string nameCheckName = "";

        // The API does not exist yet.
        /*[Header("API events")]
        [Tooltip("Tick this to allow triggering the actions below through an OSC API call.")]
        public bool onAPI = false;
        [Tooltip("When this name is received as the OSC address on the API server, the API event triggers.")]
        public string apiName = "";*/
        
        [Header("Actions")]
        public UnityEvent actions;
        
        private float startTime = 0f;
        private bool timerRunning = false;
        private bool firstFrame = false;

        public void Trigger() {
            actions.Invoke();
        }
        
        void OnDisable() {
            if (onDisable)
                Trigger();
        }

        void OnEnable() {
            if (onEnable)
                Trigger();
            startTime = Time.time;
            timerRunning = true;
            firstFrame = true;
        }
        
        public void SetTimerRunning(bool running) {
            timerRunning = running;
        }
        
        void Update() {
            if (onTimer && timerRunning && startTime + timerTime < Time.time) {
                if (!onTimerRepeat)
                    timerRunning = false;
                startTime = Time.time;
                Trigger();
            }
            if (onUpdateSkipOne && !firstFrame) {
                Trigger();
            }
            firstFrame = false;
        }
        
        bool CheckName(Collision collision) {
            if (!nameCheck)
                return true;
            if (collision.gameObject.name == nameCheckName)
                return true;
            return false;
        }
        
        bool CheckName(Collider collision) {
            if (!nameCheck)
                return true;
            if (collision.gameObject.name == nameCheckName)
                return true;
            return false;
        }
        
        void OnCollisionEnter(Collision collisionInfo) {
            if (onCollisionEnter && CheckName(collisionInfo))
                Trigger();
        }
        
        void OnCollisionExit(Collision collisionInfo) {
            if (onCollisionExit && CheckName(collisionInfo))
                Trigger();
        }
        
        void OnCollisionStay(Collision collisionInfo) {
            if (onCollisionStay && CheckName(collisionInfo))
                Trigger();
        }
        
        void OnParticleCollision(GameObject other) {
            if (onParticleCollision && (!nameCheck || other.name == nameCheckName))
                Trigger();
        }
        
        void OnTriggerEnter(Collider other) {
            if (onTriggerEnter && CheckName(other))
                Trigger();
        }
        
        void OnTriggerExit(Collider other) {
            if (onTriggerExit && CheckName(other))
                Trigger();
        }
        
        void OnTriggerStay(Collider other) {
            if (onTriggerStay && CheckName(other))
                Trigger();
        }

        void OnParticleTrigger() {
            if (onParticleTrigger)
                Trigger();
        }
    }
}