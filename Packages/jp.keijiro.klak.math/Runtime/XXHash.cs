// XXHash as a deterministic random number generator

using Unity.Mathematics;

namespace Klak.Math {

public readonly struct XXHash
{
    #region Base interface

    public uint Seed { get; }

    public XXHash(uint seed) => Seed = seed;

    #endregion

    #region UInt: Full range

    public uint  UInt (uint  data) => CalculateHash(data, Seed);
    public uint2 UInt2(uint2 data) => CalculateHash(data, (uint2)Seed);
    public uint3 UInt3(uint3 data) => CalculateHash(data, (uint3)Seed);
    public uint4 UInt4(uint4 data) => CalculateHash(data, (uint4)Seed);

    #endregion

    #region UInt: Single seed support

    public uint2 UInt2(uint data) => UInt2(math.uint2(data, data + 0x10000000));
    public uint3 UInt3(uint data) => UInt3(math.uint3(data, data + 0x10000000, data + 0x20000000));
    public uint4 UInt4(uint data) => UInt4(math.uint4(data, data + 0x10000000, data + 0x20000000, data + 0x30000000));

    #endregion

    #region UInt: (0 - Max) range

    public uint  UInt (uint max, uint  data) => math.select(0u, UInt (data) % max, max > 0u);
    public uint2 UInt2(uint max, uint2 data) => math.select(0u, UInt2(data) % max, max > 0u);
    public uint2 UInt2(uint max, uint  data) => math.select(0u, UInt2(data) % max, max > 0u);
    public uint3 UInt3(uint max, uint3 data) => math.select(0u, UInt3(data) % max, max > 0u);
    public uint3 UInt3(uint max, uint  data) => math.select(0u, UInt3(data) % max, max > 0u);
    public uint4 UInt4(uint max, uint4 data) => math.select(0u, UInt4(data) % max, max > 0u);
    public uint4 UInt4(uint max, uint  data) => math.select(0u, UInt4(data) % max, max > 0u);

    #endregion

    #region UInt: (Min - Max) range

    public uint  UInt (uint min, uint max, uint  data) => UInt (max - min, data) + min;
    public uint2 UInt2(uint min, uint max, uint2 data) => UInt2(max - min, data) + min;
    public uint2 UInt2(uint min, uint max, uint  data) => UInt2(max - min, data) + min;
    public uint3 UInt3(uint min, uint max, uint3 data) => UInt3(max - min, data) + min;
    public uint3 UInt3(uint min, uint max, uint  data) => UInt3(max - min, data) + min;
    public uint4 UInt4(uint min, uint max, uint4 data) => UInt4(max - min, data) + min;
    public uint4 UInt4(uint min, uint max, uint  data) => UInt4(max - min, data) + min;

    #endregion

    #region Bool methods

    public bool  Bool (uint  data) => (UInt (data) & 1) != 0;
    public bool2 Bool2(uint2 data) => (UInt2(data) & 1) != 0;
    public bool2 Bool2(uint  data) => (UInt2(data) & 1) != 0;
    public bool3 Bool3(uint3 data) => (UInt3(data) & 1) != 0;
    public bool3 Bool3(uint  data) => (UInt3(data) & 1) != 0;
    public bool4 Bool4(uint4 data) => (UInt4(data) & 1) != 0;
    public bool4 Bool4(uint  data) => (UInt4(data) & 1) != 0;

    #endregion

    #region Int: Full range

    public int  Int (uint  data) => (int) UInt (data);
    public int2 Int2(uint  data) => (int2)UInt2(data);
    public int2 Int2(uint2 data) => (int2)UInt2(data);
    public int3 Int3(uint  data) => (int3)UInt3(data);
    public int3 Int3(uint3 data) => (int3)UInt3(data);
    public int4 Int4(uint  data) => (int4)UInt4(data);
    public int4 Int4(uint4 data) => (int4)UInt4(data);

    #endregion

    #region Int: (0 - Max) range

    public int  Int (int  max, uint  data) => math.select(0, Int (data) % max, max > 0);
    public int2 Int2(int2 max, uint  data) => math.select(0, Int2(data) % max, max > 0);
    public int2 Int2(int2 max, uint2 data) => math.select(0, Int2(data) % max, max > 0);
    public int3 Int3(int3 max, uint  data) => math.select(0, Int3(data) % max, max > 0);
    public int3 Int3(int3 max, uint3 data) => math.select(0, Int3(data) % max, max > 0);
    public int4 Int4(int4 max, uint  data) => math.select(0, Int4(data) % max, max > 0);
    public int4 Int4(int4 max, uint4 data) => math.select(0, Int4(data) % max, max > 0);

    #endregion

    #region Int: (Min - Max) range

    public int  Int (int  min, int  max, uint  data) => Int (max - min, data) + min;
    public int2 Int2(int2 min, int2 max, uint  data) => Int2(max - min, data) + min;
    public int2 Int2(int2 min, int2 max, uint2 data) => Int2(max - min, data) + min;
    public int3 Int3(int3 min, int3 max, uint  data) => Int3(max - min, data) + min;
    public int3 Int3(int3 min, int3 max, uint3 data) => Int3(max - min, data) + min;
    public int4 Int4(int4 min, int4 max, uint  data) => Int4(max - min, data) + min;
    public int4 Int4(int4 min, int4 max, uint4 data) => Int4(max - min, data) + min;

    #endregion

    #region Float: Full range

    public float  Float (uint  data) =>         UInt (data) / (float)uint.MaxValue;
    public float2 Float2(uint  data) => (float2)UInt2(data) / (float)uint.MaxValue;
    public float2 Float2(uint2 data) => (float2)UInt2(data) / (float)uint.MaxValue;
    public float3 Float3(uint  data) => (float3)UInt3(data) / (float)uint.MaxValue;
    public float3 Float3(uint3 data) => (float3)UInt3(data) / (float)uint.MaxValue;
    public float4 Float4(uint  data) => (float4)UInt4(data) / (float)uint.MaxValue;
    public float4 Float4(uint4 data) => (float4)UInt4(data) / (float)uint.MaxValue;

    #endregion

    #region Float: (0 - Max) range

    public float  Float (float  max, uint  data) => Float (data) * max;
    public float2 Float2(float2 max, uint  data) => Float2(data) * max;
    public float2 Float2(float2 max, uint2 data) => Float2(data) * max;
    public float3 Float3(float3 max, uint  data) => Float3(data) * max;
    public float3 Float3(float3 max, uint3 data) => Float3(data) * max;
    public float4 Float4(float4 max, uint  data) => Float4(data) * max;
    public float4 Float4(float4 max, uint4 data) => Float4(data) * max;

    #endregion

    #region Float: (Min - Max) range

    public float  Float (float  min, float  max, uint  data) => Float (data) * (max - min) + min;
    public float2 Float2(float2 min, float2 max, uint  data) => Float2(data) * (max - min) + min;
    public float2 Float2(float2 min, float2 max, uint2 data) => Float2(data) * (max - min) + min;
    public float3 Float3(float3 min, float3 max, uint  data) => Float3(data) * (max - min) + min;
    public float3 Float3(float3 min, float3 max, uint3 data) => Float3(data) * (max - min) + min;
    public float4 Float4(float4 min, float4 max, uint  data) => Float4(data) * (max - min) + min;
    public float4 Float4(float4 min, float4 max, uint4 data) => Float4(data) * (max - min) + min;

    #endregion

    #region Geometric utilities

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

    static XXHash RandomHash => new XXHash(XXHash.CalculateHash(0xcafe, _counter++));

    static uint CalculateHash(uint data, uint seed)
    {
        var h32 = seed + PRIME32_5;
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

    static uint2 CalculateHash(uint2 data, uint2 seed)
    {
        var h32 = seed + PRIME32_5;
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

    static uint3 CalculateHash(uint3 data, uint3 seed)
    {
        var h32 = seed + PRIME32_5;
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

    static uint4 CalculateHash(uint4 data, uint4 seed)
    {
        var h32 = seed + PRIME32_5;
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

    static uint  rotl32(uint  x, int r) => (x << r) | (x >> 32 - r);
    static uint2 rotl32(uint2 x, int r) => (x << r) | (x >> 32 - r);
    static uint3 rotl32(uint3 x, int r) => (x << r) | (x >> 32 - r);
    static uint4 rotl32(uint4 x, int r) => (x << r) | (x >> 32 - r);

    #endregion
}

} // namespace Klak.Math
