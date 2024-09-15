Shader "Polytope Studio/PT_Water_Shader"
{
    Properties
    {
        _DeepColor("Deep Color", Color) = (0.3114988,0.5266015,0.5283019,0)
        _ShallowColor("Shallow Color", Color) = (0.5238074,0.7314408,0.745283,0)
        _Depth("Depth", Range(0, 1)) = 0.3
        _DepthStrength("Depth Strength", Range(0, 1)) = 0.3
        _Smoothness("Smoothness", Range(0, 1)) = 1
        _Metallic("Metallic", Range(0, 1)) = 1
        _WaveSpeed("Wave Speed", Range(0, 1)) = 0.5
        _WaveTile("Wave Tile", Range(0, 0.9)) = 0.5
        _WaveAmplitude("Wave Amplitude", Range(0, 1)) = 0.2
        [NoScaleOffset][Normal] _NormalMapTexture("Normal Map Texture", 2D) = "bump" {}
        _FoamColor("Foam Color", Color) = (0.3066038,1,0.9145772,0)
        _FoamAmount("Foam Amount", Range(0, 10)) = 1.5
        _FoamPower("Foam Power", Range(0.1, 5)) = 0.5
        _FoamNoiseScale("Foam Noise Scale", Range(0, 1000)) = 150
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent+0" }
        LOD 200

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _NormalMapTexture;
            float4 _DeepColor;
            float4 _ShallowColor;
            float _Depth;
            float _DepthStrength;
            float _Smoothness;
            float _Metallic;
            float _WaveSpeed;
            float _WaveTile;
            float _WaveAmplitude;
            float4 _FoamColor;
            float _FoamAmount;
            float _FoamPower;
            float _FoamNoiseScale;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _WaveTile;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 texColor = tex2D(_NormalMapTexture, i.uv);
                half3 color = lerp(_ShallowColor.rgb, _DeepColor.rgb, _DepthStrength);
                return half4(color, 1.0);
            }
            ENDCG
        }
    }
}