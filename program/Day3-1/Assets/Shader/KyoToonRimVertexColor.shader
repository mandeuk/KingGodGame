Shader "KyoToon/KyoToonRimVertexColor"
{
	Properties {
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_outLineThickness ("outLine thickness", Range(0,1)) = 0.01
		_outLineColor ("Out Line Color", Color) = (0,0,0,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RampTex ("RampTex", 2D) = "white" {}
		_shadow1Color("shadow 1 Color", Color) = (0,0,0,0)
		_shadow2Color("shadow 2 Color", Color) = (0,0,0,0)
		_shadow1Range("shadow 1 Range", Range(0,1)) = 0.6
		_shadow2Range("shadow 2 Range", Range(0,1)) = 0.3
		_rimPow("rim Pow", Range(0,10)) = 0
		_rimColor("rim Color", Color) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		cull front

		CGPROGRAM
		#pragma surface surf Nolight vertex:vert noshadow noambient

		sampler2D _MainTex;
		float _outLineThickness = 0.01;
		float4 _outLineColor;

		void vert (inout appdata_full v)
		{
			v.vertex.xyz = v.vertex + (v.normal.xyz * (_outLineThickness * v.color.g));
		}

		struct Input{
			float4 color:COLOR;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{

		}

		float4 LightingNolight(SurfaceOutput s, float3 ligthDir, float atten)
		{
			return _outLineColor;
		}
		ENDCG

		cull back
		CGPROGRAM
		#pragma surface surf warp noambient

		sampler2D _MainTex;
		sampler2D _RampTex;

		fixed4 _shadow1Color;
		fixed4 _shadow2Color;

		float _shadow1Range;
		float _shadow2Range;

		float4 _Color;

		float _rimPow;
		float3 _rimColor;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex)* _Color;
			//o.Normal = UnpackNormal(tex2D (_BumpMap,IN.uv_BumpMap));
			o.Albedo = c.rgb;
			o.Alpha = c.a;

			float rim =  dot(o.Normal, IN.viewDir);
			o.Emission = saturate(pow (1-rim, _rimPow)) * _rimColor.rgb;

			o.Alpha = c.a;
		}

		float4 Lightingwarp (SurfaceOutput s, float3 lightDir, float viewDir, float attaen)
		{
			float rim = abs(dot(s.Normal, viewDir)) * 1;
			float3 H = normalize(lightDir + viewDir);
			float spec = saturate(dot(s.Normal, H));

			float ndot1 = dot(s.Normal, lightDir) * 0.5 + 0.5;

			float4 shadowColor;

			if(ndot1 <_shadow2Range)
				shadowColor = _shadow2Color;
			else if(ndot1 <_shadow1Range)
				shadowColor = _shadow1Color;
			else 
				shadowColor = float4(1,1,1,1);

			float4 redColor = float4(1,0,0,1);
				
//			스페큘러 넣을 경우
//			if(spec > 0.97 )
//			{
//					shadowColor = float4(1,0,0,1);
//			}
//			else
//			{
//				if(ndot1 <_shadow2Range)
//					shadowColor = _shadow2Color;
//				else if(ndot1 <_shadow1Range)
//					shadowColor = _shadow1Color;
//				else 
//					shadowColor = float4(1,1,1,1);
//			}
			float4 result;

			if(spec > 0.97 )
				result.rgb = (s.Albedo.rgb * shadowColor.rgb )+ (shadowColor.rgb * 0.1);
			else
				result.rgb = (s.Albedo.rgb * shadowColor.rgb )+ (shadowColor.rgb * 0.05);
			//result.rgb = (s.Albedo.rgb * shadowColor.rgb )+ ((redColor.rgb) );
			//result.rgb = (s.Albedo.rgb * shadowColor.rgb )+ (shadowColor.rgb * 0.1);
			result.a = s.Alpha;

			return result;
		}
		ENDCG
	}
	FallBack "Diffuse"
}