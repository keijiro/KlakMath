// 2D simplex noise function

using Unity.Mathematics;

namespace Klak.Math {

public static partial class Noise
{
    public static float3 SimplexNoiseGrad(float2 v)
    {
        var C1 = 0.2113248654f; // (3 - math.sqrt(3)) / 6;
        var C2 = 0.3660254038f; // (math.sqrt(3) - 1) / 2;

        // First corner
        var i  = (uint2)(v     + math.dot(v, C2));
        var x0 =        (v - i + math.dot(i, C1));

        // Other corners
        var i1 = x0.x > x0.y ? math.uint2(1, 0) : math.uint2(0, 1);
        var x1 = x0 + C1 - i1;
        var x2 = x0 + C1 * 2 - 1;

        var p1 = (new XXHash(i.y       )).Float((uint)(i.x       ));
        var p2 = (new XXHash(i.y + i1.y)).Float((uint)(i.x + i1.x));
        var p3 = (new XXHash(i.y +    1)).Float((uint)(i.x +    1));

        var pd = math.float3(p1, p2, p3);
        var pdx = math.abs(1 - 2 * pd) * 2 - 1;
        var pdy = math.frac(0.5f + pd * 2) * 2 - 1;

        var g0 = math.normalize(math.float2(pdx.x, pdy.x));
        var g1 = math.normalize(math.float2(pdx.y, pdy.y));
        var g2 = math.normalize(math.float2(pdx.z, pdy.z));

        // Compute noise and gradient at P
        var m  = math.float3(math.dot(x0, x0), math.dot(x1, x1), math.dot(x2, x2));
        var px = math.float3(math.dot(g0, x0), math.dot(g1, x1), math.dot(g2, x2));

        m = math.max(0.5f - m, 0);
        var m3 = m * m * m;
        var m4 = m * m3;

        var temp = -8 * m3 * px;
        var grad = m4.x * g0 + temp.x * x0 +
                   m4.y * g1 + temp.y * x1 +
                   m4.z * g2 + temp.z * x2;

        return 99.2f * math.float3(grad, math.dot(m4, px));
    }

    public static float SimplexNoise(float2 v)
      => SimplexNoiseGrad(v).z;
}

} // namespace Klak.Math
