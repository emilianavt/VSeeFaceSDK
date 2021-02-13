using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

namespace VSeeFace {
    public class AvatarCheck {
        private readonly static HashSet<string> validTypes = new HashSet<string>() {
            "DDPPenController",
            "DDPStampController",
            "DokoDemoPainterPaintable", // No persistence or texture loading or saving is supported
            "DokoDemoPainterPen",
            "DokoDemoPainterStamp",
            "DynamicBone",
            "DynamicBoneCollider",
            "DynamicBoneColliderBase",
            "DynamicBonePlaneCollider",
            "MagicaCloth.MagicaBoneCloth",
            "MagicaCloth.MagicaBoneSpring",
            "MagicaCloth.MagicaCapsuleCollider",
            "MagicaCloth.MagicaDirectionalWind",
            "MagicaCloth.MagicaMeshCloth",
            "MagicaCloth.MagicaMeshSpring",
            "MagicaCloth.MagicaPlaneCollider",
            "MagicaCloth.MagicaRenderDeformer",
            "MagicaCloth.MagicaSphereCollider",
            "MagicaCloth.MagicaVirtualDeformer",
            "RootMotion.FinalIK.RotationLimitAngle",
            "RootMotion.FinalIK.RotationLimitHinge",
            "RootMotion.FinalIK.RotationLimitPolygonal",
            "RootMotion.FinalIK.RotationLimitSpline",
            "RootMotion.FinalIK.TwistRelaxer",
            "SPCRJointDynamicsCollider",
            "SPCRJointDynamicsController",
            "SPCRJointDynamicsPoint",
            "SPCRJointDynamicsPointGrabber",
            "TMPro.TMP_Dropdown",
            "TMPro.TMP_InputField",
            "TMPro.TextMeshPro",
            "TMPro.TextMeshProUGUI",
            "UnityEngine.Animations.AimConstraint",
            "UnityEngine.Animations.LookAtConstraint",
            "UnityEngine.Animations.ParentConstraint",
            "UnityEngine.Animations.PositionConstraint",
            "UnityEngine.Animations.RotationConstraint",
            "UnityEngine.Animations.ScaleConstraint",
            "UnityEngine.Animator",
            "UnityEngine.AudioReverbZone",
            "UnityEngine.AudioSource",
            "UnityEngine.BillboardRenderer",
            "UnityEngine.BoxCollider",
            "UnityEngine.Camera",
            "UnityEngine.Canvas",
            "UnityEngine.CanvasRenderer",
            "UnityEngine.CapsuleCollider",
            "UnityEngine.Cloth",
            "UnityEngine.EllipsoidParticleEmitter",
            "UnityEngine.FlareLayer",
            "UnityEngine.GUILayer",
            "UnityEngine.Joint",
            "UnityEngine.Light",
            "UnityEngine.LineRenderer",
            "UnityEngine.MeshCollider",
            "UnityEngine.MeshFilter",
            "UnityEngine.MeshParticleEmitter",
            "UnityEngine.MeshRenderer",
            "UnityEngine.ParticleAnimator",
            "UnityEngine.ParticleSystem",
            "UnityEngine.ParticleSystemForceField",
            "UnityEngine.ParticleSystemRenderer",
            "UnityEngine.RectTransform",
            "UnityEngine.Rigidbody",
            "UnityEngine.SkinnedMeshRenderer",
            "UnityEngine.SphereCollider",
            "UnityEngine.SpriteMask",
            "UnityEngine.SpriteRenderer",
            "UnityEngine.TextMesh",
            "UnityEngine.TrailRenderer",
            "UnityEngine.Transform",
            "UnityEngine.UI.Button",
            "UnityEngine.UI.CanvasScaler",
            "UnityEngine.UI.Dropdown",
            "UnityEngine.UI.GraphicRaycaster",
            "UnityEngine.UI.Image",
            "UnityEngine.UI.InputField",
            "UnityEngine.UI.Mask",
            "UnityEngine.UI.RawImage",
            "UnityEngine.UI.RectMask2D",
            "UnityEngine.UI.ScrollRect",
            "UnityEngine.UI.Scrollbar",
            "UnityEngine.UI.Slider",
            "UnityEngine.UI.Text",
            "UnityEngine.UI.Toggle",
            "UnityEngine.Video.VideoPlayer",
            "UnityEngine.WindZone",
            "VRM.VRMBlendShapeProxy",
            "VRM.VRMFirstPerson",
            "VRM.VRMHumanoidDescription",
            "VRM.VRMLookAtBlendShapeApplyer",
            "VRM.VRMLookAtBoneApplyer",
            "VRM.VRMLookAtHead",
            "VRM.VRMMeta",
            "VRM.VRMSpringBone",
            "VRM.VRMSpringBoneColliderGroup",
            "VSeeFace.VSF_Animations",
            "VSeeFace.VSF_Configuration",
            "VSeeFace.VSF_IKFollower",
            "VSeeFace.VSF_MainCamera",
            "VSeeFace.VSF_Static",
            "uWindowCapture.UwcAltTabWindowTextureManager",
            "uWindowCapture.UwcCursorTexture",
            "uWindowCapture.UwcIconTexture",
            "uWindowCapture.UwcManager",
            "uWindowCapture.UwcWindowTexture",
            "uWindowCapture.UwcWindowTextureChildrenManager",
            "uWindowCapture.UwcWindowTextureManager",
        };
        
        public static bool CheckAvatar(GameObject avatar, out string error)
        {
            error = "";

            if (avatar == null)
            {
                error = "No avatar selected";
                Debug.Log("CheckAvatar error: " + error);
                return false;
            }

            if (avatar.GetComponent<VRMMeta>() == null)
            {
                error = "Component missing: VRM Meta";
                Debug.LogError("CheckAvatar error: " + error);
                return false;
            }
            if (avatar.GetComponent<VRMHumanoidDescription>() == null)
            {
                error = "Component missing: VRM Humanoid Description";
                Debug.LogError("CheckAvatar error: " + error);
                return false;
            }
            if (avatar.GetComponent<Animator>() == null)
            {
                error = "Component missing: Animator";
                Debug.LogError("CheckAvatar error: " + error);
                return false;
            }
            if (avatar.GetComponent<VRMBlendShapeProxy>() == null)
            {
                error = "Component missing: VRM Blend Shape Proxy";
                Debug.LogError("CheckAvatar error: " + error);
                return false;
            }
            
            HashSet<string> invalidTypes = new HashSet<string>();
            Component[] components = avatar.GetComponentsInChildren<Component>(true);
            foreach (var component in components) {
                if (component == null)
                    return false;
                var componentType = component.GetType();
                if (componentType == null)
                    return false;
                string name = componentType.FullName;
                if (component is UnityEngine.Camera) {
                    Camera cam = component as Camera;
                    var vsfMainCam = cam.gameObject.GetComponent<VSF_MainCamera>();
                    if (cam.targetTexture == null && cam.enabled && vsfMainCam == null) {
                        error = "Enabled cameras must render into a render texture or contain a VSF_MainCamera component.";
                        Debug.LogError("CheckAvatar error: " + error);
                        return false;
                    }
                }
                if (!validTypes.Contains(name))
                    invalidTypes.Add(name);
            }
            
            if (invalidTypes.Count > 0) {
                string invalidTypeList = "";
                foreach (string invalidType in invalidTypes) {
                    invalidTypeList += "\"" + invalidType + "\",\n";
                }

                error = "Detected invalid components on avatar:\n" + invalidTypeList;
                Debug.LogError("CheckAvatar error: " + error);
                return false;
            }

            error = null;
            return true;
        }
    }
}
