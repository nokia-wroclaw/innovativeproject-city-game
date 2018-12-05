Shader "Custom/FadeInOutv2" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_DissolveScale("Dissolve Progression", Range(0.0, 1.0)) = 0.0
		_DissolveTex("Dissolve Texture", 2D) = "white" {}
		_GlowIntensity("Glow Intensity", Range(0.0, 5.0)) = 0.05
		_GlowScale("Glow Size", Range(0.0, 5.0)) = 1.0
		_Glow("Glow Color", Color) = (1, 1, 1, 1)
		_GlowEnd("Glow End Color", Color) = (1, 1, 1, 1)
		_GlowColFac("Glow Colorshift", Range(0.01, 2.0)) = 0.75
		_DissolveStart("Dissolve Start Point", Vector) = (1, 1, 1, 1)
		_DissolveEnd("Dissolve End Point", Vector) = (0, 0, 0, 1)
		_DissolveBand("Dissolve Band Size", Float) = 0.25
	}

	SubShader{
		Tags{
		"Queue" = "Transparent"
		"RenderType" = "Transparent"
		}

		LOD 100

		CGPROGRAM
		#pragma surface surf Standard vertex:vert alpha
		#pragma target 3.0


		float4 _Color;

		sampler2D _MainTex;
		sampler2D _DissolveTex;

		float _DissolveScale;

		float _DissolveBand;
		float3 _DissolveStart;
		float3 _DissolveEnd;

		float _GlowScale;
		float4 _Glow;
		float4 _GlowEnd;
		float _GlowColFac;
		float _GlowIntensity;
		//Precompute dissolve direction.
		static float3 dDir = normalize(_DissolveEnd - _DissolveStart);

		//Precompute gradient start position.
		static float3 dissolveStartConverted = _DissolveStart - _DissolveBand * dDir;

		//Precompute reciprocal of band size.
		static float dBandFactor = 1.0f / _DissolveBand;

		struct Input {
			float2 uv_MainTex;
			float2 uv_DissolveTex;
			float dGeometry;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

			//Calculate geometry-based dissolve coefficient.
			//Compute top of dissolution gradient according to dissolve progression.
			float3 dPoint = lerp(dissolveStartConverted, _DissolveEnd, _DissolveScale);

			//Project vector between current vertex and top of gradient onto dissolve direction.
			//Scale coefficient by band (gradient) size.
			o.dGeometry = dot(v.vertex - dPoint, dDir) * dBandFactor;
		}

		void surf(Input IN, inout SurfaceOutputStandard o) {
			
			/*Alpha processing*/
			//Convert dissolve progression to -1 to 1 scale.
			half dBase = -2.0f * _DissolveScale + 1.0f;

			//Read from noise texture.
			fixed4 dTex = tex2D(_DissolveTex, IN.uv_MainTex);
			//Convert dissolve texture sample based on dissolve progression.
			half dTexRead = dTex.r + dBase;

			//Combine texture factor with geometry coefficient from vertex routine.
			half dFinal = dTexRead + IN.dGeometry;

			//Clamp and set alpha.
			half alpha = clamp(dFinal, 0.0f, 1.0f);
			o.Alpha = alpha;

			/*Albedo processing*/

			//Shift the computed raw alpha value based on the scale factor of the glow.
			//Scale the shifted value based on effect intensity.
			half dPredict = (_GlowScale - dFinal) * _GlowIntensity;
			//Change colour interpolation by adding in another factor controlling the gradient.
			half dPredictCol = (_GlowScale * _GlowColFac - dFinal) * _GlowIntensity;

			//Calculate and clamp glow colour.
			fixed4 glowCol = dPredict * lerp(_Glow, _GlowEnd, clamp(dPredictCol, 0.0f, 1.0f));
			glowCol = clamp(glowCol, 0.0f, 1.0f);

			o.Emission = glowCol;
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		}

	
		ENDCG
	}
	FallBack "Transparent"
}