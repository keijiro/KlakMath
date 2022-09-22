// 1D gradient noise functions

using Unity.Mathematics;

namespace Klak.Math {

public static partial class Noise
{
    #region 1D gradient noise (base implementation)

    public static float Float(float p, uint seed)
    {
        var hash = new XXHash(seed);

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

    public static float2 Float2(float2 p, uint seed)
    {
        var hash = new XXHash(seed);

        var i = (uint2)((int2)p + 0x10000000);
        var x = math.frac(p);

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

    public static float3 Float3(float3 p, uint seed)
    {
        var hash = new XXHash(seed);

        var i = (uint3)((int3)p + 0x10000000);
        var x = math.frac(p);

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

    public static float4 Float4(float4 p, uint seed)
    {
        var hash = new XXHash(seed);

        var i = (uint4)((int4)p + 0x10000000);
        var x = math.frac(p);

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

    #region Fractal noise

    public static float Fractal(float p, int octave, uint seed)
    {
        var f = 0.0f;
        var w = 1.0f;
        for (var i = 0; i < octave; i++)
        {
            f += w * Float(p, seed);
            p *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float2 Fractal2(float2 p, int octave, uint seed)
    {
        var f = (float2)0;
        var w = 1.0f;
        for (var i = 0; i < octave; i++)
        {
            f += w * Float2(p, seed);
            p *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float3 Fractal3(float3 p, int octave, uint seed)
    {
        var f = (float3)0;
        var w = 1.0f;
        for (var i = 0; i < octave; i++)
        {
            f += w * Float3(p, seed);
            p *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float4 Fractal4(float4 p, int octave, uint seed)
    {
        var f = (float4)0;
        var w = 1.0f;
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

    public static quaternion Rotation(float3 p, float3 angles, uint seed)
      => quaternion.EulerZXY(angles * Float3(p, seed));

    public static quaternion
      FractalRotation(float3 p, int octave, float3 angles, uint seed)
      => quaternion.EulerZXY(angles * Fractal3(p, octave, seed));

    #endregion
}

} // namespace Klak.Math
