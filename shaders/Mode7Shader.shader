﻿Shader "Custom/Mode7Shader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		_H("H", Float) = 0
		_V("V", Float) = 0
		_X0("Pivot X", Float) = 0
		_Y0("Pivot Y", Float) = 0
		_A("A", Float) = 1
		_B("B", Float) = 0
		_C("C", Float) = 0
		_D("D", Float) = 1
	}
	
	SubShader
	{
		Tags
		{
			"PreviewType" = "Plane"
			"Queue" = "Transparent"
		}
		Pass
		{
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			float _H;
			float _V;
			float _X0;
			float _Y0;
			float _A;
			float _B;
			float _C;
			float _D;

			float4 frag(v2f i) : SV_Target
			{
				float x = i.uv.x * 0.25;
				float y = (i.uv.y - 1) * -1 * 7.0 / 32;

				float xi = x + (_H - _X0) / 4;
				float yi = y + (_V - _Y0) / 4;
				
				float2 pixel = float2(
					_A * xi + _B * yi + _X0 / 4,
					_C * xi + _D * yi + _Y0 / 4);

				pixel = float2(pixel.x, (pixel.y - 1) * -1);

				float4 colour = tex2D(_MainTex, pixel);
				return colour;
			}
			ENDCG
		}
	}
}
