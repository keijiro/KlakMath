// Helper functions for noise generation

using Unity.Mathematics;

namespace Klak.Math {

public static partial class Noise
{
    #region 1D gradient noise

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

    #region FBM functions

    public static float Fbm(float2 v, int octave)
    {
        var f = 0.0f;
        var w = 0.5f;
        for (var i = 0; i < octave; i++)
        {
            f += w * noise.snoise(v);
            v *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float Fbm(float3 v, int octave)
    {
        var f = 0.0f;
        var w = 0.5f;
        for (var i = 0; i < octave; i++)
        {
            f += w * noise.snoise(v);
            v *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    #endregion

    #region Vector generators

    public static float2 Float2(float2 v, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float2(noise.snoise(v + hash.Float2(-1000, 1000, 1)),
                           noise.snoise(v + hash.Float2(-1000, 1000, 2)));
    }

    public static float2 Float2(float3 v, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float2(noise.snoise(v + hash.Float3(-1000, 1000, 1)),
                           noise.snoise(v + hash.Float3(-1000, 1000, 2)));
    }

    public static float2 Float2Fbm(float2 v, int octave, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float2(Fbm(v + hash.Float2(-1000, 1000, 1), octave),
                           Fbm(v + hash.Float2(-1000, 1000, 2), octave));
    }

    public static float2 Float2Fbm(float3 v, int octave, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float2(Fbm(v + hash.Float3(-1000, 1000, 1), octave),
                           Fbm(v + hash.Float3(-1000, 1000, 2), octave));
    }

    public static float3 Float3(float2 v, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float3(noise.snoise(v + hash.Float2(-1000, 1000, 1)),
                           noise.snoise(v + hash.Float2(-1000, 1000, 2)),
                           noise.snoise(v + hash.Float2(-1000, 1000, 3)));
    }

    public static float3 Float3(float3 v, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float3(noise.snoise(v + hash.Float3(-1000, 1000, 1)),
                           noise.snoise(v + hash.Float3(-1000, 1000, 2)),
                           noise.snoise(v + hash.Float3(-1000, 1000, 3)));
    }

    public static float3 Float3Fbm(float2 v, int octave, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float3(Fbm(v + hash.Float2(-1000, 1000, 1), octave),
                           Fbm(v + hash.Float2(-1000, 1000, 2), octave),
                           Fbm(v + hash.Float2(-1000, 1000, 3), octave));
    }

    public static float3 Float3Fbm(float3 v, int octave, uint seed)
    {
        var hash = new XXHash(seed);
        return math.float3(Fbm(v + hash.Float3(-1000, 1000, 1), octave),
                           Fbm(v + hash.Float3(-1000, 1000, 2), octave),
                           Fbm(v + hash.Float3(-1000, 1000, 3), octave));
    }

    #endregion

    #region Quaternion generators

    public static quaternion Rotation(float2 v, float3 angles, uint seed)
    {
        var hash = new XXHash(seed);
        angles *= math.float3(noise.snoise(v + hash.Float2(-1000, 1000, 1)),
                              noise.snoise(v + hash.Float2(-1000, 1000, 2)),
                              noise.snoise(v + hash.Float2(-1000, 1000, 3)));
        return quaternion.EulerZXY(angles);
    }

    public static quaternion Rotation(float3 v, float3 angles, uint seed)
    {
        var hash = new XXHash(seed);
        angles *= math.float3(noise.snoise(v + hash.Float3(-1000, 1000, 1)),
                              noise.snoise(v + hash.Float3(-1000, 1000, 2)),
                              noise.snoise(v + hash.Float3(-1000, 1000, 3)));
        return quaternion.EulerZXY(angles);
    }

    public static quaternion
      RotationFbm(float2 v, int octave, float3 angles, uint seed)
    {
        var hash = new XXHash(seed);
        angles *= math.float3(Fbm(v + hash.Float2(-1000, 1000, 1), octave),
                              Fbm(v + hash.Float2(-1000, 1000, 2), octave),
                              Fbm(v + hash.Float2(-1000, 1000, 3), octave));
        return quaternion.EulerZXY(angles);
    }

    public static quaternion
      RotationFbm(float3 v, int octave, float3 angles, uint seed)
    {
        var hash = new XXHash(seed);
        angles *= math.float3(Fbm(v + hash.Float3(-1000, 1000, 1), octave),
                              Fbm(v + hash.Float3(-1000, 1000, 2), octave),
                              Fbm(v + hash.Float3(-1000, 1000, 3), octave));
        return quaternion.EulerZXY(angles);
    }

    #endregion
}

} // namespace Klak.Math
