using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Animations;
using UnityEngine.Playables;
using VRM;
#endif

namespace VSeeFace
{
    [RequireComponent(typeof(VRM.VRMBlendShapeProxy))]
    public class VSF_Animations : MonoBehaviour
    {
        [Serializable]
        public struct BlendshapeAnimPair
        {
            public string blendshapeName;

            public AnimationClip animation;

            public bool disableIK;

            public BlendshapeAnimPair(string blendshapeName, AnimationClip animation, bool disableIK)
            {
                this.blendshapeName = blendshapeName;
                this.animation = animation;
                this.disableIK = disableIK;
            }
        }

        public BlendshapeAnimPair[] animations;

#if UNITY_EDITOR
        [HideInInspector]
        public bool enablePreview = false;
        private Vector2 scrollViewVector = Vector2.zero;
        private VRMBlendShapeProxy proxy;
        private Animator animator;
        private string currentExpression = "Neutral";
        private BlendShapeKey blendShapeKey = BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral);
        private PlayableGraph graph;
        private bool haveGraph = false;
        private int[] toGraph;
        private AnimationMixerPlayable mixer;
        private bool hadEnabled = false;

        void EnablePreview() {
            if (!Application.isEditor || haveGraph)
                return;
            
            if ((Camera.main.transform.position - new Vector3(0f, 1f, -10f)).magnitude < 0.01f) {
                Debug.Log("Default camera detected, moving");
                Camera.main.transform.position = new Vector3(0f, 1f, 2f);
                Camera.main.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            
            proxy = GetComponent<VRMBlendShapeProxy>();
            animator = GetComponent<Animator>();
            graph = PlayableGraph.Create("Avatar Animation " + UnityEngine.Random.Range(0, 1000000));
            var output = AnimationPlayableOutput.Create(graph, "BaseOutput", animator);
            mixer = AnimationMixerPlayable.Create(graph, 1);
            output.SetSourcePlayable(mixer);
            toGraph = new int[animations.Length];
            
            var tpose = AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GUIDToAssetPath("3293cdd72fddb9e428a98388947b4697"));
            var tposePlayable = AnimationClipPlayable.Create(graph, tpose);
            tposePlayable.SetApplyFootIK(false);
            mixer.SetInputCount(1);
            mixer.ConnectInput(0, tposePlayable, 0);
            mixer.SetInputWeight(0, 1);
            
            int anims = 1;
            for (int i = 0; i < animations.Length; i++) {
                var clip = animations[i].animation;
                toGraph[i] = -1;
                if (clip == null)
                    continue;
                toGraph[i] = anims;
                
                var clipPlayable = AnimationClipPlayable.Create(graph, clip);
                clipPlayable.SetApplyFootIK(false);
                
                mixer.SetInputCount(anims + 1);
                mixer.ConnectInput(anims, clipPlayable, 0);
                mixer.SetInputWeight(anims, 0);
                anims++;
                
            }
            graph.Play();
            haveGraph = true;
        }
        
        void OnGUI() {
            if (!Application.isEditor || !enablePreview)
                return;
            if (!hadEnabled) {
                hadEnabled = true;
                EnablePreview();
                return;
            }

            bool clicked = false;
            scrollViewVector = GUI.BeginScrollView(new Rect(0, 0, 116, Screen.height), scrollViewVector, new Rect(0, 0, 100, proxy.BlendShapeAvatar.Clips.Count * 23 + 23));
            if (GUI.Button(new Rect(0, 0, 100, 22), "[Hide]")) {
                enablePreview = false;
                hadEnabled = false;
                return;
            }
            int k = 1;
            foreach (var values in proxy.GetValues()) {
                if (GUI.Button(new Rect(0, 23 * k, 100, 22), values.Key.Name)) {
                    currentExpression = values.Key.Name;
                    blendShapeKey = values.Key;
                    clicked = true;
                }
                k++;
            }
            GUI.EndScrollView();

            if (clicked) {
                foreach (var values in proxy.GetValues()) {
                    if (blendShapeKey.Name.ToUpper() == values.Key.Name.ToUpper()) {
                        proxy.ImmediatelySetValue(values.Key, 1f);
                    } else {
                        proxy.ImmediatelySetValue(values.Key, 0f);
                    }
                }
                mixer.SetInputWeight(0, 1);
                for (int i = 0; i < animations.Length; i++) {
                    if (animations[i].blendshapeName.ToUpper() == currentExpression.ToUpper()) {
                        if (toGraph[i] > -1) {
                            mixer.SetInputWeight(toGraph[i], 1);
                            mixer.GetInput(toGraph[i]).SetTime(0);
                        }
                        if (animations[i].animation != null && animations[i].animation.humanMotion)
                            mixer.SetInputWeight(0, 0);
                    } else {
                        if (toGraph[i] > -1)
                            mixer.SetInputWeight(toGraph[i], 0);
                    }
                }
            }
        }
        
        void OnApplicationQuit() {
            if (haveGraph)
                graph.Destroy();
            haveGraph = false;
        }

        void OnDestroy() {
            if (haveGraph)
                graph.Destroy();
            haveGraph = false;
        }
#endif
    }
}
