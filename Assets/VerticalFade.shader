Shader "Custom/MagicMugSmoothFixed"
{
    Properties
    {
        _Fade("Heat Amount", Range(0,1)) = 0
        _EdgeSmooth("Edge Smooth", Range(0.001,0.3)) = 0.1
    }

        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        // Configuración de transparencia
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Input de vértices
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

    // Datos que van al fragment shader
    struct v2f
    {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
    };

    // Propiedades
    float _Fade;
    float _EdgeSmooth;

    // Vertex shader simple
    v2f vert(appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
    }

    // Fragment shader
    fixed4 frag(v2f i) : SV_Target
    {
        float y = 1 - i.uv.y;

    // Calcula la interpolación del fade
    float alphaFade = smoothstep(_Fade, _Fade + _EdgeSmooth, y);

    // Zona negra sólida
    if (y < _Fade)
    {
        return fixed4(0, 0, 0, 1);
    }

    // Zona de fade suave
    if (y < _Fade + _EdgeSmooth)
    {
        return fixed4(0, 0, 0, 1 - alphaFade);
    }

    // Zona transparente arriba
    return fixed4(0, 0, 0, 0);
}
ENDCG
}
    }
}