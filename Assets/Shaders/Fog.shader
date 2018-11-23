Shader "Custom/Fog"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
		_FogColor("FogColor", Color) = (1,1,1,1)
		_Radius("Radius", Range(0.001, 500)) = 25
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				float4 pos : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float4 _FogColor;
			float _Radius;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float dist = distance(i.worldPos, _WorldSpaceCameraPos);
				fixed4 col = tex2D(_MainTex, i.uv);
				col = lerp(col * _Color, _FogColor, dist / _Radius);

				return col;
			}
				ENDCG
		}
	}
}
