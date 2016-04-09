
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
#pragma exclude_renderers gles

float4 offset = float4(0, 0, 0, 0);
float4 UVMin = float4(0, 0, 0, 0);
float UVCutoff = 0;

float4 _Brightness;

sampler2D GlobalBlend;
sampler2D OcclusionBlend;

struct Input
{
	float2 uv_Splat0 : TEXCOORD0;
	float2 uv_Splat1 : TEXCOORD1;
	float2 uv_Splat2 : TEXCOORD2;
	float2 uv_Splat3 : TEXCOORD3;
	float2 tc_Control : TEXCOORD4;	// Not prefixing '_Contorl' with 'uv' allows a tighter packing of interpolators, which is necessary to support directional lightmap.
	float4 pos : TEXCOORD5;
	float3 tp;
	float2 texcoord;
	float3 normal;
	UNITY_FOG_COORDS(5)
};

sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _Splat0, _Splat1, _Splat2, _Splat3;
float BlendPower;
float OccludePower;
float2 _Splat0UV, _Splat1UV, _Splat2UV, _Splat3UV;
int doGlobalBelnd = 0;
int doGlobalOcclude = 0;
int UseTriPlanar;
float triPlanerMultiplier = .1;

sampler2D _Normal0, _Normal1, _Normal2, _Normal3;


void SplatmapVert(inout appdata_full v, out Input data)
{
	UNITY_INITIALIZE_OUTPUT(Input, data);

	data.texcoord = v.texcoord;
	data.tc_Control = TRANSFORM_TEX(v.texcoord, _MainTex);	// Need to manually transform uv here, as we choose not to use 'uv' prefix for this texcoord.

	float4 pos = mul(UNITY_MATRIX_MVP, v.vertex);
	data.pos = pos;
	UNITY_TRANSFER_FOG(data, pos);

	data.tp = v.vertex.xyz;
	data.normal = v.normal;

	v.tangent.xyz = cross(v.normal, float3(0, 0, 1));
	v.tangent.w = -1;


}

