using UnityEngine;
using UnityEngine.Profiling;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Klak.Math;

[BurstCompile]
sealed class NoiseTextureTest : MonoBehaviour
{
    public enum Implementation { Standard, Klak }

    [SerializeField] Implementation _implementation = Implementation.Standard;
    [SerializeField] int _resolution = 512;
    [SerializeField] float _frequency = 1;
    [SerializeField] float _speed = 0.5f;

    Texture2D _texture;

    void Start()
    {
        _texture = new Texture2D(_resolution, _resolution);
        _texture.wrapMode = TextureWrapMode.Clamp;
        GetComponent<MeshRenderer>().material.mainTexture = _texture;
    }

    void Update()
    {
        var raw = _texture.GetRawTextureData<Color32>();
        var offset = _speed * Time.time;
        var temp = new NativeSlice<Color32>(raw);
        Profiler.BeginSample("Noise Texture");
        if (_implementation == Implementation.Standard)
            FillBitmapStandard(ref temp, _resolution, _frequency, offset);
        else
            FillBitmapKlak(ref temp, _resolution, _frequency, offset);
        Profiler.EndSample();
        _texture.Apply();
    }

    [BurstCompile(OptimizeFor = OptimizeFor.Performance, FloatMode = FloatMode.Fast)]
    static void FillBitmapStandard
      (ref NativeSlice<Color32> raw,
       int resolution, float frequency, float offset)
    {
        var idx = 0;
        for (var y = 0; y < resolution; y++)
        {
            var ny = frequency * y / resolution + offset;
            for (var x = 0; x < resolution; x++)
            {
                var nx = frequency * x / resolution;
                var n = noise.snoise(math.float2(nx, ny)) * 1.001f;
                var gray = (byte)(math.saturate(n / 2 + 0.5f) * 255);
                raw[idx++] = new Color32(gray, gray, gray, 255);
            }
        }
    }

    [BurstCompile(OptimizeFor = OptimizeFor.Performance, FloatMode = FloatMode.Fast)]
    static void FillBitmapKlak
      (ref NativeSlice<Color32> raw,
       int resolution, float frequency, float offset)
    {
        var idx = 0;
        for (var y = 0; y < resolution; y++)
        {
            var ny = frequency * y / resolution + offset;
            for (var x = 0; x < resolution; x++)
            {
                var nx = frequency * x / resolution;
                var n = Noise.SimplexNoise(math.float2(nx, ny)) * 1.0474f;
                var gray = (byte)(math.saturate(n / 2 + 0.5f) * 255);
                raw[idx++] = new Color32(gray, gray, gray, 255);
            }
        }
    }
}
