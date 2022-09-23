using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using Klak.Math;
using Klak.Util;
using System;
using System.Linq;

[BurstCompile]
sealed class NoiseGraph1D : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] float _frequency = 1;
    [SerializeField, Range(1, 10)] int _octaves = 1;
    [SerializeField] int _resolution = 256;
    [SerializeField] Material _material = null;

    Vector3[] _vertices;
    Mesh _mesh;

    void Start()
    {
        var indices = Enumerable.Range(0, _resolution).ToArray();

        _vertices = new Vector3[_resolution];

        _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
    }

    void OnDestroy()
      => _mesh = ObjectUtil.TryDestroy(_mesh);

    void Update()
    {
        var span = new Span<Vector3>(_vertices).GetRawSpan();
        UpdateVertices(span, _frequency, _octaves, Time.time, _seed);
        _mesh.SetVertices(_vertices);

        var rparams = new RenderParams(_material);
        Graphics.RenderMesh(rparams, _mesh, 0, transform.localToWorldMatrix);
    }

    [BurstCompile]
    static void UpdateVertices
      (in RawSpan<Vector3> buffer, float freq, int oct, float time, uint seed)
    {
        var span = buffer.Span;
        for (var i = 0; i < span.Length; i++)
        {
            var x = math.remap(0, span.Length - 1, -1, 1, i);
            var y = Noise.Fractal((x + time) * freq, oct, seed);
            span[i] = math.float3(x, y, 0);
        }
    }
}
