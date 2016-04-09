Shader "Hidden/TerrainEngine/Splatmap/Distingo Standard-AddPass" 
{
	Properties 
	{
		// set by terrain engine
		[HideInInspector] _Control ("Control (RGBA)", 2D) = "red" {}
		[HideInInspector] _Splat3 ("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Splat2 ("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat1 ("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat0 ("Layer 0 (R)", 2D) = "white" {}
		[HideInInspector] _Normal3 ("Normal 3 (A)", 2D) = "bump" {}
		[HideInInspector] _Normal2 ("Normal 2 (B)", 2D) = "bump" {}
		[HideInInspector] _Normal1 ("Normal 1 (G)", 2D) = "bump" {}
		[HideInInspector] _Normal0 ("Normal 0 (R)", 2D) = "bump" {}
		/*[HideInInspector] [Gamma] _Metallic0 ("Metallic 0", Range(0.0, 1.0)) = 0.0	
		[HideInInspector] [Gamma] _Metallic1 ("Metallic 1", Range(0.0, 1.0)) = 0.0	
		[HideInInspector] [Gamma] _Metallic2 ("Metallic 2", Range(0.0, 1.0)) = 0.0	
		[HideInInspector] [Gamma] _Metallic3 ("Metallic 3", Range(0.0, 1.0)) = 0.0
		[HideInInspector] _Smoothness0 ("Smoothness 0", Range(0.0, 1.0)) = 1.0	
		[HideInInspector] _Smoothness1 ("Smoothness 1", Range(0.0, 1.0)) = 1.0	
		[HideInInspector] _Smoothness2 ("Smoothness 2", Range(0.0, 1.0)) = 1.0	
		[HideInInspector] _Smoothness3 ("Smoothness 3", Range(0.0, 1.0)) = 1.0*/
	}

	SubShader 
	{
		Tags 
		{
			"SplatCount" = "4"
			"Queue" = "Geometry-99"
			"IgnoreProjector"="True"
			"RenderType" = "Opaque"
		}

		CGPROGRAM
		// As we can't blend normals in g-buffer, this shader will not work in standard deferred path. 
		// So we use exclude_path:deferred to force it to only use the forward path.
		#pragma surface surf Standard decal:add vertex:SplatmapVert finalcolor:myfinal exclude_path:prepass exclude_path:deferred fullforwardshadows
		#pragma multi_compile_fog
		#pragma target 4.0
		// needs more than 8 texcoords
		#pragma exclude_renderers gles
		#include "UnityPBSLighting.cginc"

		#pragma multi_compile __ _TERRAIN_NORMAL_MAP

		#define TERRAIN_SPLAT_ADDPASS
		#define TERRAIN_STANDARD_SHADER
		#include "DistingoTerrainSplatmapCommon.cginc"

		half _Metallic0;
		half _Metallic1;
		half _Metallic2;
		half _Metallic3;
		
		half _Smoothness0;
		half _Smoothness1;
		half _Smoothness2;
		half _Smoothness3;

		float4 normalMod = float4(1, 1, 1, 1);
		float bmod = 1.5;

		int ShowFallOff = 1;

		void surf(Input IN, inout SurfaceOutputStandard o) 
		{
			half4 splat_control;
			half weight;
			fixed4 mixedDiffuse;
			half4 defaultSmoothness = half4(_Smoothness0, _Smoothness1, _Smoothness2, _Smoothness3);
			
			float3 pos = (IN.pos.xyz / IN.pos.w);
			float d = pow(pos.z, 256);
			
			float4 occlusion = 1;

			SplatmapMix(IN, defaultSmoothness, splat_control, weight, mixedDiffuse, o.Normal, UVMin, offset, normalMod, d, occlusion );

			o.Albedo = mixedDiffuse.rgb + (float4(0,d,0,1) * UVCutoff * ShowFallOff);
			o.Alpha = weight;
			o.Occlusion = occlusion;
			o.Smoothness = mixedDiffuse.a;
			o.Metallic = dot(splat_control, half4(_Metallic0, _Metallic1, _Metallic2, _Metallic3));
		}

		void myfinal(Input IN, SurfaceOutputStandard o, inout fixed4 color)
		{
			SplatmapApplyWeight(color, o.Alpha);
			SplatmapApplyFog(color, IN);
		}

		ENDCG
	}

	Fallback "Hidden/TerrainEngine/Splatmap/Distingo Diffuse-AddPass"
}
