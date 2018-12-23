Shader "HighlightPlus/Geometry/Overlay" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _Color ("Color", Color) = (1,1,1,1)
    _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    _OutlineWidth ("Outline Offset", Float) = 0.01
    _Glow ("Glow", Vector) = (1, 0.025, 0.75, 0.5)
    _Glow2 ("Glow2", Vector) = (0.01, 1, 0)
    _GlowColor ("Glow Color", Color) = (1,1,1)
    _OverlayBackColor ("Overlay Back Color", Color) = (1,1,1,1)
    _OverlaySpeed("Overlay Speed", Float) = 1
    _SeeThrough ("See Through", Range(0,1)) = 0.8
    _SeeThroughTintColor ("See Through Tint Color", Color) = (1,0,0)
}
    SubShader
    {
        Tags { "Queue"="Transparent+121" "RenderType"="Transparent" }
    
        // Overlay
        Pass
        {
           	Stencil {
                Ref 2
                Comp Equal
                Pass keep 
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Offset -1, -1
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
            };

      		fixed4 _Color;
      		sampler2D _MainTex;
      		float4 _MainTex_ST;
      		fixed4 _OverlayBackColor;
      		fixed _OverlaySpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX (v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
				fixed t = 0.5 + abs(0.5 - frac(_Time.y * _OverlaySpeed));
                fixed4 col = tex2D(_MainTex, i.uv) * _OverlayBackColor * _Color;
                col.a *= t;
				return col;
            }
            ENDCG
        }

    }
}