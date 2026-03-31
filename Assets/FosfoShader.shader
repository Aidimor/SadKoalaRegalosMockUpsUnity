Shader "Custom/FosfoShader"
{
    Properties
    {
        _Fade("Heat Amount", Range(0,1)) = 0
        _EdgeSmooth("Edge Smooth", Range(0.001,0.3)) = 0.1

        _BottomColor("Bottom Color", Color) = (0,0,0,1)
        _TopColor("Top Color", Color) = (1,1,1,1)
    }

        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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

            float _Fade;
            float _EdgeSmooth;
            fixed4 _BottomColor;
            fixed4 _TopColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float y = 1 - i.uv.y;

                float t = smoothstep(_Fade, _Fade + _EdgeSmooth, y);

                // 🔽 Color inferior
                if (y < _Fade)
                {
                    return _BottomColor;
                }

                // 🌫️ Transición entre colores
                if (y < _Fade + _EdgeSmooth)
                {
                    return lerp(_BottomColor, _TopColor, t);
                }

                // 🔼 Color superior
                return _TopColor;
            }

            ENDCG
        }
    }
}