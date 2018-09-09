Shader "Custom/Bomb"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_AnimationTime ("Animation Time", float) = 2
		_AnimationFrame ("Animation Frame", int) = 8
	}
	SubShader
	{
		Tags { 
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent" 
		}
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			// 2のべき乗でなければならぬ
			int _AnimationFrame;
			// アニメーション時間(秒)
			float _AnimationTime;

			// StructuredBuffer<float2> _PositionBuffer;
			// StructuredBuffer<float> _StartTimeBuffer;
			
			v2f vert (appdata v, uint id : SV_INSTANCEID)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				// v.vertex.xz = v.vertex.xz + _PositionBuffer[id];
				o.vertex = UnityObjectToClipPos(v.vertex);
				// おら、アニメーションを２秒でやるんだよあくしろよ
				// const float stage = floor((_Time.y - _StartTimeBuffer[id]) * _AnimationFrame / _AnimationTime);
				// o.uv.x = (v.uv.x + stage) / _AnimationFrame;
				// o.uv.y = v.uv.y;
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}
}