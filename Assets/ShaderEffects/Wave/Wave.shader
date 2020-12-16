Shader "Hidden/Wave"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float time;
			float waveX;
			float waveY;
			float uvScaleX;
			float uvScaleY;

			fixed4 frag(v2f i) : SV_Target
			{
				float sinWave = sin(i.uv.y * waveY + time) + sin(i.uv.x * waveX + time);
				float2 waveUV = float2(i.uv.x + sinWave * uvScaleX, i.uv.y + sinWave * uvScaleY);
				fixed4 colSin = tex2D(_MainTex, waveUV);
				return colSin;
			}
			ENDCG
		}
	}
}
