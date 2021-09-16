using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    public class VSF_Configuration : MonoBehaviour
    {
        [Tooltip("When enabled, twist relaxers will not be added to the model's arms automatically. Use this if you set up your own twist relaxers, otherwise keep it disabled.")]
        public bool disableAutomaticTwistRelaxers = false;
        [Tooltip("When enabled, you can override the height of the model manually. The model's height is used to adjust the scaling of the tracking space. This can be used in case the automatic calculation fails for some reason.")]
        public bool overrideModelHeight = false;
        [Tooltip("The value of this field is used as the model height if \"Override Model Height\" is enabled.")]
        public float modelHeight = 0f;
        [Tooltip("The value of this field is used as the offset of the model from the ground if \"Override Model Height\" is enabled.")]
        public float modelGroundOffset = 0f;
        [Tooltip("When this option is enabled, the default reflection probe in VSeeFace, which takes its color from the ambient lighting, will be disabled.")]
        public bool disableDefaultReflectionProbe = false;
    }
}