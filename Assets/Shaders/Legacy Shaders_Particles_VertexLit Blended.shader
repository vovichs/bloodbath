Shader "Legacy Shaders/Particles/VertexLit Blended"
{
  Properties
  {
    _EmisColor ("Emissive Color", Color) = (0.2,0.2,0.2,0)
    _MainTex ("Particle Texture", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "LIGHTMODE" = "Vertex"
      "PreviewType" = "Plane"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "Vertex"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      uniform float4 _EmisColor;
      uniform int4 unity_VertexLightParams;
      uniform float4 _MainTex_ST;
      //uniform float4 unity_LightColor;
      //uniform float4 unity_LightPosition;
      //uniform float4 unity_LightAtten;
      //uniform float4 unity_SpotDirection;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixV;
      //uniform float4x4 unity_MatrixInvV;
      //uniform float4x4 unity_MatrixVP;
      //uniform float4 glstate_lightmodel_ambient;
      //uniform float4 unity_FogParams;
      //uniform float4 unity_FogColor;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float3 vertex :POSITION0;
          float3 normal :NORMAL;
          float4 color :COLOR;
          float3 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float4 color :COLOR;
          float2 texcoord :TEXCOORD0;
          float texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 color :COLOR;
          float2 texcoord :TEXCOORD0;
          float texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float4 u_xlat2;
      float4 u_xlat3;
      float4 u_xlat4;
      float4 u_xlat5;
      float4 u_xlat6;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0.xyz = (conv_mxt4x4_-4(unity_WorldToObject).yyy * glstate_lightmodel_ambient.xyz);
          u_xlat0.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-4(unity_WorldToObject).xxx) + u_xlat0.xyz);
          u_xlat0.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-4(unity_WorldToObject).zzz) + u_xlat0.xyz);
          u_xlat0.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-4(unity_WorldToObject).www) + u_xlat0.xyz);
          u_xlat1.xyz = (conv_mxt4x4_-3(unity_WorldToObject).yyy * glstate_lightmodel_ambient.xyz);
          u_xlat1.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-3(unity_WorldToObject).xxx) + u_xlat1.xyz);
          u_xlat1.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-3(unity_WorldToObject).zzz) + u_xlat1.xyz);
          u_xlat1.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-3(unity_WorldToObject).www) + u_xlat1.xyz);
          u_xlat2.xyz = (conv_mxt4x4_-2(unity_WorldToObject).yyy * glstate_lightmodel_ambient.xyz);
          u_xlat2.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-2(unity_WorldToObject).xxx) + u_xlat2.xyz);
          u_xlat2.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-2(unity_WorldToObject).zzz) + u_xlat2.xyz);
          u_xlat2.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-2(unity_WorldToObject).www) + u_xlat2.xyz);
          u_xlat3.xyz = (conv_mxt4x4_-1(unity_WorldToObject).yyy * glstate_lightmodel_ambient.xyz);
          u_xlat3.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-1(unity_WorldToObject).xxx) + u_xlat3.xyz);
          u_xlat3.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-1(unity_WorldToObject).zzz) + u_xlat3.xyz);
          u_xlat3.xyz = ((glstate_lightmodel_ambient.xyz * conv_mxt4x4_-1(unity_WorldToObject).www) + u_xlat3.xyz);
          u_xlat4.xyz = mul(unity_WorldToObject, glstate_lightmodel_ambient);
          u_xlat5.xyz = mul(unity_WorldToObject, glstate_lightmodel_ambient);
          u_xlat6.xyz = mul(unity_WorldToObject, glstate_lightmodel_ambient);
          u_xlat1.xyz = (u_xlat1.xyz * in_v.vertex.yyy);
          u_xlat0.xyz = ((u_xlat0.xyz * in_v.vertex.xxx) + u_xlat1.xyz);
          u_xlat0.xyz = ((u_xlat2.xyz * in_v.vertex.zzz) + u_xlat0.xyz);
          u_xlat0.xyz = (u_xlat3.xyz + u_xlat0.xyz);
          u_xlat1.x = dot(u_xlat4.xyzx, in_v.normal.xyzx);
          u_xlat1.y = dot(u_xlat5.xyzx, in_v.normal.xyzx);
          u_xlat1.z = dot(u_xlat6.xyzx, in_v.normal.xyzx);
          u_xlat0.w = dot(u_xlat1.xyzx, u_xlat1.xyzx);
          u_xlat0.w = rsqrt(u_xlat0.w);
          u_xlat1.xyz = (u_xlat0.www * u_xlat1.xyz);
          u_xlat2.xyz = ((in_v.color.xyz * glstate_lightmodel_ambient.xyz) + _MainTex_ST.xyz);
          u_xlat3.xyz = u_xlat2.xyz;
          u_xlat0.w = 0;
          while(true)
          {
              u_xlat1.w = (u_xlat0.w>=_MainTex_ST.x);
              if((u_xlat1.w!=0))
              {
                  break;
              }
              u_xlat4.xyz = (((-u_xlat0.xyz) * unity_SpotDirection.www) + unity_SpotDirection.xyz);
              u_xlat1.w = dot(u_xlat4.xyzx, u_xlat4.xyzx);
              u_xlat2.w = ((unity_SpotDirection.z * u_xlat1.w) + 1);
              u_xlat2.w = (1 / u_xlat2.w);
              u_xlat4.w = (0!=unity_SpotDirection.w);
              u_xlat5.x = (unity_SpotDirection.w<u_xlat1.w);
              u_xlat4.w = (uint(u_xlat4.w) & uint(u_xlat5.x));
              u_xlat2.w = (u_xlat4.w)?(0):(u_xlat2.w);
              u_xlat1.w = max(u_xlat1.w, 1E-06);
              u_xlat1.w = rsqrt(u_xlat1.w);
              u_xlat4.xyz = (u_xlat1.www * u_xlat4.xyz);
              u_xlat1.w = dot(u_xlat4.xyzx, unity_SpotDirection.xyzx);
              u_xlat1.w = max(u_xlat1.w, 0);
              u_xlat1.w = (u_xlat1.w - unity_SpotDirection.x);
              u_xlat1.w = saturate((u_xlat1.w * unity_SpotDirection.y));
              u_xlat1.w = (u_xlat1.w * u_xlat2.w);
              u_xlat1.w = (u_xlat1.w * 0.5);
              u_xlat2.w = dot(u_xlat1.xyzx, u_xlat4.xyzx);
              u_xlat2.w = max(u_xlat2.w, 0);
              u_xlat4.xyz = (u_xlat2.www * in_v.color.xyz);
              u_xlat4.xyz = (u_xlat4.xyz * unity_SpotDirection.xyz);
              u_xlat4.xyz = (u_xlat1.www * u_xlat4.xyz);
              u_xlat4.xyz = min(u_xlat4.xyz, float3(1, 1, 1));
              u_xlat3.xyz = (u_xlat3.xyz + u_xlat4.xyz);
              u_xlat0.w = (u_xlat0.w + 1);
          }
          u_xlat3.w = in_v.color.w;
          out_v.color = saturate(u_xlat3);
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          u_xlat0.x = length(u_xlat0.xyzx);
          u_xlat0.x = (u_xlat0.x * unity_FogParams.x);
          u_xlat0.x = (u_xlat0.x * (-u_xlat0.x));
          out_v.texcoord1.x = exp(u_xlat0.x);
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_-3(unity_WorldToObject));
          u_xlat0 = ((conv_mxt4x4_-4(unity_WorldToObject) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_-2(unity_WorldToObject) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat0 = (u_xlat0 + conv_mxt4x4_-1(unity_WorldToObject));
          u_xlat1 = (u_xlat0.yyyy * glstate_lightmodel_ambient);
          u_xlat1 = ((glstate_lightmodel_ambient * u_xlat0.xxxx) + u_xlat1);
          u_xlat1 = ((glstate_lightmodel_ambient * u_xlat0.zzzz) + u_xlat1);
          out_v.vertex = ((glstate_lightmodel_ambient * u_xlat0.wwww) + u_xlat1);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d = tex2D(_MainTex, in_f.texcoord.xy);
          u_xlat0_d.xyz = ((in_f.color.xyz * u_xlat0_d.xyz) - unity_FogColor.xyz);
          u_xlat0_d.w = (u_xlat0_d.w * in_f.color.w);
          out_f.color.w = u_xlat0_d.w;
          out_f.color.xyz = ((in_f.texcoord1.xxx * u_xlat0_d.xyz) + unity_FogColor.xyz);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
