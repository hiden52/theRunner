Shader "Custom/Surface"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Curvature("Curvature", float) = 0.001
        _Color ("Color Tint", Color) = (1,1,1,1) // 머테리얼 색상 추가
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert addshadow
        uniform sampler2D _MainTex;
        uniform float _Curvature;
        uniform float4 _Color; // 색상 값 추가

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v)
        {
            float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
            worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
            worldSpace = float4(0.0f, (worldSpace.z * worldSpace.z) * -_Curvature, 0.0f, 0.0f);
    
            v.vertex += mul(unity_WorldToObject, worldSpace);
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color.rgb; // 머테리얼의 색상과 텍스처 색상을 곱하기
            o.Alpha = c.a;
        }

        ENDCG        
    }
    FallBack "Mobile/Diffuse"
}
