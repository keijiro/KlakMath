using UnityEngine;
using Unity.Mathematics;
using Klak.Math;

sealed class TweenTest : MonoBehaviour
{
    public enum TweenType { Exp, Cds }

    [SerializeField] TweenType _type = TweenType.Exp;
    [SerializeField] uint _seed = 100;
    [SerializeField] float _speed = 4;
    [SerializeField] float _radius = 5;
    [SerializeField] float _interval = 0.5f;

    (float3 p, quaternion r) _target;
    (float3 p, float4 r) _velocity;

    System.Collections.IEnumerator Start()
    {
        var hash = new XXHash(_seed);
        var wait = new WaitForSeconds(_interval);
        for (var i = 0u;;)
        {
            _target.p = hash.InSphere(i++) * _radius;
            _target.r = hash.Rotation(i++);
            yield return wait;
        }
    }

    void Update()
    {
        if (_type == TweenType.Exp)
        {
            transform.localPosition = ExpTween.Step(transform.localPosition, _target.p, _speed);
            transform.localRotation = ExpTween.Step(transform.localRotation, _target.r, _speed);
        }
        else
        {
            (transform.localPosition, _velocity.p) = CdsTween.Step((transform.localPosition, _velocity.p), _target.p, _speed);
            (transform.localRotation, _velocity.r) = CdsTween.Step((transform.localRotation, _velocity.r), _target.r, _speed);
        }
    }
}
