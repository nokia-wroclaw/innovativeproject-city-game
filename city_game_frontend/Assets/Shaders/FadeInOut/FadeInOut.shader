Shader "Custom/FadeInOut" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_DissolveTex("Dissolve Texture", 2D) = "white" {}
		_Level("Dissolution level", Range(0.0, 1.0)) = 0.1
		_EdgeColour1("Edge colour 1", Color) = (1.0, 1.0, 1.0, 1.0)
		_EdgeColour2("Edge colour 2", Color) = (1.0, 1.0, 1.0, 1.0)
		_Edges("Edge width", Range(0.0, 1.0)) = 0.1
		_FadeFromTime("Fade from time? set 1", Range(0.0, 1.0)) = 1

		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}

		SubShader{
			Tags{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			LOD 100

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows alpha:blend
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _DissolveTex;

			struct Input {
				float2 uv_MainTex;
				float2 uv_DissolveTex;
			};

			float4 _Color;
			half _Glossiness;
			half _Metallic;
			float4 _EdgeColour1;
			float4 _EdgeColour2;
			float _Level;
			float _Edges;
			float _FadeFromTime;

			
			void surf(Input IN, inout SurfaceOutputStandard o) {

				
				if (_FadeFromTime == 1) {
					_Level = abs(sin(_Time[1]));
				}

				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				fixed4 cutout = tex2D(_DissolveTex, IN.uv_MainTex).g;

				o.Albedo = c.rgb;

				


				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				if (_Level == 0) {
					o.Alpha = 0;
				}
				else {
					o.Alpha = lerp(_EdgeColour1, _EdgeColour2, (cutout.a - _Level) / _Edges);
				}
			}
		ENDCG
		}
	FallBack "Transparent"
}