//#if GAIA_PRESENT

using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gaia.GX.Randomchaos_Ltd
{
    public class DistingoGaiaInstalled : MonoBehaviour
    {

        #region Generic informational methods

        /// <summary>
        /// Returns the publisher name if provided. 
        /// This will override the publisher name in the namespace ie Gaia.GX.PublisherName
        /// </summary>
        /// <returns>Publisher name</returns>
        public static string GetPublisherName()
        {
            return "Randomchaos Ltd";
        }

        /// <summary>
        /// Returns the package name if provided
        /// This will override the package name in the class name ie public class PackageName.
        /// </summary>
        /// <returns>Package name</returns>
        public static string GetPackageName()
        {
            return "Distingo - Terrain in Detail";
        }

        public static string GetPackageImage()
        {
            return "DistingoGaiaImage";
        }

        #endregion

#if UNITY_EDITOR

        public static void GX_About()
        {
            EditorUtility.DisplayDialog("About Distingo - Terrain in Detail", @"Distingo – Bringing ever increasing detail to your teerrain.

Alter terrain splatting distance.
Regulate texture tialing based on distance from the camera.
Alter individual textures:-
    Near and Far UV Multipliers
    Normal map power
    Smoothness
    Metallic
", "OK");
        }

#endif

        public static void GX_AddToSelectedTerrain()
        {
            if (Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0)
            {
#if UNITY_EDITOR
                EditorUtility.DisplayDialog("No Terrain Found", "You have no terrain in your scene, please add a terrain and try again.", "OK");
#endif
                return;
            }

            foreach (Terrain t in Terrain.activeTerrains)
            {
                if (t.gameObject.GetComponent<RandomchaosLtd.Distingo.DistingoTerrainScript>() == null)
                    t.gameObject.AddComponent<RandomchaosLtd.Distingo.DistingoTerrainScript>();
            }
        }
    }
}
//#endif