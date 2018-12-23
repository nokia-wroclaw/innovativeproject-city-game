Shader "HighlightPlus/Geometry/Mask" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
}
    SubShader
    {
        Tags { "Queue"="Transparent+100" "RenderType"="Transparent" }

        // Create mask
        Pass
        {
			Stencil {
                Ref 2
                Comp always
                Pass replace
            }
            ColorMask 0
            ZWrite Off
            Offset -1, -1
            Cull Off
//            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
            };

            void vert (appdata v, out float4 pos : SV_POSITION)
            {
                pos = UnityObjectToClipPos(v.vertex);
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
            	return 0;
            }
            ENDCG
        }

    }
}