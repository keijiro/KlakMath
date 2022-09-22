using UnityEngine;
using Unity.Burst;
using Klak.Math;
using Klak.Util;
using System;
using System.Linq;

[BurstCompile]
sealed class NoiseGraph1D : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] float _frequency = 1;
    [SerializeField, Range(0, 10)] uint _octaves = 0;
    [SerializeField] int _resolution = 256;
    [SerializeField] Material _material = null;

    Mesh _mesh;
    Vector3[] _vertices;

    void Start()
    {
        _vertices = new Vector3[_resolution];

        var indices = Enumerable.Range(0, _resolution).ToArray();

        _mesh = new Mesh();
        _mesh.SetVertices(_vertices);
        _mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
        _mesh.bounds = new Bounds(Vector3.zero, Vector3.one);
    }

    void OnDestroy()
      => _mesh = SafeObject.TryDestroy(_mesh);

    void Update()
    {
        var span = new Span<Vector3>(_vertices).GetUntyped();
        var t = Time.time;

        if (_octaves > 0)
            UpdateVerticesFractal(span, _frequency, _octaves, t, _seed);
        else
            UpdateVertices(span, _frequency, t, _seed);

        _mesh.SetVertices(_vertices);

        var rparams = new RenderParams(_material);
        Graphics.RenderMesh(rparams, _mesh, 0, transform.localToWorldMatrix);
    }

    [BurstCompile]
    static void UpdateVerticesFractal
      (in UntypedSpan buffer, float freq, uint octaves, float time, uint seed)
    {
        var span = buffer.GetTyped<Vector3>();
        for (var i = 0; i < span.Length; i++)
        {
            var x = i * 2.0f / (span.Length - 1) - 1;
            var y = Noise.Fractal((x + time) * freq, (int)octaves, seed);
            span[i] = new Vector3(x, y, 0);
        }
    }

    [BurstCompile]
    static void UpdateVertices
      (in UntypedSpan buffer, float freq, float time, uint seed)
    {
        var span = buffer.GetTyped<Vector3>();
        for (var i = 0; i < span.Length; i++)
        {
            var x = i * 2.0f / (span.Length - 1) - 1;
            var y = Noise.Float((x + time) * freq, seed);
            span[i] = new Vector3(x, y, 0);
        }
    }
}
