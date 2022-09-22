// Helper functions for noise generation

using Unity.Mathematics;

namespace Klak.Math {

public static partial class Noise
{
    #region 1D gradient noise (base implementation)

    public static float Float(float p, uint seed)
    {
        var hash = new XXHash(seed);

        p += hash.Float(-1000, 1000, 0x20000000);

        var i = (uint)((int)p + 0x10000000);
        var x = math.frac(p);

        var k = math.float2(x, 1 - x);
        k = 1 - k * k;
        k = k * k * k;

        var g = math.float2(hash.Float(-1, 1, i    ),
                            hash.Float(-1, 1, i + 1));

        var n = math.dot(k * g, math.float2(x, x - 1));
        return n * 2 * 32 / 27;
    }

    #endregion

    #region Vector generators

    public static float2 Float2(float p, uint seed)
    {
        var hash = new XXHash(seed);

        var p2 = p + hash.Float2(-1000, 1000, 0x20000000);

        var i = (uint2)((int2)p2 + 0x10000000);
        var x = math.frac(p2);

        var x0 = x;
        x0 = 1 - x0 * x0;
        x0 = x0 * x0 * x0;

        var x1 = 1 - x;
        x1 = 1 - x1 * x1;
        x1 = x1 * x1 * x1;

        var g0 = hash.Float2(-1, 1, i);
        var g1 = hash.Float2(-1, 1, i + 1);

        var n = x0 * g0 * x + x1 * g1 * (x - 1);
        return n * 2 * 32 / 27;
    }

    public static float3 Float3(float p, uint seed)
    {
        var hash = new XXHash(seed);

        var p2 = p + hash.Float3(-1000, 1000, 0x20000000);

        var i = (uint3)((int3)p2 + 0x10000000);
        var x = math.frac(p2);

        var x0 = x;
        x0 = 1 - x0 * x0;
        x0 = x0 * x0 * x0;

        var x1 = 1 - x;
        x1 = 1 - x1 * x1;
        x1 = x1 * x1 * x1;

        var g0 = hash.Float3(-1, 1, i);
        var g1 = hash.Float3(-1, 1, i + 1);

        var n = x0 * g0 * x + x1 * g1 * (x - 1);
        return n * 2 * 32 / 27;
    }

    public static float4 Float4(float p, uint seed)
    {
        var hash = new XXHash(seed);

        var p2 = p + hash.Float4(-1000, 1000, 0x20000000);

        var i = (uint4)((int4)p2 + 0x10000000);
        var x = math.frac(p2);

        var x0 = x;
        x0 = 1 - x0 * x0;
        x0 = x0 * x0 * x0;

        var x1 = 1 - x;
        x1 = 1 - x1 * x1;
        x1 = x1 * x1 * x1;

        var g0 = hash.Float4(-1, 1, i);
        var g1 = hash.Float4(-1, 1, i + 1);

        var n = x0 * g0 * x + x1 * g1 * (x - 1);
        return n * 2 * 32 / 27;
    }

    #endregion

    #region Fractal noise (fBm)

    public static float FloatFbm(float p, int octave, uint seed)
    {
        var f = 0.0f;
        var w = 0.5f;
        for (var i = 0; i < octave; i++)
        {
            f += w * Float(p, seed);
            p *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float2 Float2Fbm(float p, int octave, uint seed)
    {
        var f = (float2)0;
        var w = 0.5f;
        for (var i = 0; i < octave; i++)
        {
            f += w * Float2(p, seed);
            p *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float3 Float3Fbm(float p, int octave, uint seed)
    {
        var f = (float3)0;
        var w = 0.5f;
        for (var i = 0; i < octave; i++)
        {
            f += w * Float3(p, seed);
            p *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float4 Float4Fbm(float p, int octave, uint seed)
    {
        var f = (float4)0;
        var w = 0.5f;
        for (var i = 0; i < octave; i++)
        {
            f += w * Float4(p, seed);
            p *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    #endregion

    #region Quaternion generators

    public static quaternion Rotation(float p, float3 angles, uint seed)
      => quaternion.EulerZXY(angles * Float3(p, seed));

    public static quaternion
      RotationFbm(float p, int octave, float3 angles, uint seed)
      => quaternion.EulerZXY(angles * Float3Fbm(p, octave, seed));

    #endregion
}

} // namespace Klak.Math
