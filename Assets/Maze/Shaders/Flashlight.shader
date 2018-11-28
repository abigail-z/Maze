Shader "Custom/Flashlight"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
		_ShadowColor("ShadowColor", Color) = (0,0,0,1)
		_CircleScreenPercent("Radius", Range(0,1)) = 0.5
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
				float4 pos : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			fixed4 _ShadowColor;
			float _CircleScreenPercent;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * _Color;
				float dist = distance(i.pos.xy, _ScreenParams.xy / 2);
				float lightSize = _ScreenParams.y * _CircleScreenPercent;

				return lerp(col, _ShadowColor, saturate(dist / lightSize));
			}
				ENDCG
		}
	}
}
