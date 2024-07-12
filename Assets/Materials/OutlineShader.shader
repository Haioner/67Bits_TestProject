Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineThickness ("Outline Thickness", Float) = 0.03
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "BASE"
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 posWorld : TEXCOORD1;
                float3 normal : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.normal = mul((float3x3)unity_ObjectToWorld, v.normal);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }

        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            float _OutlineThickness;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                // Inflate the vertex along its normal
                v2f o;
                float3 norm = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                o.vertex = UnityObjectToClipPos(v.vertex + float4(norm * _OutlineThickness, 0));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.normal = norm;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
