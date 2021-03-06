﻿Shader "Hidden/DepthLight"
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
            sampler2D _CameraDepthTexture;
            float _XOffset;
            float _YOffset;
            float _Exp;
            float4 _VignetteColor;

            fixed4 frag (v2f i) : SV_Target
            {
                float radial = sin(i.uv.x + _XOffset) * cos(i.uv.y + _YOffset);
                radial = pow(radial, _Exp);

                fixed4 col = tex2D(_MainTex, i.uv);
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
                return lerp(depth, _VignetteColor + col*_VignetteColor * pow(1.0 - depth, _Exp*2), radial);
            }
            ENDCG
        }
    }
}
