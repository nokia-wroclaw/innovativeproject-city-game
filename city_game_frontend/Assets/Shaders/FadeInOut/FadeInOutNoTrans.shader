Shader "Custom/FadeInOutNoTrans" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_DissolveTex("Dissolve Texture", 2D) = "white" {}
		_Level("Dissolution level[test]", Range(-1.0, 2.0)) = 0.1
		_Edges("Edge width", Range(0.0, 1.0)) = 0.1
		_EdgeColor("Edge color", Color) = (1,0,0,1)
		_StartTime("Start time[animation]", Float) = 0
		_Duration("Duration[a]", Float) = 10
		_Direction("Direction[a]", Float) = 0
			//this params define pixels which emit emission
			//if pixel's color is located between H and L then
			//it will emit emission. Params need to be 
			//determined experimentally
		_EmmisionColorH("EmmisonBaseH", Color) = (1,1,1,1)
		_EmmisionColorL("EmmisonBaseL", Color) = (1,1,1,1)
			//color of the emision
		_Emission("Emission", Color) = (1,1,1,1)
		
	}

		SubShader{
			Tags{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			LOD 100

			CGPROGRAM
			#pragma surface surf Lambert
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _DissolveTex;

			struct Input {
				float2 uv_MainTex;
				float2 uv_DissolveTex;
			};

			float4 _Color;
			float4 _EmmisionColorH;
			float4 _EmmisionColorL;
			float4 _EdgeColor;
			float4 _Emission;
			float _Level;
			float _Edges;
			float _FadeFromTime;
			float _StartTime;
			float _Duration;
			float _Direction;

			
			void surf(Input IN, inout SurfaceOutput o) {

				
				

				//fade in
				if (_Direction == 1) {
					_Level = (_Time[1] - _StartTime) / _Duration;
				}
				//fade out
				else {
					_Level = 1 - (_Time[1] - _StartTime) / _Duration;
				}

				fixed4 map = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = map * _Color;
				if (_EmmisionColorH.r >= map.r && _EmmisionColorL.r <= map.r &&
					_EmmisionColorH.b >= map.b && _EmmisionColorL.b <= map.b &&
					_EmmisionColorH.g >= map.g && _EmmisionColorL.g <= map.g) {
					o.Emission = _Emission;
				}
				
				fixed4 cutout = tex2D(_DissolveTex, IN.uv_MainTex).g;


				o.Alpha = lerp(float4(1,0,1,1),float4(0,0,1,1), (cutout.a - _Level) / _Edges);
				if (o.Alpha > 0.4 && o.Alpha < 0.6) {
					o.Albedo = _EdgeColor;
					o.Alpha = 0.9;
				}

				if (o.Alpha < 0) {
					o.Alpha = 0;
				}
				else if (o.Alpha > 1) {
					o.Alpha = 1;
				}

			}
		ENDCG
		}
	FallBack "Transparent"
}