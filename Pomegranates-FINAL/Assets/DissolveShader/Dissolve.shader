Shader "Custom/Dissolve"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DissolveMap("Dissolve Map", 2D) = "white" {}
        _Cutoff("Dissolve Amount", Range(0,1)) = 1.0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _EdgeColor("Edge Color", Color) = (1,1,1,1)
        _EdgeWidth("Edge Thickness", Range(0.01, .3)) = 0.1

        _AlphaClip("AlphaClip Threshold", Range(0.0, 1)) = .1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _DissolveMap;
        float _Cutoff;

        float4 _EdgeColor;
        float _EdgeWidth;

        float _AlphaClip;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            clip(c.rgb - _AlphaClip);
            fixed4 dissolveMap = tex2D(_DissolveMap, IN.uv_MainTex);
            o.Albedo = c.rgb;
            clip(dissolveMap.a - _Cutoff);
            o.Metallic = _Metallic;
            float edge = step(dissolveMap.a, _Cutoff + _EdgeWidth);
            o.Smoothness = _Glossiness;
            o.Emission = _EdgeColor * edge;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
