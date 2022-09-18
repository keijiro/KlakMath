using UnityEngine;
using Klak.Math;

sealed class HashedXformTest : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] float _interval = 0.1f;

    System.Collections.IEnumerator Start()
    {
        var hash = new XXHash(_seed);

        for (uint i = 0;;)
        {
            transform.localPosition = hash.InSphere(i++);
            transform.localRotation = hash.Rotation(i++);
            yield return new WaitForSeconds(_interval);
        }
    }
}
