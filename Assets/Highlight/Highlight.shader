Shader "Unlit/Highlight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        LOD 100
        GrabPass
        {
            "_BackgroundTexture"
        }
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
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 normal : NORMAL;
                float4 grabPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _HighlightColor;
            float _Highlight;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = v.normal;
                //o.vertex = v.vertex + (o.normal * 0.025);
                //o.vertex = UnityObjectToClipPos(o.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                return o;
            }

            fixed4 blur(sampler2D tex, float4 pos)
            {
                fixed4 grabA = tex2Dproj(tex, pos - .1);
                fixed4 grabB = tex2Dproj(tex, pos + .1);
                fixed4 grabC = tex2Dproj(tex, pos - .05);
                fixed4 grabD = tex2Dproj(tex, pos + .05);
                fixed4 grabE = tex2Dproj(tex, pos + .15);
                fixed4 grabF = tex2Dproj(tex, pos - .15);

                return (grabA + grabB + grabC + grabD + grabE + grabF) / 6.0;
            }
            
            sampler2D _BackgroundTexture;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 grab = tex2Dproj(_BackgroundTexture, i.grabPos);
                //return lerp(grab, col * _HighlightColor, _Highlight);
                //return lerp(grab, grab * _HighlightColor, _Highlight);
                //return lerp(grab, blur(_BackgroundTexture, i.grabPos), _Highlight);
                return blur(_BackgroundTexture, i.grabPos);
            }
            ENDCG
        }
    }
}
