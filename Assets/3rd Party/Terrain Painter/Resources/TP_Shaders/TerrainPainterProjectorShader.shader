Shader "Terrain Painter/Projector"
{
	Properties
	{
		_MainTex ("Brush", 2D) = "" { }
		_Color ("Color", Color) = (0.0, 0.34, 1.0, 1.0)
	}

	SubShader
	{
		Pass
		{
			Tags{ "RenderType" = "Transparent" }
			ZWrite off
			Blend SrcAlpha One

			CGPROGRAM
			#pragma nofog
			#pragma vertex vert
			#pragma fragment frag 

			uniform sampler2D _MainTex;		// Alpha texture
			uniform float4 _Color;			// Color
			uniform float4x4 _Projector;	// Projector

			struct vertexInput
			{ // in
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertexOutput
			{ // out
				float4 pos : SV_POSITION;
				float4 posProj : TEXCOORD0;
			};

			vertexOutput vert (vertexInput input)
			{ // vertex
				vertexOutput output;

				output.posProj = mul(_Projector, input.vertex);
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{ // fragment
				return _Color * tex2D(_MainTex,  input.posProj.xy / input.posProj.w);
			}

			ENDCG
		}
	}
}
