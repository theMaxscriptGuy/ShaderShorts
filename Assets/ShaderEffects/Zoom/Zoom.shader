Shader "Hidden/Zoom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Zoom ("Zoom", Float) = 1.0
        _Exp("Exp", Float) = 1.0
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 grabPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _BackgroundTexture;
            float _Zoom;
            float _Exp;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                float4 modVertex = o.vertex;
                modVertex.w += _Zoom;
                o.grabPos = ComputeGrabScreenPos(modVertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
                half4 col = tex2D(_MainTex, i.uv);
                return pow(col, _Exp) * bgcolor;
            }
            ENDCG
        }
        
    }
}
