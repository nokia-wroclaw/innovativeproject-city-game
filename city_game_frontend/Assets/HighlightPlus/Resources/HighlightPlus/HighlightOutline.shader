Shader "HighlightPlus/Geometry/Outline" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    _OutlineWidth ("Outline Offset", Float) = 0.01
}
    SubShader
    {
        Tags { "Queue"="Transparent+120" "RenderType"="Transparent" }

        // Black outline
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
//            ZTest Always
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
                float4 vertex : SV_POSITION;
            };

            fixed4 _OutlineColor;
            float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 offset = TransformViewToProjection(normalize(norm.xy));
				float z = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);
				o.vertex.xy += offset * z * _OutlineWidth;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
  		      fixed4 col = _OutlineColor;
  		      return col;
            }
            ENDCG
        }

    }
}