Shader "Custom/ThicShader" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_ThicTex("Texture", 2D) = "black" {}
		_Amount("Extrusion Amount", Range(-1,1)) = 0.5
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		#pragma target 3.0
		#pragma glsl
		struct Input 
		{
			float2 uv_MainTex;
		};
		sampler2D _MainTex;
		sampler2D _ThicTex;

	float _Amount;
	void vert(inout appdata_tan v) 
	{
		v.vertex.xyz += v.normal * tex2Dlod(_ThicTex, float4(v.texcoord.xyz, 0)) * _Amount;
	}
	
	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
	}
	ENDCG
	}
		Fallback "Diffuse"
}
