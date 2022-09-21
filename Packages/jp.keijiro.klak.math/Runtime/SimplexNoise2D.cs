// 2D simplex noise function

using Unity.Mathematics;

namespace Klak.Math {

public static partial class Noise
{
    static float3 SinApprox(float3 x)
      => 1.27323954f * x - 0.405284735f * x * x;

    static float3 FastSin(float3 x)
      => math.select(-SinApprox(x - math.PI), SinApprox(x), x < math.PI);

    static float3 FastCos(float3 x)
      => FastSin(math.frac(x + 0.25f));

    public static float3 SimplexNoiseGrad(float2 v)
    {
        const float C1 = 0.2113248654f; // (3 - sqrt(3)) / 6;
        const float C2 = 0.3660254038f; // (sqrt(3) - 1) / 2;

        // First corner
        var i  = (uint2)(v     + math.dot(v, C2));
        var x0 =        (v - i + math.dot(i, C1));

        // Other corners
        var i1 = x0.x > x0.y ? math.uint2(1, 0) : math.uint2(0, 1);
        var x1 = x0 + C1 - i1;
        var x2 = x0 + C1 * 2 - 1;

        var h = math.uint3(i.x, i.x + i1.x, i.x + 1);
        h += math.uint3(i.y, i.y + i1.y, i.y + 1) << 16;
        h ^= h << 13;
        h ^= h >> 17;
        h ^= h << 5;

        var phi = math.frac((float3)(h >> 8) / 256.0f) * math.PI * 2;
        var gg0 = FastCos(phi);
        var gg1 = FastSin(phi);

        var g0 = math.float2(gg0.x, gg1.x);
        var g1 = math.float2(gg0.y, gg1.y);
        var g2 = math.float2(gg0.z, gg1.z);

        /*
        h = ((h >> 8) & 127) * 2;
        var g0 = math.float2(table[h.x], table[h.x + 1]);
        var g1 = math.float2(table[h.y], table[h.y + 1]);
        var g2 = math.float2(table[h.z], table[h.z + 1]);
        */

        // Compute noise and gradient at P
        var m  = math.float3(math.dot(x0, x0), math.dot(x1, x1), math.dot(x2, x2));
        var px = math.float3(math.dot(g0, x0), math.dot(g1, x1), math.dot(g2, x2));

        m = math.max(0.5f - m, 0);
        var m3 = m * m * m;
        var m4 = m * m3;

        return 99.2f *  math.dot(m4, px);

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
