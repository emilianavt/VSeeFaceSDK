using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    // This component can be used to set shader parameters based on a transform's position. You can either get the full transformation matrix itself, or a vector for position, rotation, or scale.
    public class VSF_SetShaderParamFromTransform : MonoBehaviour
    {
        [Tooltip("This is the transform from which the parameters are extracted.")]
        public Transform referenceTransform;

        [Tooltip("This is the material for which the shader parameters are set.")]
        public Material targetMaterial;

        [Tooltip("This is the name of the parameter to set in the shader. Type is dependant on the mode.")]
        public string shaderParameterName = "ShaderParameterName";

        [Tooltip("This defines how the parameters are extracted. WorldToLocalMatrix and LocalToWorldMatrix are transformation matrices (float4x4) while the others are a vector type (float4).")]
        public VSF_SetShaderParamFromTransform_Mode mode;
        
        public void SetReferenceTransform(Transform v) {
            referenceTransform = v;
        }
        public void SetTargetMaterial(Material v) {
            targetMaterial = v;
        }
        public void SetShaderParameterName(string v) {
            shaderParameterName = v;
        }
        public void SetMode(VSF_SetShaderParamFromTransform_Mode v) {
            mode = v;
        }


        public void Update() {
            if(targetMaterial == null) {
                Renderer r = GetComponent<Renderer>();
                if(r == null)
                    return;
                targetMaterial = r.sharedMaterial;
            }

            if(referenceTransform == null)
                referenceTransform = transform;

            Vector3 v;
            switch(mode) {
                case VSF_SetShaderParamFromTransform_Mode.WorldToLocalMatrix:
                    targetMaterial.SetMatrix(shaderParameterName, referenceTransform.worldToLocalMatrix);
                    break;

                case VSF_SetShaderParamFromTransform_Mode.LocalToWorldMatrix:
                    targetMaterial.SetMatrix(shaderParameterName, referenceTransform.localToWorldMatrix);
                    break;

                case VSF_SetShaderParamFromTransform_Mode.WorldPositionVector:
                    v = referenceTransform.position;
                    targetMaterial.SetVector(shaderParameterName, new Vector4(v.x, v.y, v.z, 1.0f));
                    break;

                case VSF_SetShaderParamFromTransform_Mode.WorldEulerAnglesVector:
                    v = referenceTransform.eulerAngles;
                    targetMaterial.SetVector(shaderParameterName, new Vector4(v.x, v.y, v.z, 0.0f));
                    break;

                case VSF_SetShaderParamFromTransform_Mode.WorldLossyScaleVector:
                    v = referenceTransform.lossyScale;
                    targetMaterial.SetVector(shaderParameterName, new Vector4(v.x, v.y, v.z, 0.0f));
                    break;

                case VSF_SetShaderParamFromTransform_Mode.LocalPositionVector:
                    v = referenceTransform.localPosition;
                    targetMaterial.SetVector(shaderParameterName, new Vector4(v.x, v.y, v.z, 0.0f));
                    break;

                case VSF_SetShaderParamFromTransform_Mode.LocalEulerAnglesVector:
                    v = referenceTransform.localEulerAngles;
                    targetMaterial.SetVector(shaderParameterName, new Vector4(v.x, v.y, v.z, 0.0f));
                    break;

                case VSF_SetShaderParamFromTransform_Mode.LocalScaleVector:
                    v = referenceTransform.localScale;
                    targetMaterial.SetVector(shaderParameterName, new Vector4(v.x, v.y, v.z, 0.0f));
                    break;
            }
        }

        public enum VSF_SetShaderParamFromTransform_Mode
        {
            WorldToLocalMatrix, LocalToWorldMatrix,
            WorldPositionVector, WorldEulerAnglesVector, WorldLossyScaleVector,
            LocalPositionVector, LocalEulerAnglesVector, LocalScaleVector
        }
    }
}

// 2021-10-28 By Panthavma, under the MIT Licence, originally for the GenerateFaceNormals project.
