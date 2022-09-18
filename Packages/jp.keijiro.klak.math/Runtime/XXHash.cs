// XXHash deterministic random number generator

using Unity.Mathematics;

namespace Klak.Math {

public readonly struct XXHash
{
    #region Public members

    public uint Seed { get; }

    public XXHash(uint seed)
      => Seed = seed;

    // bool

    public bool Bool(uint data)
      => (CalculateHash(data, Seed) & 1) != 0;

    // uint

    public uint UInt(uint data)
      => CalculateHash(data, Seed);

    public uint UInt(uint max, uint data)
      => math.select(0u, UInt(data) % max, max > 0u);

    public uint UInt(uint min, uint max, uint data)
      => UInt(max - min, data) + min;

    // int

    public int Int(uint data)
      => (int)UInt(data);

    public int Int(int max, uint data)
      => math.select(0, Int(data) % max, max > 0);

    public int Int(int min, int max, uint data)
      => Int(max - min, data) + min;

    // int2

    public int2 Int2(uint data)
      => math.int2(Int(data), Int(data + 0x10000000));

    public int2 Int2(int2 max, uint data)
      => math.select(0, Int2(data) % max, max > 0);

    public int2 Int2(int2 min, int2 max, uint data)
      => Int2(max - min, data) + min;

    // int3

    public int3 Int3(uint data)
      => math.int3(Int2(data), Int(data + 0x20000000));

    public int3 Int3(int3 max, uint data)
      => math.select(0, Int3(data) % max, max > 0);

    public int3 Int3(int3 min, int3 max, uint data)
      => Int3(max - min, data) + min;

    // int4

    public int4 Int4(uint data)
      => math.int4(Int3(data), Int(data + 0x30000000));

    public int4 Int4(int4 max, uint data)
      => math.select(0, Int4(data) % max, max > 0);

    public int4 Int4(int4 min, int4 max, uint data)
      => Int4(max - min, data) + min;

    // float

    public float Float(uint data)
      => UInt(data) / (float)uint.MaxValue;

    public float Float(float max, uint data)
      => Float(data) * max;

    public float Float(float min, float max, uint data)
      => Float(data) * (max - min) + min;

    // float2

    public float2 Float2(uint data)
      => math.float2(Float(data), Float(data + 0x10000000));

    public float2 Float2(float2 max, uint data)
      => Float2(data) * max;

    public float2 Float2(float2 min, float2 max, uint data)
      => Float2(data) * (max - min) + min;

    // float3

    public float3 Float3(uint data)
      => math.float3(Float2(data), Float(data + 0x20000000));

    public float3 Float3(float3 max, uint data)
      => Float3(data) * max;

    public float3 Float3(float3 min, float3 max, uint data)
      => Float3(data) * (max - min) + min;

    // float4

    public float4 Float4(uint data)
      => math.float4(Float3(data), Float(data + 0x30000000));

    public float4 Float4(float4 max, uint data)
      => Float4(data) * max;

    public float4 Float4(float4 min, float4 max, uint data)
      => Float4(data) * (max - min) + min;

    // On unit circle

    public float2 OnCircle(uint data)
    {
        var phi = Float(math.PI * 2, data);
        return math.float2(math.cos(phi), math.sin(phi));
    }

    // Inside unit circle

    public float2 InCircle(uint data)
      => OnCircle(data) * math.sqrt(Float(data + 0x10000000));

    // On unit sphere

    public float3 OnSphere(uint data)
    {
        var phi = Float(math.PI * 2, data);
        var z = Float(-1, 1, data + 0x10000000);
        var w = math.sqrt(1 - z * z);
        return math.float3(math.cos(phi) * w, math.sin(phi) * w, z);
    }

    // Inside unit sphere

    public float3 InSphere(uint data)
      => OnSphere(data) * math.pow(Float(data + 0x20000000), 1.0f / 3);

    // Rotation

    public quaternion Rotation(uint data)
    {
        var u1 = Float(data);
        var r1 = Float(math.PI * 2, data + 0x10000000);
        var r2 = Float(math.PI * 2, data + 0x20000000);
        var s1 = math.sqrt(1 - u1);
        var s2 = math.sqrt(    u1);
        var v = math.float4(s1 * math.sin(r1), s1 * math.cos(r1),
                            s2 * math.sin(r2), s2 * math.cos(r2));
        return math.quaternion(math.select(v, -v, v.w < 0));
    }

    #endregion

    #region Public class members

    static XXHash RandomHash
      => new XXHash(XXHash.CalculateHash(0xcafe, _counter++));

    static uint CalculateHash(uint data, uint seed)
    {
        uint h32 = seed + PRIME32_5;
        h32 += 4U;
        h32 += data * PRIME32_3;
        h32 = rotl32(h32, 17) * PRIME32_4;
        h32 ^= h32 >> 15;
        h32 *= PRIME32_2;
        h32 ^= h32 >> 13;
        h32 *= PRIME32_3;
        h32 ^= h32 >> 16;
        return h32;
    }

    #endregion

    #region Private Members

    static uint _counter;

    const uint PRIME32_1 = 2654435761U;
    const uint PRIME32_2 = 2246822519U;
    const uint PRIME32_3 = 3266489917U;
    const uint PRIME32_4 = 668265263U;
    const uint PRIME32_5 = 374761393U;

    static uint rotl32(uint x, int r)
      => (x << r) | (x >> 32 - r);

    #endregion
}

} // namespace Klak.Math2
