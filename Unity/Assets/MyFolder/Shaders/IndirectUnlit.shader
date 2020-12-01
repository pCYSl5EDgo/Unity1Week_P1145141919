Shader "Unlit/IndirectUnlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct Position2D
            {
                float4x4 xy;
            };

            StructuredBuffer<Position2D> positions;

            v2f vert (appdata v, const uint id : SV_INSTANCEID)
            {
                const Position2D position = positions[id >> 3];
                const uint big_index = (id >> 2) & 1;
                const uint small_index = id & 3;
                v.vertex.x += position.xy[big_index][small_index];
                v.vertex.y += position.xy[2 + big_index][small_index];
                
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}