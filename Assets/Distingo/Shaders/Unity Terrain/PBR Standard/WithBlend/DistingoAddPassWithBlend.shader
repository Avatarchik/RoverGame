Shader "Hidden/TerrainEngine/Splatmap/Distingo Diffuse-AddPassWithBlend" 
{
	Properties 
	{
		[HideInInspector] _Control ("Control (RGBA)", 2D) = "black" {}
		[HideInInspector] _Splat3 ("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Splat2 ("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat1 ("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat0 ("Layer 0 (R)", 2D) = "white" {}
		[HideInInspector] _Normal3 ("Normal 3 (A)", 2D) = "bump" {}
		[HideInInspector] _Normal2 ("Normal 2 (B)", 2D) = "bump" {}
		[HideInInspector] _Normal1 ("Normal 1 (G)", 2D) = "bump" {}
		[HideInInspector] _Normal0 ("Normal 0 (R)", 2D) = "bump" {}
	}

	CGINCLUDE
		#pragma surface surf Lambert decal:add vertex:SplatmapVert finalcolor:myfinal exclude_path:prepass exclude_path:deferred
		#pragma multi_compile_fog
		#define TERRAIN_SPLAT_ADDPASS
		#include "DistingoTerrainSplatmapCommonWithBlend.cginc"

		float4 normalMod = float4(1, 1, 1, 1);
		float bmod = 1.5;

		int ShowFallOff = 1;

		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 splat_control;
			half weight;
			fixed4 mixedDiffuse;
			float3 pos = (IN.pos.xyz / IN.pos.w);
			float d = pow(pos.z, 256);

			SplatmapMix(IN, splat_control, weight, mixedDiffuse, o.Normal, UVMin, offset, normalMod, d);

			o.Albedo = mixedDiffuse.rgb + (float4(0,d,0,1) * UVCutoff * ShowFallOff);
			o.Alpha = weight;
		}

		void myfinal(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			SplatmapApplyWeight(color, o.Alpha);
			SplatmapApplyFog(color, IN);
		}

	ENDCG

	Category 
	{
		Tags 
		{
			"SplatCount" = "4"
			"Queue" = "Geometry-99"
			"IgnoreProjector"="True"
			"RenderType" = "Opaque"
		}
		// TODO: Seems like "#pragma target 3.0 _TERRAIN_NORMAL_MAP" can't fallback correctly on less capable devices?
		// Use two sub-shaders to simulate different features for different targets and still fallback correctly.
		SubShader { // for sm3.0+ targets
			CGPROGRAM
				#pragma target 4.0
				#pragma multi_compile __ _TERRAIN_NORMAL_MAP
			ENDCG
		}
		SubShader { // for sm2.0 targets
			CGPROGRAM
				#pragma target 4.0
			ENDCG
		}
	}

	Fallback off
}
