using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
