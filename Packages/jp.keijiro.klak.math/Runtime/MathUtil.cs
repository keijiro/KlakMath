// Miscellaneous utility functions

using Unity.Mathematics;

namespace Klak.Math
{
    public static class MathUtil
    {
        public static float2 SinCos(float x)
        {
            float sin, cos;
            math.sincos(x, out sin, out cos);
            return math.float2(sin, cos);
        }

        public static quaternion FromToRotation(float3 v1, float3 v2)
        {
            var a = math.cross(v1, v2);
            var v1v2 = math.dot(v1, v1) * math.dot(v2, v2);
            var w = math.sqrt(v1v2) + math.dot(v1, v2);
            return math.normalizesafe(math.quaternion(math.float4(a, w)));
        }
    }
}
