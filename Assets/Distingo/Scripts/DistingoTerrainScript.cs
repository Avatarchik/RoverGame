using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RandomchaosLtd.Distingo
{
    [ExecuteInEditMode]
    [AddComponentMenu("Scripts/Randomchaos Ltd/Distingo/DistingoTerrainScript")]
    public class DistingoTerrainScript : MonoBehaviour
    {
        public List<string> ShaderList = new List<string>();
        public string SelectedShader;
        public string lastSelectedShader;

        public static string Version = "1.2";

        public float TriPlanerUVMultiplier = .1f;

        public bool IsMeshTerrain = true;

        #region Unity Terrain Prameters
        public float SplattingDistance = 6000;

        public const float maxUV = 10;
        public const float minUV = .1f;

        public const float maxN = 3;
        public const float minN = .1f;

        [Range(minUV, maxUV)]
        public float BlendChannel0Max = 1f;
        [Range(minUV, maxUV)]
        public float BlendChannel1Max = 1f;
        [Range(minUV, maxUV)]
        public float BlendChannel2Max = 1f;
        [Range(minUV, maxUV)]
        public float BlendChannel3Max = 1f;

        [Range(minUV, maxUV)]
        public float BlendChannel0Min = 1f;
        [Range(minUV, maxUV)]
        public float BlendChannel1Min = 1f;
        [Range(minUV, maxUV)]
        public float BlendChannel2Min = 1f;
        [Range(minUV, maxUV)]
        public float BlendChannel3Min = 1f;

        [Range(minN, maxN)]
        public float BlendNChannel0 = 1f;
        [Range(minN, maxN)]
        public float BlendNChannel1 = 1f;
        [Range(minN, maxN)]
        public float BlendNChannel2 = 1f;
        [Range(minN, maxN)]
        public float BlendNChannel3 = 1f;

        [Range(0, 1)]
        public float Channel0Smoothness = 0;
        [Range(0, 1)]
        public float Channel1Smoothness = 0;
        [Range(0, 1)]
        public float Channel2Smoothness = 0;
        [Range(0, 1)]
        public float Channel3Smoothness = 0;

        [Range(0, 1)]
        public float Channel0Metalic = 0;
        [Range(0, 1)]
        public float Channel1Metalic = 0;
        [Range(0, 1)]
        public float Channel2Metalic = 0;
        [Range(0, 1)]
        public float Channel3Metalic = 0;

        [Range(1, 10)]
        public float BrightnessMod = 1f;

        public bool UseNormalShader = false;

        [Range(0, 1)]
        public float MinMaxUVCutoff = 1f;

        public bool ShowFallOff = false;

        public Texture2D[] OcclusionMaps = new Texture2D[12];

        public float OcclusionPower0 = 1;
        public float OcclusionPower1 = 1;
        public float OcclusionPower2 = 1;
        public float OcclusionPower3 = 1;

        public float Brightness0 = 1;
        public float Brightness1 = 1;
        public float Brightness2 = 1;
        public float Brightness3 = 1;

        public Texture2D GlobalBlendTexture;
        public Texture2D GlobalOcclusionTexture;

        public bool HasGlobalBlend;
        public float BlendPower = 1;
        public float OcclusionPower = 1;
        bool WasBlend;
        bool WasStandard; 

        Material OcclusionMat;
        Material BlendMat;

        #endregion

        #region Terrain Mesh Parameters

        public Texture2D tm_SplatMap;

        public Texture2D[] tm_Splat = new Texture2D[4];
        public Texture2D[] tm_Normal = new Texture2D[4];
        public Vector2[] tmp_UVs = new Vector2[4] { new Vector2(5, 5), new Vector2(5, 5), new Vector2(5, 5), new Vector2(5, 5) };

        public bool UseSpeculerPBR;

        Material MeshTerrainSplatMat;
        #endregion

        #region VTP Parameters

        public bool useTriPlanar = false;
        public bool UseHeightBasedBlending = false;
        public bool UseParallaxMapping = false;
        public int ParallaxInterpolations = 8;

        [Range(-0.15f, 0.15f)]
        public float ParallaxStrength0 = 0;
        [Range(-0.15f, 0.15f)]
        public float ParallaxStrength1 = 0;
        [Range(-0.15f, 0.15f)]
        public float ParallaxStrength2 = 0;
        [Range(-0.15f, 0.15f)]
        public float ParallaxStrength3 = 0;

        public bool IsBaseLayer0 = false;
        public bool IsBaseLayer1 = false;
        public bool IsBaseLayer2 = false;
        public bool IsBaseLayer3 = false;

        public float BaseHeight0 = 0;
        public float BaseHeight1 = 0;
        public float BaseHeight2 = 0;
        public float BaseHeight3 = 0;

        public float HeightBasedTransparency0 = 0;
        public float HeightBasedTransparency1 = 0;
        public float HeightBasedTransparency2 = 0;
        public float HeightBasedTransparency3 = 0;

        public float HeightMapMultiplier0 = 0;
        public float HeightMapMultiplier1 = 0;
        public float HeightMapMultiplier2 = 0;
        public float HeightMapMultiplier3 = 0;

        public float BlendSmooth0 = 0;
        public float BlendSmooth1 = 0;
        public float BlendSmooth2 = 0;
        public float BlendSmooth3 = 0;

        [Range(0, 1)]
        public float SmoothnessNear0 = 0;
        [Range(0, 1)]
        public float SmoothnessNear1 = 0;
        [Range(0, 1)]
        public float SmoothnessNear2 = 0;
        [Range(0, 1)]
        public float SmoothnessNear3 = 0;

        [Range(0, 1)]
        public float SmoothnessFar0 = 0;
        [Range(0, 1)]
        public float SmoothnessFar1 = 0;
        [Range(0, 1)]
        public float SmoothnessFar2 = 0;
        [Range(0, 1)]
        public float SmoothnessFar3 = 0;

        [Range(0, 1)]
        public float Metallic0 = 0;
        [Range(0, 1)]
        public float Metallic1 = 0;
        [Range(0, 1)]
        public float Metallic2 = 0;
        [Range(0, 1)]
        public float Metallic3 = 0;


        [Range(0, 1)]
        public float Occlusion0 = 1;
        [Range(0, 1)]
        public float Occlusion1 = 1;
        [Range(0, 1)]
        public float Occlusion2 = 1;
        [Range(0, 1)]
        public float Occlusion3 = 1;

        [Range(0, 1)]
        public float OccludeAlbedo0 = 0.25f;
        [Range(0, 1)]
        public float OccludeAlbedo1 = 0.25f;
        [Range(0, 1)]
        public float OccludeAlbedo2 = 0.25f;
        [Range(0, 1)]
        public float OccludeAlbedo3 = 0.25f;

        #endregion

        public bool Mobile = false;
        bool WasMobile;

        public bool VTPPresent = false;

        Dictionary<string, Material> Materials = new Dictionary<string, Material>();
        
        void Start()
        {
            SetUpMaterials();

            ApplyShaderVariables();
        }

        // Method kindly donated by Adam from Gaia :)
        public List<Type> GetTypesInNamespace(string nameSpace)
        {
            List<Type> matchingTypes = new List<Type>();

            int assyIdx, typeIdx;
            System.Type[] types;
            System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            for (assyIdx = 0; assyIdx < assemblies.Length; assyIdx++)
            {
                if (assemblies[assyIdx].FullName.StartsWith("Assembly"))
                {
                    types = assemblies[assyIdx].GetTypes();
                    for (typeIdx = 0; typeIdx < types.Length; typeIdx++)
                    {
                        if (!string.IsNullOrEmpty(types[typeIdx].Namespace))
                        {
                            if (types[typeIdx].Namespace.StartsWith(nameSpace))
                            {
                                matchingTypes.Add(types[typeIdx]);
                            }
                        }
                    }
                }
            }
            return matchingTypes;
        }

        void SetUpMaterials()
        {
            Materials.Clear();

            // Set up stock list
            ShaderList = new List<string>();

            ShaderList.Add("Unity Terrain Occlusion Standard PBR");
            ShaderList.Add("Unity Terrain Occlusion Standard Specular PBR");
            ShaderList.Add("Unity Terrain Blend Standard PBR");
            ShaderList.Add("Unity Terrain Blend Standard Specular PBR");
            ShaderList.Add("Mesh Terrain Occlusion Standard PBR");
            ShaderList.Add("Mesh Terrain Occlusion Standard Specular PBR");
            ShaderList.Add("Mesh Terrain Blend Standard PBR");
            ShaderList.Add("Mesh Terrain Blend Standard Specular PBR");
            //ShaderList.Add("Mobile Unity Terrain Standard PBR");
            //ShaderList.Add("Mobile Unity Terrain Standard Specular PBR");
            //ShaderList.Add("Unity Terrain Occlusion Standard PBR + VTP");

            if (string.IsNullOrEmpty(SelectedShader))
                SelectedShader = ShaderList[0];

            Materials.Add(ShaderList[0], new Material(Shader.Find("Nature/Terrain/Distingo Standard")));
            Materials.Add(ShaderList[1], new Material(Shader.Find("Nature/Terrain/Distingo Standard Specular")));

            Materials.Add(ShaderList[2], new Material(Shader.Find("Nature/Terrain/Distingo StandardWithBlend")));
            Materials.Add(ShaderList[3], new Material(Shader.Find("Nature/Terrain/Distingo Standard SpecularWithBlend")));

            Materials.Add(ShaderList[4], new Material(Shader.Find("Nature/Terrain/Distingo Mesh Terrain Splat")));
            Materials.Add(ShaderList[5], new Material(Shader.Find("Nature/Terrain/Distingo Mesh Terrain Splat Specular")));

            Materials.Add(ShaderList[6], new Material(Shader.Find("Nature/Terrain/Distingo Mesh Terrain Splat Blend")));
            Materials.Add(ShaderList[7], new Material(Shader.Find("Nature/Terrain/Distingo Mesh Terrain Splat Specular Blend")));

            //Materials.Add("ShaderList[8]", new Material(Shader.Find("Nature/Terrain/Distingo Mobile")));
            //Materials.Add("ShaderList[9]", new Material(Shader.Find("Nature/Terrain/Distingo Specular Mobile")));

            // VTP may not be present.
            //Materials.Add(ShaderList[8], new Material(Shader.Find("Nature/Terrain/Distingo+VTP Standard")));
            
        }

        void Update()
        {
            if (Application.isEditor)
                ApplyShaderVariables();
        }

        void SetUpUnityTerrain(Material material)
        {
            if (Terrain.activeTerrain.materialType != Terrain.MaterialType.Custom)
                Terrain.activeTerrain.materialType = Terrain.MaterialType.Custom;

            Terrain.activeTerrain.materialTemplate = material;
        }


        void InitializeDistingo()
        {
            // Set selected 
            switch (SelectedShader)
            {
                case "Unity Terrain Occlusion Standard PBR":
                case "Unity Terrain Occlusion Standard Specular PBR":
                case "Unity Terrain Blend Standard PBR":
                case "Unity Terrain Blend Standard Specular PBR":
                case "Mobile Unity Terrain Standard PBR":
                case "Mobile Unity Terrain Standard Specular PBR":
                case "Unity Terrain Occlusion Standard PBR + VTP":
                    if (Materials.ContainsKey(ShaderList[ShaderList.IndexOf(SelectedShader)]))
                        SetUpUnityTerrain(Materials[ShaderList[ShaderList.IndexOf(SelectedShader)]]);
                    break;
                case "Mesh Terrain Occlusion Standard PBR":
                case "Mesh Terrain Occlusion Standard Specular PBR":
                case "Mesh Terrain Blend Standard PBR":
                case "Mesh Terrain Blend Standard Specular PBR":
                    GetComponent<MeshRenderer>().sharedMaterial = Materials[ShaderList[ShaderList.IndexOf(SelectedShader)]];
                    break;
            }
        }

        void ApplyShaderVariables()
        {
            if (Materials.Count == 0)
                SetUpMaterials();

            if (gameObject.GetComponent<Terrain>() != null)
                IsMeshTerrain = false;

            if (SelectedShader.ToString().Contains("Mesh") && !IsMeshTerrain)
                SelectedShader = ShaderList[0];

            if (!SelectedShader.ToString().Contains("Mesh") && IsMeshTerrain)
                SelectedShader = ShaderList[4];

            InitializeDistingo();

            VTPPresent = false;
            if (!VTPPresent)
            {
                if (GetTypesInNamespace("VTP").Count > 0)
                {
                    VTPPresent = true;
                    if(!Materials.ContainsKey(ShaderList[8]))
                        Materials.Add(ShaderList[8], new Material(Shader.Find("Nature/Terrain/Distingo+VTP Standard")));
                }
            }


            if (SelectedShader == "Unity Terrain Occlusion Standard PBR + VTP" && !VTPPresent)
                return;

            // Update Params Same for all types
            switch (SelectedShader)
            {
                case "Unity Terrain Occlusion Standard PBR":
                case "Unity Terrain Occlusion Standard Specular PBR":
                case "Unity Terrain Blend Standard PBR":
                case "Unity Terrain Blend Standard Specular PBR":
                case "Mobile Unity Terrain Standard PBR":
                case "Mobile Unity Terrain Standard Specular PBR":
                case "Unity Terrain Occlusion Standard PBR + VTP":
                    Terrain.activeTerrain.basemapDistance = SplattingDistance;

                    Shader.SetGlobalVector("offset", new Vector4(BlendChannel0Max, BlendChannel1Max, BlendChannel2Max, BlendChannel3Max));
                    Shader.SetGlobalVector("UVMin", new Vector4(BlendChannel0Min, BlendChannel1Min, BlendChannel2Min, BlendChannel3Min));
                    Shader.SetGlobalVector("normalMod", new Vector4(BlendNChannel0, BlendNChannel1, BlendNChannel2, BlendNChannel3));

                    Shader.SetGlobalVector("_Brightness", new Vector4(Brightness0, Brightness1, Brightness2, Brightness3));

                    Shader.SetGlobalInt("ShowFallOff", ShowFallOff ? 1 : 0);

                    Shader.SetGlobalFloat("_Smoothness0", Channel0Smoothness);
                    Shader.SetGlobalFloat("_Smoothness1", Channel1Smoothness);
                    Shader.SetGlobalFloat("_Smoothness2", Channel2Smoothness);
                    Shader.SetGlobalFloat("_Smoothness3", Channel3Smoothness);

                    Shader.SetGlobalFloat("_Metallic0", Channel0Metalic);
                    Shader.SetGlobalFloat("_Metallic1", Channel1Metalic);
                    Shader.SetGlobalFloat("_Metallic2", Channel2Metalic);
                    Shader.SetGlobalFloat("_Metallic3", Channel3Metalic);

                    Shader.SetGlobalFloat("UVCutoff", MinMaxUVCutoff);

                    Shader.SetGlobalFloat("bmod", BrightnessMod);
                    Shader.SetGlobalInt("UseNormal", UseNormalShader ? 1 : 0);

                    if (SelectedShader != "Unity Terrain Occlusion Standard PBR + VTP")
                    {

                        Shader.SetGlobalFloat("_Smoothness0", Channel0Smoothness);
                        Shader.SetGlobalFloat("_Smoothness1", Channel1Smoothness);
                        Shader.SetGlobalFloat("_Smoothness2", Channel2Smoothness);
                        Shader.SetGlobalFloat("_Smoothness3", Channel3Smoothness);

                        Shader.SetGlobalFloat("_Metallic0", Channel0Metalic);
                        Shader.SetGlobalFloat("_Metallic1", Channel1Metalic);
                        Shader.SetGlobalFloat("_Metallic2", Channel2Metalic);
                        Shader.SetGlobalFloat("_Metallic3", Channel3Metalic);


                        Shader.SetGlobalFloat("bmod", BrightnessMod);

                    }

                    Shader.SetGlobalInt("UseNormal", UseNormalShader ? 1 : 0);

                    if (SelectedShader == "Unity Terrain Occlusion Standard PBR + VTP")
                    {
                        Shader.SetGlobalInt("useTriPlanar", useTriPlanar ? 1 : 0);
                        Shader.SetGlobalInt("useHeightBasedBlending", UseHeightBasedBlending ? 1 : 0);
                        Shader.SetGlobalInt("useParallax", UseParallaxMapping ? 1 : 0);
                        Shader.SetGlobalInt("parallaxInterpolations", ParallaxInterpolations);
                    }
                    else
                    {
                        Shader.SetGlobalInt("UseTriPlanar", useTriPlanar ? 1 : 0);
                        Shader.SetGlobalFloat("triPlanerMultiplier", TriPlanerUVMultiplier);
                    }
                    break;
                case "Mesh Terrain Occlusion Standard PBR":
                case "Mesh Terrain Occlusion Standard Specular PBR":
                case "Mesh Terrain Blend Standard PBR":
                case "Mesh Terrain Blend Standard Specular PBR":
                    Materials[SelectedShader].SetTexture("_MainTex", tm_SplatMap);

                    for (int t = 0; t < tm_Splat.Length; t++)
                    {
                        Materials[SelectedShader].SetTexture(string.Format("_Normal{0}", t), tm_Normal[t]);
                        Materials[SelectedShader].SetTexture(string.Format("_OcculisonTex{0}", t), tm_Normal[t]);
                        Materials[SelectedShader].SetVector(string.Format("_Splat{0}UV", t), tmp_UVs[t]);
                        Materials[SelectedShader].SetTexture(string.Format("_Splat{0}", t), tm_Splat[t]);

                    }

                    Materials[SelectedShader].SetVector("offset", new Vector4(BlendChannel0Max, BlendChannel1Max, BlendChannel2Max, BlendChannel3Max));
                    Materials[SelectedShader].SetVector("UVMin", new Vector4(BlendChannel0Min, BlendChannel1Min, BlendChannel2Min, BlendChannel3Min));
                    Materials[SelectedShader].SetVector("normalMod", new Vector4(BlendNChannel0, BlendNChannel1, BlendNChannel2, BlendNChannel3));

                    Materials[SelectedShader].SetVector("_Brightness", new Vector4(Brightness0, Brightness1, Brightness2, Brightness3));

                    Materials[SelectedShader].SetInt("ShowFallOff", ShowFallOff ? 1 : 0);

                    Materials[SelectedShader].SetFloat("_Smoothness0", Channel0Smoothness);
                    Materials[SelectedShader].SetFloat("_Smoothness1", Channel1Smoothness);
                    Materials[SelectedShader].SetFloat("_Smoothness2", Channel2Smoothness);
                    Materials[SelectedShader].SetFloat("_Smoothness3", Channel3Smoothness);

                    Materials[SelectedShader].SetFloat("_Metallic0", Channel0Metalic);
                    Materials[SelectedShader].SetFloat("_Metallic1", Channel1Metalic);
                    Materials[SelectedShader].SetFloat("_Metallic2", Channel2Metalic);
                    Materials[SelectedShader].SetFloat("_Metallic3", Channel3Metalic);

                    Materials[SelectedShader].SetFloat("UVCutoff", MinMaxUVCutoff);

                    Materials[SelectedShader].SetFloat("bmod", BrightnessMod);

                    Materials[SelectedShader].SetInt("UseTriPlanar", useTriPlanar ? 1 : 0);
                    Materials[SelectedShader].SetFloat("triPlanerMultiplier", TriPlanerUVMultiplier);

                    break;
            }

            // Specifics
            switch (SelectedShader)
            {
                case "Unity Terrain Occlusion Standard PBR":
                case "Unity Terrain Occlusion Standard Specular PBR":
                case "Unity Terrain Occlusion Standard PBR + VTP":
                    if (OcclusionMaps != null && OcclusionMaps.Length > 0)
                    {
                        for (int h = 0; h < OcclusionMaps.Length; h++)
                        {
                            Shader.SetGlobalTexture(string.Format("_OcculisonTex{0}", h), OcclusionMaps[h]);
                            Shader.SetGlobalInt(string.Format("occ{0}", h), OcclusionMaps[h] != null ? 1 : 0);
                        }
                    }
                    else
                    {
                        Shader.SetGlobalInt("occ0", 0);
                        Shader.SetGlobalInt("occ1", 0);
                        Shader.SetGlobalInt("occ2", 0);
                        Shader.SetGlobalInt("occ3", 0);
                    }

                    Shader.SetGlobalVector("_OcclusionPower", new Vector4(OcclusionPower0, OcclusionPower1, OcclusionPower2, OcclusionPower3));

                    if (SelectedShader == "Unity Terrain Occlusion Standard PBR + VTP")
                    {
                        // Pete, pass your shader values in here.
                        Shader.SetGlobalFloat("baseHeight0", BaseHeight0);
                        Shader.SetGlobalFloat("baseHeight1", BaseHeight1);
                        Shader.SetGlobalFloat("baseHeight2", BaseHeight2);
                        Shader.SetGlobalFloat("baseHeight3", BaseHeight3);

                        Shader.SetGlobalFloat("blendSmooth0", BlendSmooth0);
                        Shader.SetGlobalFloat("blendSmooth1", BlendSmooth1);
                        Shader.SetGlobalFloat("blendSmooth2", BlendSmooth2);
                        Shader.SetGlobalFloat("blendSmooth3", BlendSmooth3);

                        Shader.SetGlobalFloat("heightBasedTransparency0", HeightBasedTransparency0);
                        Shader.SetGlobalFloat("heightBasedTransparency1", HeightBasedTransparency1);
                        Shader.SetGlobalFloat("heightBasedTransparency2", HeightBasedTransparency2);
                        Shader.SetGlobalFloat("heightBasedTransparency3", HeightBasedTransparency3);

                        Shader.SetGlobalFloat("HeightmapMultiplier0", HeightMapMultiplier0);
                        Shader.SetGlobalFloat("HeightmapMultiplier1", HeightMapMultiplier1);
                        Shader.SetGlobalFloat("HeightmapMultiplier2", HeightMapMultiplier2);
                        Shader.SetGlobalFloat("HeightmapMultiplier3", HeightMapMultiplier3);

                        Shader.SetGlobalInt("isBaseLayer0", IsBaseLayer0 ? 1 : 0);
                        Shader.SetGlobalInt("isBaseLayer1", IsBaseLayer1 ? 1 : 0);
                        Shader.SetGlobalInt("isBaseLayer2", IsBaseLayer2 ? 1 : 0);
                        Shader.SetGlobalInt("isBaseLayer3", IsBaseLayer3 ? 1 : 0);


                        Shader.SetGlobalFloat("smoothnessNear0", SmoothnessNear0);
                        Shader.SetGlobalFloat("smoothnessNear1", SmoothnessNear1);
                        Shader.SetGlobalFloat("smoothnessNear2", SmoothnessNear2);
                        Shader.SetGlobalFloat("smoothnessNear3", SmoothnessNear3);

                        Shader.SetGlobalFloat("smoothnessFar0", SmoothnessFar0);
                        Shader.SetGlobalFloat("smoothnessFar1", SmoothnessFar1);
                        Shader.SetGlobalFloat("smoothnessFar2", SmoothnessFar2);
                        Shader.SetGlobalFloat("smoothnessFar3", SmoothnessFar3);

                        Shader.SetGlobalFloat("metallic0", Metallic0);
                        Shader.SetGlobalFloat("metallic1", Metallic1);
                        Shader.SetGlobalFloat("metallic2", Metallic2);
                        Shader.SetGlobalFloat("metallic3", Metallic3);

                        Shader.SetGlobalFloat("occlusion0", Occlusion0);
                        Shader.SetGlobalFloat("occlusion1", Occlusion1);
                        Shader.SetGlobalFloat("occlusion2", Occlusion2);
                        Shader.SetGlobalFloat("occlusion3", Occlusion3);

                        Shader.SetGlobalFloat("occludeAlbedo0", OccludeAlbedo0);
                        Shader.SetGlobalFloat("occludeAlbedo1", OccludeAlbedo1);
                        Shader.SetGlobalFloat("occludeAlbedo2", OccludeAlbedo2);
                        Shader.SetGlobalFloat("occludeAlbedo3", OccludeAlbedo3);

                        Shader.SetGlobalFloat("parallaxStrength0", ParallaxStrength0);
                        Shader.SetGlobalFloat("parallaxStrength1", ParallaxStrength1);
                        Shader.SetGlobalFloat("parallaxStrength2", ParallaxStrength2);
                        Shader.SetGlobalFloat("parallaxStrength3", ParallaxStrength3);

                    }

                    break;
                case "Unity Terrain Blend Standard PBR":
                case "Unity Terrain Blend Standard Specular PBR":
                    Shader.SetGlobalInt("doGlobalBelnd", GlobalBlendTexture != null ? 1 : 0);
                    Shader.SetGlobalTexture("GlobalBlend", GlobalBlendTexture);
                    Shader.SetGlobalFloat("BlendPower", BlendPower);

                    Shader.SetGlobalInt("doGlobalOcclude", GlobalOcclusionTexture != null ? 1 : 0);
                    Shader.SetGlobalTexture("OcclusionBlend", GlobalOcclusionTexture);
                    Shader.SetGlobalFloat("OccludePower", OcclusionPower);
                    break;
                case "Mobile Unity Terrain Standard PBR":
                case "Mobile Unity Terrain Standard Specular PBR":
                    // None
                    break;
                case "Mesh Terrain Occlusion Standard PBR":
                case "Mesh Terrain Occlusion Standard Specular PBR":
                    if (OcclusionMaps != null && OcclusionMaps.Length > 0)
                    {
                        for (int h = 0; h < OcclusionMaps.Length; h++)
                        {
                            Materials[SelectedShader].SetTexture(string.Format("_OcculisonTex{0}", h), OcclusionMaps[h]);
                            Materials[SelectedShader].SetInt(string.Format("occ{0}", h), OcclusionMaps[h] != null ? 1 : 0);
                        }
                    }
                    else
                    {
                        Materials[SelectedShader].SetInt("occ0", 0);
                        Materials[SelectedShader].SetInt("occ1", 0);
                        Materials[SelectedShader].SetInt("occ2", 0);
                        Materials[SelectedShader].SetInt("occ3", 0);
                    }

                    Materials[SelectedShader].SetVector("_OcclusionPower", new Vector4(OcclusionPower0, OcclusionPower1, OcclusionPower2, OcclusionPower3));
                    break;
                case "Mesh Terrain Blend Standard PBR":
                case "Mesh Terrain Blend Standard Specular PBR":
                    Materials[SelectedShader].SetInt("doGlobalBelnd", GlobalBlendTexture != null ? 1 : 0);
                    Materials[SelectedShader].SetTexture("GlobalBlend", GlobalBlendTexture);
                    Materials[SelectedShader].SetFloat("BlendPower", BlendPower);

                    Materials[SelectedShader].SetInt("doGlobalOcclude", GlobalOcclusionTexture != null ? 1 : 0);
                    Materials[SelectedShader].SetTexture("OcclusionBlend", GlobalOcclusionTexture);
                    Materials[SelectedShader].SetFloat("OccludePower", OcclusionPower);
                    break;
            }

            lastSelectedShader = SelectedShader;

        }
    }
}
    