void SplatmapMix(Input IN, half4 defaultAlpha, out half4 splat_control, out half weight, out fixed4 mixedDiffuse, inout fixed3 mixedNormal, float4 offsetMin, float4 offsetMax, float4 normalMod, float depth)
{
	splat_control = tex2D(_MainTex, IN.tc_Control);
	weight = dot(splat_control, half4(1, 1, 1, 1));

#ifndef UNITY_PASS_DEFERRED
	// Normalize weights before lighting and restore weights in applyWeights function so that the overal
	// lighting result can be correctly weighted.
	// In G-Buffer pass we don't need to do it if Additive blending is enabled.
	// TODO: Normal blending in G-buffer pass...
	splat_control /= (weight + 1e-3f); // avoid NaNs in splat_control
#endif

#if !defined(SHADER_API_MOBILE) && defined(TERRAIN_SPLAT_ADDPASS)
	clip(weight - 0.0039 /*1/255*/);
#endif

	IN.uv_Splat0 *= _Splat0UV;
	IN.uv_Splat1 *= _Splat1UV;
	IN.uv_Splat2 *= _Splat2UV;
	IN.uv_Splat3 *= _Splat3UV;

	mixedDiffuse = 0.0f;
	float2 splat0uv = IN.uv_Splat0;
	float2 splat1uv = IN.uv_Splat1;
	float2 splat2uv = IN.uv_Splat2;
	float2 splat3uv = IN.uv_Splat3;

	float2 splat0uvMax = IN.uv_Splat0;
	float2 splat1uvMax = IN.uv_Splat1;
	float2 splat2uvMax = IN.uv_Splat2;
	float2 splat3uvMax = IN.uv_Splat3;

	depth *= UVCutoff;

	splat0uvMax *= offsetMax.r;
	splat1uvMax *= offsetMax.g;
	splat2uvMax *= offsetMax.b;
	splat3uvMax *= offsetMax.a;

	splat0uv *= offsetMin.r;
	splat1uv *= offsetMin.g;
	splat2uv *= offsetMin.b;
	splat3uv *= offsetMin.a;


	float4x4 col;

	half3 blend = abs(IN.normal);
	blend /= dot(blend, 1.0);
	IN.tp.xyz *= triPlanerMultiplier;

	float4x4 cxs;
	float4x4 cys;
	float4x4 czs;
	float4x4 cs;

	if (UseTriPlanar)
	{
		cxs[0] = lerp(tex2D(_Splat0, IN.tp.yz * offsetMax.r), tex2D(_Splat0, IN.tp.yz * offsetMin.r), depth);
		cxs[1] = lerp(tex2D(_Splat1, IN.tp.yz * offsetMax.g), tex2D(_Splat1, IN.tp.yz * offsetMin.g), depth);
		cxs[2] = lerp(tex2D(_Splat2, IN.tp.yz * offsetMax.b), tex2D(_Splat2, IN.tp.yz * offsetMin.b), depth);
		cxs[3] = lerp(tex2D(_Splat3, IN.tp.yz * offsetMax.a), tex2D(_Splat3, IN.tp.yz * offsetMin.a), depth);

		cys[0] = lerp(tex2D(_Splat0, IN.tp.xz * offsetMax.r), tex2D(_Splat0, IN.tp.xz * offsetMin.r), depth);
		cys[1] = lerp(tex2D(_Splat1, IN.tp.xz * offsetMax.g), tex2D(_Splat1, IN.tp.xz * offsetMin.g), depth);
		cys[2] = lerp(tex2D(_Splat2, IN.tp.xz * offsetMax.b), tex2D(_Splat2, IN.tp.xz * offsetMin.b), depth);
		cys[3] = lerp(tex2D(_Splat3, IN.tp.xz * offsetMax.a), tex2D(_Splat3, IN.tp.xz * offsetMin.a), depth);

		czs[0] = lerp(tex2D(_Splat0, IN.tp.xy * offsetMax.r), tex2D(_Splat0, IN.tp.xy * offsetMin.r), depth);
		czs[1] = lerp(tex2D(_Splat1, IN.tp.xy * offsetMax.g), tex2D(_Splat1, IN.tp.xy * offsetMin.g), depth);
		czs[2] = lerp(tex2D(_Splat2, IN.tp.xy * offsetMax.b), tex2D(_Splat2, IN.tp.xy * offsetMin.b), depth);
		czs[3] = lerp(tex2D(_Splat3, IN.tp.xy * offsetMax.a), tex2D(_Splat3, IN.tp.xy * offsetMin.a), depth);

		
		cs[0] = cxs[0] * blend.x + cys[0] * blend.y + czs[0] * blend.z;
		cs[1] = cxs[1] * blend.x + cys[1] * blend.y + czs[1] * blend.z;
		cs[2] = cxs[2] * blend.x + cys[2] * blend.y + czs[2] * blend.z;
		cs[3] = cxs[3] * blend.x + cys[3] * blend.y + czs[3] * blend.z;

		col[0] = cs[0] * half4(1.0, 1.0, 1.0, defaultAlpha.r) * _Brightness.r;
		col[1] = cs[1] * half4(1.0, 1.0, 1.0, defaultAlpha.g) * _Brightness.g;
		col[2] = cs[2] * half4(1.0, 1.0, 1.0, defaultAlpha.b) * _Brightness.b;
		col[3] = cs[3] * half4(1.0, 1.0, 1.0, defaultAlpha.a) * _Brightness.a;
	}
	else
	{
		col[0] = lerp(tex2D(_Splat0, splat0uvMax), tex2D(_Splat0, splat0uv), depth) * half4(1.0, 1.0, 1.0, defaultAlpha.r) * _Brightness.r;
		col[1] = lerp(tex2D(_Splat1, splat1uvMax), tex2D(_Splat1, splat1uv), depth) * half4(1.0, 1.0, 1.0, defaultAlpha.g) * _Brightness.g;
		col[2] = lerp(tex2D(_Splat2, splat2uvMax), tex2D(_Splat2, splat2uv), depth) * half4(1.0, 1.0, 1.0, defaultAlpha.b) * _Brightness.b;
		col[3] = lerp(tex2D(_Splat3, splat3uvMax), tex2D(_Splat3, splat3uv), depth) * half4(1.0, 1.0, 1.0, defaultAlpha.a) * _Brightness.a;
	}

	
	float4 sc = mul(splat_control, col);

	if (doGlobalBelnd)
	{
		float4 c = tex2D(GlobalBlend, IN.texcoord);
		sc.rgb = lerp(sc.rgb, sc.rgb * c.rgb, c.a * BlendPower);
	}
		

	mixedDiffuse = sc;
	

	fixed4 nrm = 0.0f;

	float4x4 norm;

	
	if (UseTriPlanar)
	{
		float4x4 cxs;
		float4x4 cys;
		float4x4 czs;

		cxs[0] = lerp(tex2D(_Normal0, IN.tp.yz * offsetMax.r), tex2D(_Normal0, IN.tp.yz * offsetMin.r), depth);
		cxs[1] = lerp(tex2D(_Normal1, IN.tp.yz * offsetMax.g), tex2D(_Normal1, IN.tp.yz * offsetMin.g), depth);
		cxs[2] = lerp(tex2D(_Normal2, IN.tp.yz * offsetMax.b), tex2D(_Normal2, IN.tp.yz * offsetMin.b), depth);
		cxs[3] = lerp(tex2D(_Normal3, IN.tp.yz * offsetMax.a), tex2D(_Normal3, IN.tp.yz * offsetMin.a), depth);

		cys[0] = lerp(tex2D(_Normal0, IN.tp.xz * offsetMax.r), tex2D(_Normal0, IN.tp.xz * offsetMin.r), depth);
		cys[1] = lerp(tex2D(_Normal1, IN.tp.xz * offsetMax.g), tex2D(_Normal1, IN.tp.xz * offsetMin.g), depth);
		cys[2] = lerp(tex2D(_Normal2, IN.tp.xz * offsetMax.b), tex2D(_Normal2, IN.tp.xz * offsetMin.b), depth);
		cys[3] = lerp(tex2D(_Normal3, IN.tp.xz * offsetMax.a), tex2D(_Normal3, IN.tp.xz * offsetMin.a), depth);

		czs[0] = lerp(tex2D(_Normal0, IN.tp.xy * offsetMax.r), tex2D(_Normal0, IN.tp.xy * offsetMin.r), depth);
		czs[1] = lerp(tex2D(_Normal1, IN.tp.xy * offsetMax.g), tex2D(_Normal1, IN.tp.xy * offsetMin.g), depth);
		czs[2] = lerp(tex2D(_Normal2, IN.tp.xy * offsetMax.b), tex2D(_Normal2, IN.tp.xy * offsetMin.b), depth);
		czs[3] = lerp(tex2D(_Normal3, IN.tp.xy * offsetMax.a), tex2D(_Normal3, IN.tp.xy * offsetMin.a), depth);

		float4x4 cs;
		cs[0] = cxs[0] * blend.x + cys[0] * blend.y + czs[0] * blend.z;
		cs[1] = cxs[1] * blend.x + cys[1] * blend.y + czs[1] * blend.z;
		cs[2] = cxs[2] * blend.x + cys[2] * blend.y + czs[2] * blend.z;
		cs[3] = cxs[3] * blend.x + cys[3] * blend.y + czs[3] * blend.z;

		norm[0] = cs[0] * normalMod.r;
		norm[1] = cs[1] * normalMod.g;
		norm[2] = cs[2] * normalMod.b;
		norm[3] = cs[3] * normalMod.a;
	}
	else
	{
		norm[0] = lerp(tex2D(_Normal0, splat0uvMax), tex2D(_Normal0, splat0uv), depth) * normalMod.r;
		norm[1] = lerp(tex2D(_Normal1, splat1uvMax), tex2D(_Normal1, splat1uv), depth) * normalMod.g;
		norm[2] = lerp(tex2D(_Normal2, splat2uvMax), tex2D(_Normal2, splat2uv), depth) * normalMod.b;
		norm[3] = lerp(tex2D(_Normal3, splat3uvMax), tex2D(_Normal3, splat3uv), depth) * normalMod.a;
	}

	
	nrm = mul(splat_control, norm);

	mixedNormal = UnpackNormal(nrm);
}

void SplatmapApplyWeight(inout fixed4 color, fixed weight)
{
	color.rgb *= weight;
	color.a = 1.0f;
}

void SplatmapApplyFog(inout fixed4 color, Input IN)
{
	UNITY_APPLY_FOG(IN.fogCoord, color);
}
