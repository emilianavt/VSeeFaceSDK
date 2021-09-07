using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using VSeeFace;

// This component can be used to inspect the way an exported vsfavatar model looks to debug blendshape issues etc.
// Supports playing an animation as long as only one blend shape clip value is set to 1.
namespace VSeeFace {
    public class VSFAvatarInspector : MonoBehaviour
    {
        [Header("Play scene to inspect exported model")]
        public string vsfAvatarFilename = "c:\\path\\to\\yourmodel.vsfavatar";
        [Header("Will be filled automatically after loading")]
        public GameObject avatar;

        void Start() {
            var assetBundle = AssetBundle.LoadFromFile(vsfAvatarFilename);
            var prefab = assetBundle.LoadAsset<GameObject>("VSFAvatar");
            string error;
            if (!VSeeFace.AvatarCheck.CheckAvatar(prefab, out error)) {
                assetBundle.Unload(true);
                assetBundle = null;
                throw new InvalidDataException(error);
            }
            avatar = Instantiate(prefab);

        }
    }
}