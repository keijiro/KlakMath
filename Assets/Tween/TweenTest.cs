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
        for (var i = 0u;;)
        {
            _target.p = hash.InSphere(i++) * _radius;
            _target.r = hash.Rotation(i++);
            yield return new WaitForSeconds(_interval);
        }
    }

    void Update()
    {
        var p = transform.localPosition;
        var r = transform.localRotation;

        if (_type == TweenType.Exp)
        {
            p = ExpTween.Step(p, _target.p, _speed);
            r = ExpTween.Step(r, _target.r, _speed);
        }
        else
        {
            (p, _velocity.p) = CdsTween.Step((p, _velocity.p), _target.p, _speed);
            (r, _velocity.r) = CdsTween.Step((r, _velocity.r), _target.r, _speed);
        }

        transform.localPosition = p;
        transform.localRotation = r;
    }
}
