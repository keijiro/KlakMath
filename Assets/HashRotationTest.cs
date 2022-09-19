using UnityEngine;
using Unity.Mathematics;
using Klak.Math;
using System.Linq;

sealed class HashRotationTest : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] int _iteration = 10000;
    [SerializeField] Vector3 _baseVector = Vector3.right;

    void Start()
    {
        var hash = new XXHash(_seed);
        var mesh = new Mesh();
        var indices = Enumerable.Range(0, _iteration);
        var vertices = indices.Select(i => (Vector3)(math.mul(hash.Rotation((uint)i), (float3)_baseVector)));
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Points, 0);
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
