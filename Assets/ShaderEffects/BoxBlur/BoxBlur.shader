Shader "Hidden/BoxBlur"
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
            int iterations;
            float _XOffset;
            float _YOffset;
            float _BlurOffset;
            float _Exp;

            fixed4 frag(v2f i) : SV_Target
            {
                float radial = sin(i.uv.x + _XOffset) * cos(i.uv.y + _YOffset);
                radial = pow(radial, _Exp);
                fixed4 col = 0;
                float x;
                float y;
                for (int id = 0; id < iterations; id++)
                {
                    x = i.uv.x + _BlurOffset * id/iterations;
                    y = i.uv.y + _BlurOffset * id / iterations;
                    col += tex2D(_MainTex, float2(x,y));
                }
                fixed4 finalImage = tex2D(_MainTex, i.uv);
                col = col / iterations;
                return pow(lerp(col, finalImage,radial), 1);
            }
            ENDCG
        }
    }
}
