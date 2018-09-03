Shader "Custom/OutlineShader"
{
	Properties
	{
		_MainColor("Main Color", Color) = (1,1,1,1)
		_TintColor("Tint Color", Color) = (1,1,1,.5)
		_TintStrength("Tint Strength", Range(0,1)) = 1
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineSize("Outline Size", Range(0,.5)) = .1
		_MainTex("Main Texture", 2D) = "white" {}
	}

	SubShader
	{
		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD;
				float4 color : COLOR;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				float R = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				o.color = R * _LightColor0;
				o.color.rgb += ShadeSH9(float4(worldNormal, 1));
				return o;
			}

			sampler2D _MainTex;
			float4 _TintColor;
			float4 _MainColor;
			float _TintStrength;

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);
				color *= i.color;
				color *= _MainColor + (_TintColor * _TintStrength);
				return color;
			}
			ENDCG
		}

		Pass
		{
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			uniform float4 _OutlineColor;
			uniform float _OutlineSize;

			v2f vert(appdata v)
			{
				v.vertex.xyz += _OutlineSize * normalize(v.vertex.xyz);
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = _OutlineColor;
				return o;
			}

			float4 frag(v2f i) :COLOR
			{
				return i.color;
			}
			ENDCG
		}
	}
	CustomEditor "ShaderViewer"
}