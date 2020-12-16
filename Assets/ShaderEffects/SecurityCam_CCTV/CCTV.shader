Shader "Hidden/CCTV"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _Lines;
            float _ClampValue;
            float _AnimatedValue;
            float4 _FootageColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 lines = lerp(_ClampValue, 1, tex2D(_Lines, i.uv));
                fixed4 linesAnimated = lerp(_ClampValue, 1, tex2D(_Lines, i.uv + _AnimatedValue));
                fixed4 finalLines = lerp(_FootageColor, 1, lines + linesAnimated);
                return col * finalLines;
            }
            ENDCG
        }
    }
}
