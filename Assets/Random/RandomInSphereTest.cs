using UnityEngine;
using Klak.Math;
using System.Linq;
using Random = Unity.Mathematics.Random;

sealed class RandomInSphereTest : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] int _iteration = 10000;

    void Start()
    {
        var rand = new Random(_seed);
        var mesh = new Mesh();
        var indices = Enumerable.Range(0, _iteration);
        var vertices = indices.Select(_ => (Vector3)rand.NextFloat3InSphere());
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Points, 0);
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
