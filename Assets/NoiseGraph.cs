using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Klak.Math;
using System.Linq;

[BurstCompile]
sealed class NoiseGraph : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] float _frequency = 1;
    [SerializeField] int _resolution = 256;
    [SerializeField] Material _material = null;

    Mesh _mesh;
    NativeArray<Vector3> _vertices;

    void Start()
    {
        _mesh = new Mesh();
        _vertices = new NativeArray<Vector3>(_resolution, Allocator.Persistent);

        _mesh.SetVertices(_vertices);
        _mesh.SetIndices(Enumerable.Range(0, _resolution).ToArray(), MeshTopology.LineStrip, 0);
        _mesh.bounds = new Bounds(Vector3.zero, Vector3.one);
    }

    void OnDestroy()
      => _vertices.Dispose();

    void Update()
    {
        var slice = new NativeSlice<Vector3>(_vertices);
        UpdateVertices(ref slice, _frequency, Time.time, _seed);
        _mesh.SetVertices(_vertices);
        Graphics.RenderMesh(new RenderParams(_material), _mesh, 0, transform.localToWorldMatrix);
    }

    [BurstCompile]
    static void UpdateVertices(ref NativeSlice<Vector3> buffer, float freq, float time, uint seed)
    {
        for (var i = 0; i < buffer.Length; i++)
        {
            var x = i * 2.0f / (buffer.Length - 1) - 1;
            var y = Noise.Float((x + time) * freq, seed);
            buffer[i] = new Vector3(x, y, 0);
        }
    }
}
