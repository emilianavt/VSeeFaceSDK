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
            "MagicaCloth.MagicaAreaWind",
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
            "Obi.ObiAmbientForceZone",
            "Obi.ObiCharacter",
            "Obi.ObiCloth",
            "Obi.ObiClothProxy",
            "Obi.ObiClothRenderer",
            "Obi.ObiCollider",
            "Obi.ObiCollider2D",
            "Obi.ObiContactEventDispatcher",
            "Obi.ObiDistanceFieldRenderer",
            "Obi.ObiFixedUpdater",
            "Obi.ObiInstancedParticleRenderer",
            "Obi.ObiLateFixedUpdater",
            "Obi.ObiLateUpdater",
            "Obi.ObiParticleAttachment",
            "Obi.ObiParticleDragger",
            "Obi.ObiParticleGridDebugger",
            "Obi.ObiParticlePicker",
            "Obi.ObiParticleRenderer",
            "Obi.ObiProfiler",
            "Obi.ObiRigidbody",
            "Obi.ObiRigidbody2D",
            "Obi.ObiSkinnedCloth",
            "Obi.ObiSkinnedClothRenderer",
            "Obi.ObiSolver",
            "Obi.ObiSphericalForceZone",
            "Obi.ObiStitcher",
            "Obi.ObiTearableCloth",
            "Obi.ObiTearableClothRenderer",
            "ObiActorTeleport",
            "ObiContactGrabber",
            "ObiParticleCounter",
            "RootMotion.FinalIK.AimController",
            "RootMotion.FinalIK.AimIK",
            "RootMotion.FinalIK.AimPoser",
            "RootMotion.FinalIK.Amplifier",
            "RootMotion.FinalIK.ArmIK",
            "RootMotion.FinalIK.BipedIK",
            "RootMotion.FinalIK.BodyTilt",
            "RootMotion.FinalIK.CCDBendGoal",
            "RootMotion.FinalIK.CCDIK",
            "RootMotion.FinalIK.FABRIK",
            "RootMotion.FinalIK.FABRIKRoot",
            "RootMotion.FinalIK.FullBodyBipedIK",
            "RootMotion.FinalIK.GenericPoser",
            "RootMotion.FinalIK.GrounderBipedIK",
            "RootMotion.FinalIK.GrounderFBBIK",
            "RootMotion.FinalIK.GrounderIK",
            "RootMotion.FinalIK.GrounderQuadruped",
            "RootMotion.FinalIK.GrounderVRIK",
            "RootMotion.FinalIK.HandPoser",
            "RootMotion.FinalIK.HitReaction",
            "RootMotion.FinalIK.HitReactionVRIK",
            "RootMotion.FinalIK.IKExecutionOrder",
            "RootMotion.FinalIK.Inertia",
            "RootMotion.FinalIK.InteractionObject",
            "RootMotion.FinalIK.InteractionSystem",
            "RootMotion.FinalIK.InteractionTarget",
            "RootMotion.FinalIK.InteractionTrigger",
            "RootMotion.FinalIK.LegIK",
            "RootMotion.FinalIK.LimbIK",
            "RootMotion.FinalIK.LookAtController",
            "RootMotion.FinalIK.LookAtIK",
            "RootMotion.FinalIK.OffsetPose",
            "RootMotion.FinalIK.PenetrationAvoidance",
            "RootMotion.FinalIK.RagdollUtility",
            "RootMotion.FinalIK.Recoil",
            "RootMotion.FinalIK.RotationLimitAngle",
            "RootMotion.FinalIK.RotationLimitHinge",
            "RootMotion.FinalIK.RotationLimitPolygonal",
            "RootMotion.FinalIK.RotationLimitSpline",
            "RootMotion.FinalIK.ShoulderRotator",
            "RootMotion.FinalIK.TrigonometricIK",
            "RootMotion.FinalIK.TwistRelaxer",
            "RootMotion.FinalIK.VRIK",
            "RootMotion.FinalIK.VRIKLODController",
            "RootMotion.FinalIK.VRIKRootController",
            "SPCRJointDynamicsCollider",
            "SPCRJointDynamicsController",
            "SPCRJointDynamicsPoint",
            "SPCRJointDynamicsPointGrabber",
            "Spout.InvertCamera",
            "Spout.SpoutReceiver",
            "Spout.SpoutSender",
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
            "UnityEngine.BoxCollider2D",
            "UnityEngine.Camera",
            "UnityEngine.Canvas",
            "UnityEngine.CanvasRenderer",
            "UnityEngine.CapsuleCollider",
            "UnityEngine.CharacterJoint",
            "UnityEngine.Cloth",
            "UnityEngine.ConfigurableJoint",
            "UnityEngine.ConstantForce",
            "UnityEngine.DistanceJoint2D",
            "UnityEngine.EllipsoidParticleEmitter",
            "UnityEngine.FixedJoint",
            "UnityEngine.FixedJoint2D",
            "UnityEngine.FlareLayer",
            "UnityEngine.FrictionJoint2D",
            "UnityEngine.GUILayer",
            "UnityEngine.HingeJoint",
            "UnityEngine.HingeJoint2D",
            "UnityEngine.Joint",
            "UnityEngine.Light",
            "UnityEngine.LightProbeGroup",
            "UnityEngine.LightProbeProxyVolume",
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
            "UnityEngine.ReflectionProbe",
            "UnityEngine.RelativeJoint2D",
            "UnityEngine.Rigidbody",
            "UnityEngine.Rigidbody2D",
            "UnityEngine.SkinnedMeshRenderer",
            "UnityEngine.SphereCollider",
            "UnityEngine.SpringJoint",
            "UnityEngine.SpringJoint2D",
            "UnityEngine.SpriteMask",
            "UnityEngine.SpriteRenderer",
            "UnityEngine.TargetJoint2D",
            "UnityEngine.TextMesh",
            "UnityEngine.TrailRenderer",
            "UnityEngine.Transform",
            "UnityEngine.WheelJoint2D",
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
            "VSeeFace.VSF_SetAnimatorBool",
            "VSeeFace.VSF_SetAnimatorFloat",
            "VSeeFace.VSF_SetAnimatorInt",
            "VSeeFace.VSF_SetBlendShapeClip",
            "VSeeFace.VSF_SetEffectAmbientOcclusion",
            "VSeeFace.VSF_SetEffectBloom",
            "VSeeFace.VSF_SetEffectChromaticAberration",
            "VSeeFace.VSF_SetEffectColorGrading",
            "VSeeFace.VSF_SetEffectDepthOfField",
            "VSeeFace.VSF_SetEffectGrain",
            "VSeeFace.VSF_SetEffectHalftone",
            "VSeeFace.VSF_SetEffectLensDistortion",
            "VSeeFace.VSF_SetShaderParamFromTransform",
            "VSeeFace.VSF_SetTransform",
            "VSeeFace.VSF_SpoutReceiverSettings",
            "VSeeFace.VSF_Static",
            "VSeeFace.VSF_Toggle",
            "VSeeFace.VSF_Trigger",
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
