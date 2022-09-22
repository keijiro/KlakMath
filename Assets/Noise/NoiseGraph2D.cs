using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using Klak.Math;
using Klak.Util;
using System;
using System.Linq;

[BurstCompile]
sealed class NoiseGraph2D : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] float _frequency = 1;
    [SerializeField] float _speed = 1;
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

        var hash = new XXHash(_seed + 0x100000);
        var freq = hash.Float2(0.9f, 1.1f, 0) * _frequency;
        var offs = _speed * (Time.time + 100);

        if (_octaves > 0)
            UpdateVerticesFractal(span, freq, _octaves, offs, _seed);
        else
            UpdateVertices(span, freq, offs, _seed);

        _mesh.SetVertices(_vertices);

        var rparams = new RenderParams(_material);
        Graphics.RenderMesh(rparams, _mesh, 0, transform.localToWorldMatrix);
    }

    [BurstCompile]
    static void UpdateVerticesFractal
      (in UntypedSpan buffer, in float2 freq, uint octaves, float offs, uint seed)
    {
        var span = buffer.GetTyped<Vector3>();
        for (var i = 0; i < span.Length; i++)
        {
            var x = i * 2.0f / (span.Length - 1) - 1;
            var yz = Noise.Fractal2((x + offs) * freq, (int)octaves, seed);
            span[i] = new Vector3(yz.x, yz.y, 0);
        }
    }

    [BurstCompile]
    static void UpdateVertices
      (in UntypedSpan buffer, in float2 freq, float offs, uint seed)
    {
        var span = buffer.GetTyped<Vector3>();
        for (var i = 0; i < span.Length; i++)
        {
            var x = i * 2.0f / (span.Length - 1) - 1;
            var yz = Noise.Float2((x + offs) * freq, seed);
            span[i] = new Vector3(yz.x, yz.y, 0);
        }
    }
}
