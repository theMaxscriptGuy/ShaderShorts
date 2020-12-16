Shader "Hidden/Magic"
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
            half _Angle;
            int _Step;
            half _PingPong;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                half spacing = _Angle / _Step;
                half dots = sin(i.uv.x * spacing) * sin(i.uv.y * spacing + _PingPong);
                half dots2 = sin(i.uv.x * spacing) * sin(i.uv.y * spacing);
                col = col * lerp(dots, dots2, dots);
                return col;
            }
            ENDCG
        }
    }
}
