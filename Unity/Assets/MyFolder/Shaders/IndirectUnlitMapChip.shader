Shader "Unlit/IndirectUnlitMapChip"
{
    Properties
    {
        _MainTex ("Tex", 2DArray) = "" {}
        _WidthCountShift ("WidthCountShift", Int) = 8
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
            #pragma require 2darray
            
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

            UNITY_DECLARE_TEX2DARRAY(_MainTex);
            StructuredBuffer<uint> kinds;
            uint _WidthCountShift;
            float _AdjustPosition;
            float _ChipSize;
            
            v2f vert (appdata v, const uint id : SV_INSTANCEID)
            {
                const uint y = id >> _WidthCountShift;
                const uint x = id ^ (y << _WidthCountShift);
                v.vertex.x += x;
                v.vertex.y += y;
                v.vertex.xy -= 1 << (_WidthCountShift - 1);
                v.vertex.xy *= _ChipSize;
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = v.uv;
                o.uv.z = kinds[id];
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return UNITY_SAMPLE_TEX2DARRAY(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
