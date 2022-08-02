Shader "Harnam/Tile"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Space(10)]
        _outColor ("OutColor", Color) = (0, 0, 0, 1)
        _outValue ("OutValue", Range(0.0, 0.2)) = 0.1
    }
    SubShader
    {
        //outlinepass
        Pass
        {
            Tags {
                "Queue" = "Transparent"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
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
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _outColor;
            float _outValue;

            float4 vertexpos(float4 pos, float outval){
                float4x4 scale = float4x4(
                    1 + outval, 0, 0, 0,
                    0, 1 + outval, 0, 0,
                    0, 0, 1 + outval, 0,
                    0, 0, 0, 1 + outval
                );
                return mul(scale, pos);
            }

            v2f vert (appdata v)
            {
                v2f o;
                float4 vertexPos = vertexpos(v.vertex, _outValue);
                o.vertex = UnityObjectToClipPos(vertexPos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return float4(_outColor.r, _outColor.g, _outColor.b, col.a);
            }
            ENDCG
        }

        //default pass
        Pass
        {
            Tags {
                "Queue" = "Transparent+1"
            }
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
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
