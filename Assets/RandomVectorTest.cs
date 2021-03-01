using Unity.Mathematics;
using UnityEngine;
using Klak.Math;

sealed class RandomVectorTest : MonoBehaviour
{
    enum Method { Direction, InSphere }

    [SerializeField] Method _method = Method.Direction;
    [SerializeField] int _instanceCount = 100;
    [SerializeField] float _cubeScale = 0.1f;
    [SerializeField] uint _randomSeed = 1;

    float3 Random(in XXHash hash, uint i)
      => _method == Method.Direction ? hash.Direction(i) : hash.InSphere(i);

    void Start()
    {
        var hash = new XXHash(_randomSeed);
        var offs = (float3)transform.position;
        var rot = quaternion.identity;

        var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        temp.transform.localScale = Vector3.one * _cubeScale;

        for (var i = 0u; i < _instanceCount; i++)
            Instantiate(temp, Random(hash, i) + offs, rot, transform);

        Destroy(temp);
    }
}
