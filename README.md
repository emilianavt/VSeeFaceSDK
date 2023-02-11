# VSeeFace SDK

You need Unity 2019.4.31f1. Make a project and import
the UniVRM 0.89, then import this SDK. Also make sure to
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

In other cases, where using a shader with transparency leads to
objects becoming translucent in OBS in an incorrect manner,
setting the alpha blending operation to "Max" often helps. For
example, there is a setting for this in the "Rendering Options",
"Blending" section of the Poiyomi shader. In the case of a custom
shader, setting `BlendOp Add, Max` or similar, with the important
part being the `Max` should help.

When an avatar uses a VideoPlayer component with a video file,
loading the same avatar into multiple slots of the avatar
selection will cause errors, because the internal path of the
video will be the same, which is not supported by Unity. It
should also be noted that for related reasons, selecting another
avatar in the avatar selection and then selecting an avatar that
includes a VideoPlayer and is currently in use will not reload
the file.

Supported additional assets are:
- Dynamic Bones (v1.3.2)
- SPCRJointDynamics
- Magica Cloth (v1.12.10)
- uWindowCapture (v0.6.0)
- Obi Cloth (6.2)
- DokoDemoPainter (needs to be moved outside the "Plugins" folder, no persistence supported)
- Spout4Unity (https://github.com/sloopidoopi/Spout4Unity/tree/5cb448f30b807aa08d98269fef04d59547c201bd)
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

For Spout4Unity's SpoutSender, set the component on a camera with
a render texture set and set the same render texture on the
sender. Make sure the texture's color format is set to
R8G8B8A8_UNORM. The SpoutReceiver will automatically set the
received texture as the main texture on the first material of a
renderer that is on the same object.

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
* `MagicaCloth.MagicaAreaWind`
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
* `RootMotion.FinalIK.AimController`
* `RootMotion.FinalIK.AimIK`
* `RootMotion.FinalIK.AimPoser`
* `RootMotion.FinalIK.Amplifier`
* `RootMotion.FinalIK.ArmIK`
* `RootMotion.FinalIK.BipedIK`
* `RootMotion.FinalIK.BodyTilt`
* `RootMotion.FinalIK.CCDBendGoal`
* `RootMotion.FinalIK.CCDIK`
* `RootMotion.FinalIK.FABRIK`
* `RootMotion.FinalIK.FABRIKRoot`
* `RootMotion.FinalIK.FullBodyBipedIK`
* `RootMotion.FinalIK.GenericPoser`
* `RootMotion.FinalIK.GrounderBipedIK`
* `RootMotion.FinalIK.GrounderFBBIK`
* `RootMotion.FinalIK.GrounderIK`
* `RootMotion.FinalIK.GrounderQuadruped`
* `RootMotion.FinalIK.GrounderVRIK`
* `RootMotion.FinalIK.HandPoser`
* `RootMotion.FinalIK.HitReaction`
* `RootMotion.FinalIK.HitReactionVRIK`
* `RootMotion.FinalIK.IKExecutionOrder`
* `RootMotion.FinalIK.Inertia`
* `RootMotion.FinalIK.InteractionObject`
* `RootMotion.FinalIK.InteractionSystem`
* `RootMotion.FinalIK.InteractionTarget`
* `RootMotion.FinalIK.InteractionTrigger`
* `RootMotion.FinalIK.LegIK`
* `RootMotion.FinalIK.LimbIK`
* `RootMotion.FinalIK.LookAtController`
* `RootMotion.FinalIK.LookAtIK`
* `RootMotion.FinalIK.OffsetPose`
* `RootMotion.FinalIK.PenetrationAvoidance`
* `RootMotion.FinalIK.RagdollUtility`
* `RootMotion.FinalIK.Recoil`
* `RootMotion.FinalIK.RotationLimitAngle`
* `RootMotion.FinalIK.RotationLimitHinge`
* `RootMotion.FinalIK.RotationLimitPolygonal`
* `RootMotion.FinalIK.RotationLimitSpline`
* `RootMotion.FinalIK.ShoulderRotator`
* `RootMotion.FinalIK.TrigonometricIK`
* `RootMotion.FinalIK.TwistRelaxer`
* `RootMotion.FinalIK.VRIK`
* `RootMotion.FinalIK.VRIKLODController`
* `RootMotion.FinalIK.VRIKRootController`
* `SPCRJointDynamicsCollider`
* `SPCRJointDynamicsController`
* `SPCRJointDynamicsPoint`
* `SPCRJointDynamicsPointGrabber`
* `Spout.InvertCamera`
* `Spout.SpoutReceiver`
* `Spout.SpoutSender`
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
* `UnityEngine.ConstantForce`
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
* `VSeeFace.VSF_SetShaderParamFromTransform`
* `VSeeFace.VSF_SetTransform`
* `VSeeFace.VSF_SpoutReceiverSettings`
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

## Integrating VSFAvatar support in other applications

Integrating VSFAvatar support is not really supported and the format might change at any time, but if you'd still like to try supporting it, here are some notes. The first thing you'll want to do is include the VSF SDK in your application, so references to the `VSF_*` components can be resolved. Also make sure to include all the assets listed under "Supported additional assets" in the correct versions to ensure compatibility with existing VSFAvatar files. Verifying the whitelist using the `AvatarCheck` component when loading a model is recommended. DokoDemoPainter should be modified to remove persistence support to prevent VSFAvatar files from being able to write to arbitrary paths. Magica Cloth and Spout4Unity require their corresponding manager components to be added to the scene in order to be supported correctly.

VSFAvatar files are Unity asset bundle files. You can take a look at the `BasicExporter` and `VSFAvatarInspector` scripts to see how the bundle is laid out. Using that information you should be able to load the file.

Certain features and application will need additional support from your applications. Animations on the avatar are registered to VRM blend shape clips on the `VSF_Animations` component. These animations should be played with a weight corresponding to the VRM blend shape clip being set on the VRM blend shape proxy. Since VRM blend shape clip weights are transmitted over VMC protocol, implementing this will automatically make animations work correctly when receiving tracking data over VMC protocol. If an animator controller is set on a VSFAvatar, it was probably left there by accident and should be cleared. An application supporting VSFAvatar format has to implement its own animation handling. The preview function of the `VSF_Animations` component can serve as a simplified example for this, although it only supports playing back a single animation at full strength, rather than multiple blended animations.

Cameras with the `VSF_MainCamera` component should become active instead of the regular scene camera when being activated. If multiple cameras with this component become active, they should be added and removed to a stack of cameras in the order in which they become active, with the latest activated camera on the stack being the current camera.

Settings set on `VSF_Configuration` component should be handled as far as they are applicable by an application supporting VSFAvatar format.

To support the `VSF_SetEffect*` components, the Unity Post Processing Stack v2 should be active and on every frame, settings from active `VSF_SetEffect*` components should be applied to the corresponding effects. When `VSF_SetEffect*` components become inactive, the effect settings should be reverted to their defaults.

When a VSFAvatar file contains a `VideoPlayer` component, the asset bundle must not be unloaded while the avatar is active, otherwise the video will fail to play.
