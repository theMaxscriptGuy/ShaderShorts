﻿Shader "Hidden/SecureCam"
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
            sampler2D _CamTextureA;
            half4 _CamLinesColor;
            half _Clamp;
            half _TimeOffset;
            half _UVOffset;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 camLines = lerp(_Clamp, 1, tex2D(_CamTextureA, i.uv) / _UVOffset);
                fixed4 camLines2 = lerp(_Clamp, 1, tex2D(_CamTextureA, i.uv + _TimeOffset) / _UVOffset);
                fixed4 finalCol = lerp(_CamLinesColor, 1, (camLines + camLines2));
                return col * finalCol;
            }
            ENDCG
        }
    }
}
