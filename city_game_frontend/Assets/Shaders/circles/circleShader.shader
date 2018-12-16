Shader "Custom/circleShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Emission ("Emission", Color) = (1,1,1,1)
		_Border("Border", Range(0,10)) = 1
		_Y("Y", Range(0,10)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert alpha:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float alpha;
			float3 col;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float4 _Emission;
		float _Border;
		float _Y;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.alpha = 0;

			if (v.normal.y == 1) {
				o.alpha = 1;
			}

			/*if (v.vertex.y < _Y)
				o.alpha = _Border;*/

			//o.alpha = v.vertex.y;
		}


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo =  c * _Color;
			o.Emission = _Emission * c;

			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			
		
			o.Alpha = 1 * IN.alpha;
			if (c.r < 0.3 && c.g < 0.3 && c.b < 0.3) {
				o.Alpha = 0;
			}
			//o.Alpha = 0;

			

		
		}
		ENDCG
	}
	FallBack "Diffuse"
}
