using UnityEngine;
using Klak.Math;
using System.Linq;

sealed class HashInSphereTest : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] int _iteration = 10000;

    void Start()
    {
        var hash = new XXHash(_seed);
        var mesh = new Mesh();
        var indices = Enumerable.Range(0, _iteration);
        var vertices = indices.Select(i => (Vector3)hash.InSphere((uint)i));
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Points, 0);
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
