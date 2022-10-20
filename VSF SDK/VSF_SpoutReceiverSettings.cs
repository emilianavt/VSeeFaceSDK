using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    public class VSF_SpoutReceiverSettings : MonoBehaviour {
        [Tooltip("When enabled, the spout receiver in VSeeFace will perform a colorspace conversion from sRGB to Linear. As this requires an additional blit, there is a slight performance impact.")]
        public bool convertToLinear = true;
    }
}