// Adapted from TWSDK
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRM;

namespace VSeeFace
{
    public class ExportAvatar : MonoBehaviour
    {
        [MenuItem("VSF SDK/Export avatar bundle")]
        public static void ExportAvatarBundle()
        {
            GameObject obj = Selection.activeGameObject;
            string error;
            if (!AvatarCheck.CheckAvatar(obj, out error)) {
                EditorUtility.DisplayDialog("Export Avatar Bundle", error, "OK");
                return;
            }
            
            string fullpath = EditorUtility.SaveFilePanel("Export Avatar Bundle", ".", obj.name, "vsfavatar");
            if (fullpath == null || fullpath == "")
                return;

            string filename = Path.GetFileName(fullpath);

            bool complete = false;
            string prefabPath = "Assets/VSFAvatarTemporary.prefab";
            try {
                AssetDatabase.DeleteAsset(prefabPath);
                if (File.Exists(prefabPath))
                    File.Delete(prefabPath);

                bool succeededPack = false;
                PrefabUtility.SaveAsPrefabAsset(obj, prefabPath, out succeededPack);
                if (!succeededPack) {
                    Debug.Log("Prefab creation failed");
                    return;
                }

                AssetBundleBuild bundleBuild = new AssetBundleBuild();
                AssetDatabase.RemoveUnusedAssetBundleNames();
                bundleBuild.assetBundleName = filename;
                bundleBuild.assetNames = new string[] { prefabPath };
                bundleBuild.addressableNames = new string[] { "VSFAvatar" };

                BuildAssetBundleOptions options = BuildAssetBundleOptions.ForceRebuildAssetBundle | BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.StrictMode;
                if (obj.GetComponentsInChildren<UnityEngine.Video.VideoPlayer>(true).Length > 0) {
                    Debug.Log("VideoPlayer detected, using uncompressed asset bundle.");
                    options = options | BuildAssetBundleOptions.UncompressedAssetBundle;
                }
                BuildPipeline.BuildAssetBundles(Application.temporaryCachePath, new AssetBundleBuild[] { bundleBuild }, options, BuildTarget.StandaloneWindows);
                if (File.Exists(fullpath))
                    File.Delete(fullpath);
                File.Move(Application.temporaryCachePath + "/" + filename, fullpath);

                EditorUtility.DisplayDialog("Export", "Export complete!", "OK");
                complete = true;
            }
            finally
            {
                try {
                    AssetDatabase.DeleteAsset(prefabPath);
                    if (File.Exists(prefabPath))
                        File.Delete(prefabPath);
                } catch {}

                if (!complete)
                    EditorUtility.DisplayDialog("Export", "Export failed! See the console for details.", "OK");
            }
        }
    }
}