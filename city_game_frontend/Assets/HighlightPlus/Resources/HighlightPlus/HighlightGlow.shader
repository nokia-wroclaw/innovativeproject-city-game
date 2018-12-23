Shader "HighlightPlus/Geometry/Glow" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _Glow ("Glow", Vector) = (1, 0.025, 0.75, 0.5)
    _Glow2 ("Glow2", Vector) = (0.01, 1, 0.5, 0)
    _GlowColor ("Glow Color", Color) = (1,1,1)
}
    SubShader
    {
        Tags { "Queue"="Transparent+102" "RenderType"="Transparent" }
      
        // Glow passes
        Pass
        {
        	Stencil {
                Ref 2
                Comp NotEqual
                Pass keep 
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                fixed4 color : COLOR;
            };

            float4 _Glow; // x = intensity, y = width, z = magic number 1, w = magic number 2
            float3 _Glow2; // x = outline width, y = glow speed, z = dither on/off
            fixed4 _GlowColor;

            v2f vert (appdata v, out float4 pos : SV_POSITION)
            {
                v2f o;
                pos = UnityObjectToClipPos(v.vertex);
                float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(normalize(norm.xy));
                float z = UNITY_Z_0_FAR_FROM_CLIPSPACE(pos.z);
                offset *= z * (_Glow2.x + _Glow.y * (1.0 + 0.25 * sin(_Time.w * _Glow2.y)));
                pos.xy += offset;
                o.color = _GlowColor;
                o.color.a = _Glow.x;
                return o;
            }
            
            fixed4 frag (v2f i, UNITY_VPOS_TYPE scrPos : VPOS) : SV_Target
            {
                fixed4 color = i.color;
                float2 screenPos = floor(scrPos.xy * _Glow.z) * _Glow.w;
                color.a *= saturate(_Glow2.z + frac(screenPos.x + screenPos.y));
                return color;
            }
            ENDCG
        }
 
    }
}