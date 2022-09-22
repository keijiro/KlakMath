Shader "Unlit/Additive"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    CGINCLUDE

#include "UnityCG.cginc"

float4 _Color;

void Vertex(float4 vertexIn : POSITION,
            out float4 vertexOut : SV_Position,
            out float psize : PSIZE)
{
    vertexOut = UnityObjectToClipPos(vertexIn);
    psize = 1;
}

float4 Fragment(float4 vertex : SV_Position) : SV_Target
{
    return float4(_Color.rgb * _Color.a, _Color.a);
}

    ENDCG
    SubShader
    {
        Pass
        {
            Blend One One
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDCG
        }
    }
}
