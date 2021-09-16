# VSeeFace SDK

You need Unity 2019.4.29f1. Make a project and import
the UniVRM (0.66 is known to work well, older versions may cause issues with MToon transparency), then import this SDK. Also make sure to
set the project to [linear color space](https://docs.unity3d.com/Manual/LinearRendering-LinearOrGammaWorkflow.html).

As an introduction, please watch [this tutorial video](https://youtu.be/jhQ8DF87I5I) by @[Virtual_Deat](twitter.com/Virtual_Deat).

You can watch how the two included sample models were made [here](https://www.youtube.com/watch?v=T4LxyxfUAVY).

# Usage

First create a VRM as usual, then import it. You can now change
the materials and use various Unity components on it.

Once ready, select your avatar root in the hierarchy and
export it from the VSF SDK menu.

If you prefer manually setting up twist relaxers for the arms,
you can put a VSF_Configuration component on your avatar and
disable the automatic twist relaxers VSeeFace usually adds. It
is also possible to override the avatar height calculation and
ground offset from there. If you are not sure what any of these
things are, you don't need to add a VSF_Configuration component.

To add custom animations, add a VSF_Animations component to the
avatar root. Please note that an animation which animates any
single humanoid bone also needs to animate all other humanoid
bones, otherwise the model will be put into a curled up default
pose.

It is possible to disable the IK for animations by ticking the
corresponding checkbox underneath the animation entry. This will
prevent the head and hands from following the tracking, allowing
all bones of the avatar to be controlled by the animation.

Animations are faded in and out according to the expression's
transition time. Due to this, an animation that just sets the
color of an object to red and is three frames long without
looping would still fade the color to red when activated and
fade it back when deactivated, as the animation's value would
remain at that of its last frame, while the weight of the
animation changes. If the fading should be controlled by the
animation, it is recommended to set the transition time to 0
instead.

It is possible to add custom main cameras to an avatar as long
as they are disabled by default and only enabled through
animations or similar mechanisms. In this case, a VSF_MainCamera
component should be placed on the camera, which will take care
of disabling other active cameras that do not target render
textures and will apply the transparency effect to the camera.
"Really nice" antialiasing is not supported for custom cameras.
Only the latest activated camera will render. VSF_MainCamera
cameras should be activated and deactivated by enabling or
disabling their game object, not the camera component itself,
which is managed by a camera manager.

Cameras rendering into render textures should not have a
VSF_MainCamera component and can be enabled at any time.

Objects with the VSF_Static component will reset their absolute
position and rotation to their initial ones after IK and VMC
protocol reception. In effect, they basically remain static. If
you would like to set up a scene inside your model, add an empty
game object, put VSF_Static on it and build your scene inside.
It will basically remain stationary and not move along with your
model's root object.

Objects with the VSF_Static component should not be placed
within the armature.

Starting with VSeeFace v1.13.36b, the VSF_IKFollower
component should no longer be necessary for the correct operation
of constraints and particle systems.

The standard Particles/Standard Unlit shader can cause issues
with transparency, so it is recommended to use the included
"VSeeFace/Particles/Standard Unlit" shader or the standard
"Particles/Standard Surface" shaders instead. Other shaders that
were not designed with transparent backgrounds in mind might
also cause issues. It should also be noted that, due to how
alpha blending works, particles using additive or other
blending modes other than regular alpha blending will look
different over transparent areas than they do while in front of
the model.

When an avatar uses a VideoPlayer component with a video file,
loading the same avatar into multiple slots of the avatar
selection will cause errors, because the internal path of the
video will be the same, which is not supported by Unity. It
should also be noted that for related reasons, selecting another
avatar in the avatar selection and then selecting an avatar that
includes a VideoPlayer and is currently in use will not reload
the file.

Supported additional assets are:
- Dynamic Bones
- SPCRJointDynamics
- Magica Cloth (v1.10.2)
- uWindowCapture (v0.6.0)
- Obi Cloth (6.2)
- Any regular shaders (not URP, HDRP, LWRP)
- Most things that seemed safe and useful from Unity itself

For a full list of usable components, please refer to the next section.

On systems with multiple GPUs, commonly laptops, the window
capture functionality of uWindowCapture seems to have issues,
but the desktop capture function seems likely to work.

When using Magica Cloth, before exporting, manually create a
prefab by dragging your model from the hierarchy into the files
part of Unity and when asked, select original prefab. Otherwise
the cloth may not work correctly. Don't add a MagicaPhysicsManager
to your avatar, there's already one set up in VSeeFace.

# Whitelisted components

* `DDPPenController`
* `DDPStampController`
* `DokoDemoPainterPaintable` (No persistence or texture loading or saving is supported)
* `DokoDemoPainterPen`
* `DokoDemoPainterStamp`
* `DynamicBone`
* `DynamicBoneCollider`
* `DynamicBoneColliderBase`
* `DynamicBonePlaneCollider`
* `MagicaCloth.MagicaBoneCloth`
* `MagicaCloth.MagicaBoneSpring`
* `MagicaCloth.MagicaCapsuleCollider`
* `MagicaCloth.MagicaDirectionalWind`
* `MagicaCloth.MagicaMeshCloth`
* `MagicaCloth.MagicaMeshSpring`
* `MagicaCloth.MagicaPlaneCollider`
* `MagicaCloth.MagicaRenderDeformer`
* `MagicaCloth.MagicaSphereCollider`
* `MagicaCloth.MagicaVirtualDeformer`
* `Obi.ObiAmbientForceZone`
* `Obi.ObiCharacter`
* `Obi.ObiCloth`
* `Obi.ObiClothProxy`
* `Obi.ObiClothRenderer`
* `Obi.ObiCollider`
* `Obi.ObiCollider2D`
* `Obi.ObiContactEventDispatcher`
* `Obi.ObiDistanceFieldRenderer`
* `Obi.ObiFixedUpdater`
* `Obi.ObiInstancedParticleRenderer`
* `Obi.ObiLateFixedUpdater`
* `Obi.ObiLateUpdater`
* `Obi.ObiParticleAttachment`
* `Obi.ObiParticleDragger`
* `Obi.ObiParticleGridDebugger`
* `Obi.ObiParticlePicker`
* `Obi.ObiParticleRenderer`
* `Obi.ObiProfiler`
* `Obi.ObiRigidbody`
* `Obi.ObiRigidbody2D`
* `Obi.ObiSkinnedCloth`
* `Obi.ObiSkinnedClothRenderer`
* `Obi.ObiSolver`
* `Obi.ObiSphericalForceZone`
* `Obi.ObiStitcher`
* `Obi.ObiTearableCloth`
* `Obi.ObiTearableClothRenderer`
* `ObiActorTeleport`
* `ObiContactGrabber`
* `ObiParticleCounter`
* `RootMotion.FinalIK.RotationLimitAngle`
* `RootMotion.FinalIK.RotationLimitHinge`
* `RootMotion.FinalIK.RotationLimitPolygonal`
* `RootMotion.FinalIK.RotationLimitSpline`
* `RootMotion.FinalIK.TwistRelaxer`
* `SPCRJointDynamicsCollider`
* `SPCRJointDynamicsController`
* `SPCRJointDynamicsPoint`
* `SPCRJointDynamicsPointGrabber`
* `TMPro.TMP_Dropdown`
* `TMPro.TMP_InputField`
* `TMPro.TextMeshPro`
* `TMPro.TextMeshProUGUI`
* `UnityEngine.Animations.AimConstraint`
* `UnityEngine.Animations.LookAtConstraint`
* `UnityEngine.Animations.ParentConstraint`
* `UnityEngine.Animations.PositionConstraint`
* `UnityEngine.Animations.RotationConstraint`
* `UnityEngine.Animations.ScaleConstraint`
* `UnityEngine.Animator`
* `UnityEngine.AudioReverbZone`
* `UnityEngine.AudioSource`
* `UnityEngine.BillboardRenderer`
* `UnityEngine.BoxCollider`
* `UnityEngine.BoxCollider2D`
* `UnityEngine.Camera`
* `UnityEngine.Canvas`
* `UnityEngine.CanvasRenderer`
* `UnityEngine.CapsuleCollider`
* `UnityEngine.CharacterJoint`
* `UnityEngine.Cloth`
* `UnityEngine.ConfigurableJoint`
* `UnityEngine.DistanceJoint2D`
* `UnityEngine.EllipsoidParticleEmitter`
* `UnityEngine.FixedJoint`
* `UnityEngine.FixedJoint2D`
* `UnityEngine.FlareLayer`
* `UnityEngine.FrictionJoint2D`
* `UnityEngine.GUILayer`
* `UnityEngine.HingeJoint`
* `UnityEngine.HingeJoint2D`
* `UnityEngine.Joint`
* `UnityEngine.Light`
* `UnityEngine.LightProbeGroup`
* `UnityEngine.LightProbeProxyVolume`
* `UnityEngine.LineRenderer`
* `UnityEngine.MeshCollider`
* `UnityEngine.MeshFilter`
* `UnityEngine.MeshParticleEmitter`
* `UnityEngine.MeshRenderer`
* `UnityEngine.ParticleAnimator`
* `UnityEngine.ParticleSystem`
* `UnityEngine.ParticleSystemForceField`
* `UnityEngine.ParticleSystemRenderer`
* `UnityEngine.RectTransform`
* `UnityEngine.ReflectionProbe`
* `UnityEngine.RelativeJoint2D`
* `UnityEngine.Rigidbody`
* `UnityEngine.Rigidbody2D`
* `UnityEngine.SkinnedMeshRenderer`
* `UnityEngine.SphereCollider`
* `UnityEngine.SpringJoint`
* `UnityEngine.SpringJoint2D`
* `UnityEngine.SpriteMask`
* `UnityEngine.SpriteRenderer`
* `UnityEngine.TargetJoint2D`
* `UnityEngine.TextMesh`
* `UnityEngine.TrailRenderer`
* `UnityEngine.Transform`
* `UnityEngine.WheelJoint2D`
* `UnityEngine.UI.Button`
* `UnityEngine.UI.CanvasScaler`
* `UnityEngine.UI.Dropdown`
* `UnityEngine.UI.GraphicRaycaster`
* `UnityEngine.UI.Image`
* `UnityEngine.UI.InputField`
* `UnityEngine.UI.Mask`
* `UnityEngine.UI.RawImage`
* `UnityEngine.UI.RectMask2D`
* `UnityEngine.UI.ScrollRect`
* `UnityEngine.UI.Scrollbar`
* `UnityEngine.UI.Slider`
* `UnityEngine.UI.Text`
* `UnityEngine.UI.Toggle`
* `UnityEngine.Video.VideoPlayer`
* `UnityEngine.WindZone`
* `VRM.VRMBlendShapeProxy`
* `VRM.VRMFirstPerson`
* `VRM.VRMHumanoidDescription`
* `VRM.VRMLookAtBlendShapeApplyer`
* `VRM.VRMLookAtBoneApplyer`
* `VRM.VRMLookAtHead`
* `VRM.VRMMeta`
* `VRM.VRMSpringBone`
* `VRM.VRMSpringBoneColliderGroup`
* `VSeeFace.VSF_Animations`
* `VSeeFace.VSF_Configuration`
* `VSeeFace.VSF_IKFollower`
* `VSeeFace.VSF_MainCamera`
* `VSeeFace.VSF_SetAnimatorBool`
* `VSeeFace.VSF_SetAnimatorFloat`
* `VSeeFace.VSF_SetAnimatorInt`
* `VSeeFace.VSF_SetBlendShapeClip`
* `VSeeFace.VSF_SetEffectAmbientOcclusion`
* `VSeeFace.VSF_SetEffectBloom`
* `VSeeFace.VSF_SetEffectChromaticAberration`
* `VSeeFace.VSF_SetEffectColorGrading`
* `VSeeFace.VSF_SetEffectDepthOfField`
* `VSeeFace.VSF_SetEffectGrain`
* `VSeeFace.VSF_SetEffectHalftone`
* `VSeeFace.VSF_SetEffectLensDistortion`
* `VSeeFace.VSF_SetTransform`
* `VSeeFace.VSF_Static`
* `VSeeFace.VSF_Toggle`
* `VSeeFace.VSF_Trigger`
* `uWindowCapture.UwcAltTabWindowTextureManager`
* `uWindowCapture.UwcCursorTexture`
* `uWindowCapture.UwcIconTexture`
* `uWindowCapture.UwcManager`
* `uWindowCapture.UwcWindowTexture`
* `uWindowCapture.UwcWindowTextureChildrenManager`
* `uWindowCapture.UwcWindowTextureManager`
