// 2D simplex noise function

using Unity.Mathematics;

namespace Klak.Math {

public static partial class Noise
{
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

        h = ((h >> 8) & 127) * 2;

        var g0 = math.float2(table[h.x], table[h.x + 1]);
        var g1 = math.float2(table[h.y], table[h.y + 1]);
        var g2 = math.float2(table[h.z], table[h.z + 1]);

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

    static readonly float[] table =
    {
1.000000f, 0.000000f,
0.998795f, 0.049068f,
0.995185f, 0.098017f,
0.989177f, 0.146730f,
0.980785f, 0.195090f,
0.970031f, 0.242980f,
0.956940f, 0.290285f,
0.941544f, 0.336890f,
0.923880f, 0.382683f,
0.903989f, 0.427555f,
0.881921f, 0.471397f,
0.857729f, 0.514103f,
0.831470f, 0.555570f,
0.803208f, 0.595699f,
0.773010f, 0.634393f,
0.740951f, 0.671559f,
0.707107f, 0.707107f,
0.671559f, 0.740951f,
0.634393f, 0.773010f,
0.595699f, 0.803208f,
0.555570f, 0.831470f,
0.514103f, 0.857729f,
0.471397f, 0.881921f,
0.427555f, 0.903989f,
0.382683f, 0.923880f,
0.336890f, 0.941544f,
0.290285f, 0.956940f,
0.242980f, 0.970031f,
0.195090f, 0.980785f,
0.146730f, 0.989177f,
0.098017f, 0.995185f,
0.049068f, 0.998795f,
0.000000f, 1.000000f,
-0.049068f, 0.998795f,
-0.098017f, 0.995185f,
-0.146730f, 0.989177f,
-0.195090f, 0.980785f,
-0.242980f, 0.970031f,
-0.290285f, 0.956940f,
-0.336890f, 0.941544f,
-0.382683f, 0.923880f,
-0.427555f, 0.903989f,
-0.471397f, 0.881921f,
-0.514103f, 0.857729f,
-0.555570f, 0.831470f,
-0.595699f, 0.803208f,
-0.634393f, 0.773010f,
-0.671559f, 0.740951f,
-0.707107f, 0.707107f,
-0.740951f, 0.671559f,
-0.773010f, 0.634393f,
-0.803208f, 0.595699f,
-0.831470f, 0.555570f,
-0.857729f, 0.514103f,
-0.881921f, 0.471397f,
-0.903989f, 0.427555f,
-0.923880f, 0.382683f,
-0.941544f, 0.336890f,
-0.956940f, 0.290285f,
-0.970031f, 0.242980f,
-0.980785f, 0.195090f,
-0.989177f, 0.146730f,
-0.995185f, 0.098017f,
-0.998795f, 0.049068f,
-1.000000f, 0.000000f,
-0.998795f, -0.049068f,
-0.995185f, -0.098017f,
-0.989177f, -0.146730f,
-0.980785f, -0.195090f,
-0.970031f, -0.242980f,
-0.956940f, -0.290285f,
-0.941544f, -0.336890f,
-0.923880f, -0.382683f,
-0.903989f, -0.427555f,
-0.881921f, -0.471397f,
-0.857729f, -0.514103f,
-0.831470f, -0.555570f,
-0.803208f, -0.595699f,
-0.773010f, -0.634393f,
-0.740951f, -0.671559f,
-0.707107f, -0.707107f,
-0.671559f, -0.740951f,
-0.634393f, -0.773010f,
-0.595699f, -0.803208f,
-0.555570f, -0.831470f,
-0.514103f, -0.857729f,
-0.471397f, -0.881921f,
-0.427555f, -0.903989f,
-0.382683f, -0.923880f,
-0.336890f, -0.941544f,
-0.290285f, -0.956940f,
-0.242980f, -0.970031f,
-0.195090f, -0.980785f,
-0.146730f, -0.989177f,
-0.098017f, -0.995185f,
-0.049068f, -0.998795f,
-0.000000f, -1.000000f,
0.049068f, -0.998795f,
0.098017f, -0.995185f,
0.146730f, -0.989177f,
0.195090f, -0.980785f,
0.242980f, -0.970031f,
0.290285f, -0.956940f,
0.336890f, -0.941544f,
0.382683f, -0.923880f,
0.427555f, -0.903989f,
0.471397f, -0.881921f,
0.514103f, -0.857729f,
0.555570f, -0.831470f,
0.595699f, -0.803208f,
0.634393f, -0.773010f,
0.671559f, -0.740951f,
0.707107f, -0.707107f,
0.740951f, -0.671559f,
0.773010f, -0.634393f,
0.803208f, -0.595699f,
0.831470f, -0.555570f,
0.857729f, -0.514103f,
0.881921f, -0.471397f,
0.903989f, -0.427555f,
0.923880f, -0.382683f,
0.941544f, -0.336890f,
0.956940f, -0.290285f,
0.970031f, -0.242980f,
0.980785f, -0.195090f,
0.989177f, -0.146730f,
0.995185f, -0.098017f,
0.998795f, -0.049068f
    };
}

} // namespace Klak.Math