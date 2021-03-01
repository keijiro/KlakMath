using Unity.Mathematics;
using UnityEngine;
using Klak.Math;

sealed class RandomRotationTest : MonoBehaviour
{
    [SerializeField] int _instanceCount = 100;
    [SerializeField] float _cubeScale = 0.1f;
    [SerializeField] uint _randomSeed = 1;

    void Start()
    {
        var hash = new XXHash(_randomSeed);
        var offs = (float3)transform.position;

        var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        temp.transform.localScale = new Vector3(1, 10, 1) * _cubeScale;

        for (var i = 0u; i < _instanceCount; i++)
        {
            var rot = hash.Rotation(i);
            Instantiate(temp, math.forward(rot) + offs, rot, transform);
        }

        Destroy(temp);
    }
}
