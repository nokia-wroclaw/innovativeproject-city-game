Shader "HighlightPlus/Geometry/SeeThrough" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _SeeThrough ("See Through", Range(0,1)) = 0.8
    _SeeThroughTintColor ("See Through Tint Color", Color) = (1,0,0,0.8)
}
    SubShader
    {
        Tags { "Queue"="Transparent+101" "RenderType"="Transparent" }
   
        // See through effect
        Pass
        {
            Stencil {
                Ref 2
                Comp NotEqual
                Pass Replace 
            }
            ZTest Always
            ZWrite Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

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
                float3 wpos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _SeeThrough;
            fixed4 _SeeThroughTintColor;

            v2f vert (appdata v, out float4 pos : SV_POSITION)
            {
                v2f o;
                pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i, UNITY_VPOS_TYPE scrPos : VPOS) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = lerp(col.rgb, _SeeThroughTintColor.rgb, _SeeThroughTintColor.a);
                col.rgb += frac(scrPos.y * _Time.w) * 0.1;
                col.a = _SeeThrough;
            	col.a *= (scrPos.y % 2) - 1.0;
                return col;
            }
            ENDCG
        }

    }
}