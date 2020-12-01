Shader "Unlit/IndirectUnlitMapChip"
{
    Properties
    {
        _MainTex ("Tex", 2DArray) = "" {}
        _WidthCountShift ("WidthCountShift", Int) = 8
        _AdjustPosition ("AdjustPosition", Float) = 0
        _ChipSize ("ChipSize", Float) = 32
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float3 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            StructuredBuffer<uint> kinds;
            uint _WidthCountShift;
            float _AdjustPosition;
            float _ChipSize;
            
            v2f vert (appdata v, const uint id : SV_INSTANCEID)
            {
                const uint y = id >> _WidthCountShift;
                const uint x = id ^ (y << _WidthCountShift);
                v.vertex.xy = mad(float2(x, y), _ChipSize, v.vertex.xy + _AdjustPosition);
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = float3(v.uv, kinds[id]);
                return o;
            }

            UNITY_DECLARE_TEX2DARRAY(_MainTex);
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                return UNITY_SAMPLE_TEX2DARRAY(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
