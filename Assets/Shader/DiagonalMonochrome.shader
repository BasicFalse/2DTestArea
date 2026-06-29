Shader "UI/Monochrome"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (1,1,1,1)
        _GapColor ("Gap Color", Color) = (0,0,0,1)
        _LineWidth ("Line Width", Float) = 0.1
        _LineSpacing ("Line Spacing", Float) = 10.0
        _BlurAmount ("Blur Amount", Float) = 0.05
        _Speed ("Scroll Speed", Float) = 1.0
        _AlphaThreshold ("Alpha Threshold", Range(0,1)) = 0.01
        //_LineStartY ("Line Start Y", Float) = -999.0

    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
        }

        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LineColor;
            float4 _GapColor;
            float _LineWidth;
            float _LineSpacing;
            float _BlurAmount;
            float _Speed;
            float _AlphaThreshold;
            //float _LineStartY;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.texcoord);
                if (tex.a < _AlphaThreshold)
                    discard;

                float scroll = _Time.y * _Speed;

                // Use world position XY for pattern (you can swap to XZ if preferred)
                float coord = dot(i.worldPos.xy, float2(1, 1)) + scroll;

                float pattern = frac(coord * _LineSpacing); // Always 0..1, works like positive fmod
                float lineIntensity = 1.0 - smoothstep(
                    0.5 - _LineWidth * 0.5 - _BlurAmount,
                    0.5 + _LineWidth * _BlurAmount,
                    abs(pattern - 0.5)
                );
                // Clamp out areas below the start Y

                //if (i.worldPos.y < _LineStartY)
                //    lineIntensity = 0.0;

                float4 finalColor = lerp(_GapColor, _LineColor, lineIntensity);
                //finalColor.rgb *= tex.a;
                finalColor.a = tex.a;

                return finalColor;
            }
            ENDCG
        }
    }
}
