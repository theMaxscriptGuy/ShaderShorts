Shader "Unlit/Screenspace"
{
    Properties{
_MainTex("Color Texture", 2D) = "white" {}
_SSUVScale("UV Scale", Range(0,10)) = 1
    }

        CGINCLUDE
    sampler2D _MainTex;
float _SSUVScale;

struct appdata {
    float4 vertex : POSITION;
};

struct v2f {
    float4 pos : POSITION;
    float4 pos2: TEXCOORD0;
};



float2 GetScreenUV(float2 clipPos, float UVscaleFactor)
{
    float4 SSobjectPosition = UnityObjectToClipPos(float4(0, 0, 0, 1.0));
    float2 screenUV = float2(clipPos.x, clipPos.y);
    float screenRatio = _ScreenParams.y / _ScreenParams.x;

    screenUV.x -= SSobjectPosition.x / (SSobjectPosition.w);
    screenUV.y -= SSobjectPosition.y / (SSobjectPosition.w);

    screenUV.y *= screenRatio;

    screenUV *= 1 / UVscaleFactor;
    screenUV *= SSobjectPosition.z;

    return screenUV;
};




ENDCG

SubShader{
      Tags { "RenderType" = "Opaque"
            "DisableBatching" = "True"
     }

      Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        v2f vert(appdata v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.pos2 = o.pos;

            return o;
        }

        half4 frag(v2f i) :COLOR
        {
            float2 screenUV = GetScreenUV(i.pos2.xy / i.pos2.w, _SSUVScale);
            half4 screenTexture = tex2D(_MainTex, screenUV);

            return screenTexture;
        }
        ENDCG
    }

}
}
