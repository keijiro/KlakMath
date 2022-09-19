using UnityEngine;
using Unity.Mathematics;
using Klak.Math;

sealed class FromToRotationTest : MonoBehaviour
{
    [SerializeField] Transform _target = null;
    [SerializeField] Vector3 _fromVector = Vector3.up;

    void Update()
      => transform.localRotation =
           mathx.FromToRotation(_fromVector,
                                _target.localPosition - transform.localPosition);
}
