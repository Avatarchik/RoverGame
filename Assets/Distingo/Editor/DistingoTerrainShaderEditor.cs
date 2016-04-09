using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RandomchaosLtd.Distingo
{
    [CustomEditor(typeof(DistingoTerrainScript))]
    public class DistingoTerrainShaderEditor : Editor
    {
        List<bool> ShowChannel = new List<bool>();
        List<bool> ShowVTPPBR = new List<bool>();
        List<bool> ShowVTPHBS = new List<bool>();

        DistingoTerrainScript thisDistingoTerrainScript;

        GUIStyle headingStyle;
        GUIStyle style;
        GUIStyle textArea;

        bool ShowGlobalSettings = false;

        void OnEnable()
        {
            
            //ShowChannel = new List<bool>();

            thisDistingoTerrainScript = (DistingoTerrainScript)target;
            
            SetShowChannels();

        }

        void SetShowChannels()
        {
            ShowChannel.Clear();
            if (!thisDistingoTerrainScript.IsMeshTerrain)
            {
                for (int t = 0; t < thisDistingoTerrainScript.GetComponent<Terrain>().terrainData.splatPrototypes.Length; t++)
                {
                    ShowChannel.Add(false);
                    ShowVTPPBR.Add(false);
                    ShowVTPHBS.Add(false);
                }
            }
            else
            {
                for (int t = 0; t < 4; t++)
                {
                    ShowChannel.Add(false);
                    ShowVTPPBR.Add(false);
                    ShowVTPHBS.Add(false);
                }
            }

            ShowGlobalSettings = false;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            headingStyle = new GUIStyle();
            headingStyle.alignment = TextAnchor.MiddleLeft;
            headingStyle.normal.textColor = GUI.skin.label.normal.textColor;     

            
            style = new GUIStyle();
            style.alignment = TextAnchor.MiddleLeft;
            style.fontSize = 18;
            style.fontStyle = FontStyle.Italic;
            style.normal.textColor = GUI.skin.label.normal.textColor;

            textArea = GUI.skin.textArea;
            textArea.richText = true;

            string ChannelName = string.Empty;

            if (Application.HasProLicense())
                EditorGUILayout.LabelField(new GUIContent(Resources.Load<Texture2D>("distingo_dark")), headingStyle, GUILayout.Height(64));
            else
                EditorGUILayout.LabelField(new GUIContent(Resources.Load<Texture2D>("distingo_light")), GUILayout.Height(64));

            headingStyle.alignment = TextAnchor.MiddleCenter;


            EditorGUILayout.LabelField(string.Format("Version {0}", DistingoTerrainScript.Version), style);
            style.alignment = TextAnchor.MiddleCenter;


            thisDistingoTerrainScript.SelectedShader = thisDistingoTerrainScript.ShaderList[EditorGUILayout.Popup("Shader Type", thisDistingoTerrainScript.ShaderList.IndexOf(thisDistingoTerrainScript.SelectedShader), thisDistingoTerrainScript.ShaderList.ToArray())];

            if (thisDistingoTerrainScript.SelectedShader == "Unity Terrain Occlusion Standard PBR + VTP" && !thisDistingoTerrainScript.VTPPresent)
            {
                Color orgCol = textArea.normal.textColor;
                textArea.normal.textColor = Color.red;
                EditorGUILayout.TextArea("<b>Vertext Tools Pro</b>\nYou do not have VTP installed, in order to use this shader, you MUST install VTP. To buy VTP click the button below.", textArea, GUILayout.MaxWidth(500));
                if (GUILayout.Button(new GUIContent("Buy Vertext Tool Pro")))
                    Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/55649");

                textArea.normal.textColor = orgCol;
            }

             if (!thisDistingoTerrainScript.IsMeshTerrain && ShowChannel.Count != thisDistingoTerrainScript.GetComponent<Terrain>().terrainData.splatPrototypes.Length)
                SetShowChannels();

            //if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Mobile"))
            //    EditorGUILayout.TextArea("<b>Mobile Version</b>\nThis version is trimmed down. It has a minimum requirement of SM3 and has calculations for Fog removed", textArea, GUILayout.MaxWidth(500));

            if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Occlusion"))
                EditorGUILayout.TextArea("<b>Occlusion Shaders</b>\nOcclusion shaders require that the Lightmap static be disabled as they will run out of texture registers, please see documentation for more details.", textArea, GUILayout.MaxWidth(500));

            if (GUILayout.Button(new GUIContent("Global Settings", "Edit parameters that affect all textures")))
                ShowGlobalSettings = !ShowGlobalSettings;

            // VTP conditioal stuff..
            bool IsBaseSet = false;

            // Do we have UseHeightBasedBlending set
            if (thisDistingoTerrainScript.SelectedShader == "Unity Terrain Occlusion Standard PBR + VTP" && thisDistingoTerrainScript.UseHeightBasedBlending)
            {
                // Do we have ANY base layers set?
                IsBaseSet = thisDistingoTerrainScript.IsBaseLayer0 || thisDistingoTerrainScript.IsBaseLayer1 || thisDistingoTerrainScript.IsBaseLayer2 || thisDistingoTerrainScript.IsBaseLayer3;
            }
            
            if (ShowGlobalSettings)
            {
                // Generic
                switch (thisDistingoTerrainScript.SelectedShader)
                {
                    case "Unity Terrain Occlusion Standard PBR":
                    case "Unity Terrain Occlusion Standard Specular PBR":
                    case "Unity Terrain Blend Standard PBR":
                    case "Unity Terrain Blend Standard Specular PBR":
                    //case "Mobile Unity Terrain Standard PBR":
                    //case "Mobile Unity Terrain Standard Specular PBR":
                    case "Unity Terrain Occlusion Standard PBR + VTP":
                        EditorGUILayout.Slider(serializedObject.FindProperty("SplattingDistance"), 100, 10000, new GUIContent("Base Map Dist.", "This alters the terrains basemapDistance"));
                        serializedObject.FindProperty("UseNormalShader").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Unity Shader", "Set this to true to use the built in Unity Shaders"), serializedObject.FindProperty("UseNormalShader").boolValue);

                        EditorGUILayout.Slider(serializedObject.FindProperty("MinMaxUVCutoff"), 0, 2, new GUIContent("Min Max UV CutOff", "Distance from camera where the switch between near and far UV multiplier occur"));
                        serializedObject.FindProperty("ShowFallOff").boolValue = EditorGUILayout.Toggle(new GUIContent("Render CutOff", "Show the cutoff in the render"), serializedObject.FindProperty("ShowFallOff").boolValue);
                        break;
                    case "Mesh Terrain Occlusion Standard PBR":
                    case "Mesh Terrain Occlusion Standard Specular PBR":
                    case "Mesh Terrain Blend Standard PBR":
                    case "Mesh Terrain Blend Standard Specular PBR":
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Splat Map", headingStyle, GUILayout.Width(64));
                        if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Blend"))
                        {
                            EditorGUILayout.LabelField("Blend", headingStyle, GUILayout.Width(64));
                            EditorGUILayout.LabelField("Occlusion", headingStyle, GUILayout.Width(64));
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        thisDistingoTerrainScript.tm_SplatMap = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.tm_SplatMap, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                        if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Blend"))
                        {
                            thisDistingoTerrainScript.GlobalBlendTexture = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.GlobalBlendTexture, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                            thisDistingoTerrainScript.GlobalOcclusionTexture = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.GlobalOcclusionTexture, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                        }
                        EditorGUILayout.EndHorizontal();

                        if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Blend"))
                        {
                            EditorGUILayout.Slider(serializedObject.FindProperty("BlendPower"), 0, 2, new GUIContent("Blend Power", "The amount the texture is blended with the terrain."));
                            EditorGUILayout.Slider(serializedObject.FindProperty("OcclusionPower"), 0, 1, new GUIContent("Occlusion Power", "The amount of occlusion applied."));
                        }

                        EditorGUILayout.Slider(serializedObject.FindProperty("MinMaxUVCutoff"), 0, 2, new GUIContent("Min Max UV CutOff", "Distance from camera where the switch between near and far UV multiplier occur"));
                        serializedObject.FindProperty("ShowFallOff").boolValue = EditorGUILayout.Toggle(new GUIContent("Render CutOff", "Show the cutoff in the render"), serializedObject.FindProperty("ShowFallOff").boolValue);

                        break;

                }

                // Specific
                switch (thisDistingoTerrainScript.SelectedShader)
                {
                    case "Unity Terrain Occlusion Standard PBR":
                    case "Unity Terrain Occlusion Standard Specular PBR":
                        serializedObject.FindProperty("useTriPlanar").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Triplanar Mapping", "Use UV Free mapping to avoid texture stretchin on steep terrain"), serializedObject.FindProperty("useTriPlanar").boolValue);
                        if(thisDistingoTerrainScript.useTriPlanar)
                            EditorGUILayout.Slider(serializedObject.FindProperty("TriPlanerUVMultiplier"), 0, 1, new GUIContent("Triplanar Multiplier", "Base multiplier for Triplaner textures"));
                        break;
                    case "Unity Terrain Blend Standard PBR":
                    case "Unity Terrain Blend Standard Specular PBR":
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Blend", headingStyle, GUILayout.Width(64));
                        EditorGUILayout.LabelField("Occlusion", headingStyle, GUILayout.Width(64));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        thisDistingoTerrainScript.GlobalBlendTexture = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.GlobalBlendTexture, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                        thisDistingoTerrainScript.GlobalOcclusionTexture = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.GlobalOcclusionTexture, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Slider(serializedObject.FindProperty("BlendPower"), 0, 2, new GUIContent("Blend Power", "The amount the texture is blended with the terrain."));
                        EditorGUILayout.Slider(serializedObject.FindProperty("OcclusionPower"), 0, 1, new GUIContent("Occlusion Power", "The amount of occlusion applied."));
                        serializedObject.FindProperty("useTriPlanar").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Triplanar Mapping", "Use UV Free mapping to avoid texture stretchin on steep terrain"), serializedObject.FindProperty("useTriPlanar").boolValue);
                        if (thisDistingoTerrainScript.useTriPlanar)
                            EditorGUILayout.Slider(serializedObject.FindProperty("TriPlanerUVMultiplier"), 0, 1, new GUIContent("Triplanar Multiplier", "Base multiplier for Triplaner textures"));
                        break;
                    case "Unity Terrain Occlusion Standard PBR + VTP":
                        serializedObject.FindProperty("useTriPlanar").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Triplanar Mapping", "Use UV Free mapping to avoid texture stretchin on steep terrain"), serializedObject.FindProperty("useTriPlanar").boolValue);
                        serializedObject.FindProperty("UseHeightBasedBlending").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Height Based Blending", "Set this to true to use VTP height based blending"), serializedObject.FindProperty("UseHeightBasedBlending").boolValue);

                        if (!thisDistingoTerrainScript.useTriPlanar)
                            serializedObject.FindProperty("UseParallaxMapping").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Parallax Mapping", "Set this to true to use VTP parallax mapping"), serializedObject.FindProperty("UseParallaxMapping").boolValue);

                        if (thisDistingoTerrainScript.UseParallaxMapping)
                            EditorGUILayout.IntSlider(serializedObject.FindProperty("ParallaxInterpolations"), 5, 30, new GUIContent("Parallax Interpolations", "Change the samples of the parallax effect"));
                        //serializedObject.FindProperty("UseHeightBasedBlending").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Height Based Blending", "Set this to true to use VTP height based blending"), serializedObject.FindProperty("UseHeightBasedBlending").boolValue);
                        //serializedObject.FindProperty("UseParallaxMapping").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Parallax Mapping", "Set this to true to use VTP parallax mapping"), serializedObject.FindProperty("UseParallaxMapping").boolValue);
                        ////if(thisDistingoTerrainScript.UseParallaxMapping)
                        ////    EditorGUILayout.Slider(serializedObject.FindProperty("ParallaxStrength"), 0, 1, new GUIContent("Parallax Strength", "Strength of VTP parallax mapping used."));
                        break;
                    case "Mesh Terrain Blend Standard PBR":
                    case "Mesh Terrain Blend Standard Specular PBR":
                    //case "Mobile Unity Terrain Standard PBR":
                    //case "Mobile Unity Terrain Standard Specular PBR":
                    case "Mesh Terrain Occlusion Standard PBR":
                    case "Mesh Terrain Occlusion Standard Specular PBR":
                        serializedObject.FindProperty("useTriPlanar").boolValue = EditorGUILayout.Toggle(new GUIContent("Use Triplanar Mapping", "Use UV Free mapping to avoid texture stretchin on steep terrain"), serializedObject.FindProperty("useTriPlanar").boolValue);
                        if (thisDistingoTerrainScript.useTriPlanar)
                            EditorGUILayout.Slider(serializedObject.FindProperty("TriPlanerUVMultiplier"), 0, 1, new GUIContent("Triplanar Multiplier", "Base multiplier for Triplaner textures"));
                        break;
                }
            }
            // Channels
            for (int c = 0; c < ShowChannel.Count; c++)
            {
                ChannelName = string.Format("Texture Channel {0}", c);
                if (GUILayout.Button(new GUIContent(ChannelName, string.Format("Terrain Texture Channel {0} parameters", c))))
                    ShowChannel[c] = !ShowChannel[c];

                if (ShowChannel[c])
                {
                    int tc = c;

                    switch (thisDistingoTerrainScript.SelectedShader)
                    {
                        case "Unity Terrain Occlusion Standard PBR":
                        case "Unity Terrain Occlusion Standard Specular PBR":
                        case "Mesh Terrain Occlusion Standard PBR":
                        case "Mesh Terrain Occlusion Standard Specular PBR":
                        case "Unity Terrain Blend Standard PBR":
                        case "Unity Terrain Blend Standard Specular PBR":
                        case "Mesh Terrain Blend Standard PBR":
                        case "Mesh Terrain Blend Standard Specular PBR":
                        case "Unity Terrain Occlusion Standard PBR + VTP":
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(new GUIContent("Splat", "This is the splat texture set up in your terrain, it can not be edited here."), headingStyle, GUILayout.Width(64));
                            EditorGUILayout.LabelField(new GUIContent("Normal", "This is the normal map set up in your terrain, it can not be edited here."), headingStyle, GUILayout.Width(64));
                            if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Occlusion"))
                            {
                                if (!thisDistingoTerrainScript.SelectedShader.ToString().Contains("VTP"))
                                    EditorGUILayout.LabelField(new GUIContent("Occlusion", "This is the occlusion map to be applied to this texture channel, this can be edited here."), headingStyle, GUILayout.Width(64));
                                else
                                    EditorGUILayout.LabelField(new GUIContent("VTP Map", "This combined texture map used by VTP."), headingStyle, GUILayout.Width(64));
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Unity Terrain"))
                            {
                                EditorGUILayout.ObjectField(thisDistingoTerrainScript.GetComponent<Terrain>().terrainData.splatPrototypes[c].texture, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                                EditorGUILayout.ObjectField(thisDistingoTerrainScript.GetComponent<Terrain>().terrainData.splatPrototypes[c].normalMap, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                            }
                            else
                            {
                                thisDistingoTerrainScript.tm_Splat[c] = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.tm_Splat[c], typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                                thisDistingoTerrainScript.tm_Normal[c] = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.tm_Normal[c], typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                            }

                            if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Occlusion"))
                                thisDistingoTerrainScript.OcclusionMaps[c] = (Texture2D)EditorGUILayout.ObjectField(thisDistingoTerrainScript.OcclusionMaps[c], typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));

                            EditorGUILayout.EndHorizontal();

                            if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Mesh"))
                                thisDistingoTerrainScript.tmp_UVs[c] = EditorGUILayout.Vector2Field(new GUIContent("Base UV", ""), thisDistingoTerrainScript.tmp_UVs[c]);

                            
                            if (tc >= 4)
                                tc = (tc % 4);

                            EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BlendChannel{0}Max", tc)), DistingoTerrainScript.minUV, DistingoTerrainScript.maxUV, new GUIContent("Near UV Multiplier", "Number used to multiply the UV's when near to the camera"));
                            EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BlendChannel{0}Min", tc)), DistingoTerrainScript.minUV, DistingoTerrainScript.maxUV, new GUIContent("Far UV Multiplier", "Number used to multiply the UV's when far from the camera"));
                            if (!thisDistingoTerrainScript.SelectedShader.ToString().Contains("VTP"))
                                EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BlendNChannel{0}", tc)), DistingoTerrainScript.minN, DistingoTerrainScript.maxN, new GUIContent("Normal Power", "The amound the normal map is increased by."));
                            if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("Occlusion"))
                                EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("OcclusionPower{0}", tc)), 0, 1, new GUIContent("Occlusion Power", "Level off occlusion applied"));

                            if (!thisDistingoTerrainScript.SelectedShader.ToString().Contains("VTP"))
                            {
                                EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Channel{0}Smoothness", tc)), 0, 1, new GUIContent("Smoothness", "Alter the smoothness of the texture"));
                                if (!thisDistingoTerrainScript.SelectedShader.ToString().Contains("Specular"))
                                    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Channel{0}Metalic", tc)), 0, 1, new GUIContent("Metallic", "Alter the metalic nature of the texture"));
                                else
                                    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Channel{0}Metalic", tc)), 0, 2, new GUIContent("Specular", "Alter the specular nature of the texture"));
                            }

                            if (thisDistingoTerrainScript.SelectedShader.ToString().Contains("VTP"))
                            {
                                string vtpLogo = "VTP_dark";
                                if (!Application.HasProLicense())
                                    vtpLogo = "VTP_light";

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.Space();
                                if (GUILayout.Button(new GUIContent("VTP PBR Parameters", Resources.Load<Texture2D>(vtpLogo), "VTP PBR Parameters"), GUILayout.Height(24), GUILayout.MaxWidth(250)))
                                    ShowVTPPBR[c] = !ShowVTPPBR[c];
                                EditorGUILayout.Space();
                                EditorGUILayout.EndHorizontal();

                                if (ShowVTPPBR[c])
                                {
                                    if (thisDistingoTerrainScript.UseParallaxMapping)
                                        EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("ParallaxStrength{0}", tc)), -0.15f, 0.15f, new GUIContent("Parallax Strength", "Strength of VTP parallax mapping used."));

                                    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Metallic{0}", tc)), 0, 1, new GUIContent("Metallic", "Alter the metallicness of the texture"));

                                    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("SmoothnessNear{0}", tc)), 0, 1, new GUIContent("Smoothness [Near]", "Alter the smoothness of the texture"));
                                    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("SmoothnessFar{0}", tc)), 0, 1, new GUIContent("Smoothness [Far]", "Alter the smoothness of the texture"));
                                    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Occlusion{0}", tc)), 0, 1, new GUIContent("Occlusion", "Alter the occlusion of the texture"));
                                    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("OccludeAlbedo{0}", tc)), 0, 1, new GUIContent("Occlude Albedo", "Amount how much Albedo should be occluded"));
                                }

                                EditorGUILayout.Space();

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.Space();
                                if (GUILayout.Button(new GUIContent("VTP Heightbase Parameters", Resources.Load<Texture2D>(vtpLogo), "VTP Heightbase Parameters"), GUILayout.Height(24), GUILayout.MaxWidth(250)))
                                    ShowVTPHBS[c] = !ShowVTPHBS[c];
                                EditorGUILayout.Space();
                                EditorGUILayout.EndHorizontal();
                                

                                if (ShowVTPHBS[c])
                                {
                                    if (thisDistingoTerrainScript.UseHeightBasedBlending)
                                    {
                                        bool prevVal = serializedObject.FindProperty(string.Format("IsBaseLayer{0}", tc)).boolValue;
                                        serializedObject.FindProperty(string.Format("IsBaseLayer{0}", tc)).boolValue = EditorGUILayout.Toggle(new GUIContent("Is Base Layer", ""), serializedObject.FindProperty(string.Format("IsBaseLayer{0}", tc)).boolValue);

                                        if (prevVal != serializedObject.FindProperty(string.Format("IsBaseLayer{0}", tc)).boolValue)
                                        {
                                            IsBaseSet = serializedObject.FindProperty(string.Format("IsBaseLayer{0}", tc)).boolValue;
                                            // Set all others to false
                                            for (int b = 0; b < 4; b++)
                                            {
                                                if (b != tc && IsBaseSet)
                                                    serializedObject.FindProperty(string.Format("IsBaseLayer{0}", b)).boolValue = false;
                                            }
                                        }
                                        else
                                            if (prevVal)
                                            IsBaseSet = true;

                                        EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BaseHeight{0}", tc)), -2, 2, new GUIContent("Base Height", "Height map base."));
                                        EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("HeightBasedTransparency{0}", tc)), 0, 1, new GUIContent("Heightbased Transparency", ""));
                                        EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BlendSmooth{0}", tc)), 0, 1, new GUIContent("Blend Smooth", ""));
                                        EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("HeightMapMultiplier{0}", tc)), 0, 1, new GUIContent("Heightmap Multiplier", ""));
                                    }
                                }
                                EditorGUILayout.Space();
                            }
                            
                            if (!thisDistingoTerrainScript.SelectedShader.ToString().Contains("VTP"))
                                EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Brightness{0}", tc)), 0, 8, new GUIContent("Brightness", "Raise or lower how bright this texture is rendered"));
                            break;
                        //case "Mobile Unity Terrain Standard PBR":
                        //case "Mobile Unity Terrain Standard Specular PBR":
                        //    EditorGUILayout.BeginHorizontal();
                        //    EditorGUILayout.LabelField(new GUIContent("Splat", "This is the splat texture set up in your terrain, it can not be edited here."), headingStyle, GUILayout.Width(64));
                        //    EditorGUILayout.LabelField(new GUIContent("Normal", "This is the normal map set up in your terrain, it can not be edited here."), headingStyle, GUILayout.Width(64));
                        //    EditorGUILayout.EndHorizontal();

                        //    EditorGUILayout.BeginHorizontal();
                        //    EditorGUILayout.ObjectField(thisDistingoTerrainScript.GetComponent<Terrain>().terrainData.splatPrototypes[c].texture, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));
                        //    EditorGUILayout.ObjectField(thisDistingoTerrainScript.GetComponent<Terrain>().terrainData.splatPrototypes[c].normalMap, typeof(Texture2D), false, GUILayout.Height(64), GUILayout.Width(64));

                        //    EditorGUILayout.EndHorizontal();

                        //    if (tc >= 4)
                        //        tc = (tc % 4);

                        //    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BlendChannel{0}Max", tc)), DistingoTerrainScript.minUV, DistingoTerrainScript.maxUV, new GUIContent("Near UV Multiplier", "Number used to multiply the UV's when near to the camera"));
                        //    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BlendChannel{0}Min", tc)), DistingoTerrainScript.minUV, DistingoTerrainScript.maxUV, new GUIContent("Far UV Multiplier", "Number used to multiply the UV's when far from the camera"));
                        //    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("BlendNChannel{0}", tc)), DistingoTerrainScript.minN, DistingoTerrainScript.maxN, new GUIContent("Normal Power", "The amound the normal map is increased by."));

                        //    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Channel{0}Smoothness", tc)), 0, 1, new GUIContent("Smoothness", "Alter the smoothness of the texture"));
                        //    if (!thisDistingoTerrainScript.SelectedShader.ToString().Contains("Specular"))
                        //        EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Channel{0}Metalic", tc)), 0, 1, new GUIContent("Metallic", "Alter the metalic nature of the texture"));
                        //    else
                        //        EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Channel{0}Metalic", tc)), 0, 2, new GUIContent("Specular", "Alter the specular nature of the texture"));

                        //    EditorGUILayout.Slider(serializedObject.FindProperty(string.Format("Brightness{0}", tc)), 0, 8, new GUIContent("Brightness", "Raise or lower how bright this texture is rendered"));
                        //    break;
                    }
                }
            }

            // VTP conditioal stuff..
            // Do we have UseHeightBasedBlending set
            if (thisDistingoTerrainScript.SelectedShader == "Unity Terrain Occlusion Standard PBR + VTP" && thisDistingoTerrainScript.UseHeightBasedBlending && !IsBaseSet)
            {
                thisDistingoTerrainScript.IsBaseLayer0 = true;
                thisDistingoTerrainScript.IsBaseLayer1 = IsBaseSet;
                thisDistingoTerrainScript.IsBaseLayer2 = IsBaseSet;
                thisDistingoTerrainScript.IsBaseLayer3 = IsBaseSet;
            }

            if (thisDistingoTerrainScript.SelectedShader == "Unity Terrain Occlusion Standard PBR + VTP")
            {
                if(GUILayout.Button(new GUIContent("Vertext Tool Pro Forum")))
                    Application.OpenURL("http://forum.unity3d.com/threads/vertex-tools-pro-heightbased-pbr-material-blending-flowmapping-and-mesh-deform.385577/page-999");

                if (GUILayout.Button(new GUIContent("Distingo + VTP Forum")))
                    Application.OpenURL("http://forum.unity3d.com/threads/wip-distingo-vertex-tools-pro-high-quality-terrain-rendering.391816/#page-9999");
            }

            if (GUILayout.Button(new GUIContent("Distingo Forum", "Go to the latest forum Disitngo Forum post.")))
                Application.OpenURL("http://forum.unity3d.com/threads/released-distingo-terrain-in-detail.383275/page-999");

#if !GAIA_PRESENT
            string gaiaLogo = "gaia_logo_dark BG";
            if (!Application.HasProLicense())
                gaiaLogo = "gaia_logo_light BG";

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            if (GUILayout.Button(new GUIContent(Resources.Load<Texture2D>(gaiaLogo), "Click to buy a copy of Gaia now!"), GUILayout.Height(64), GUILayout.MaxWidth(250)))
                Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/42618");

            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
#endif

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

