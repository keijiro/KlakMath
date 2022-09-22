using UnityEngine;
using Unity.Mathematics;
using Klak.Math;

sealed class NoiseMotionTest : MonoBehaviour
{
    [SerializeField] uint _seed = 100;
    [SerializeField] float _frequency = 1;
    [SerializeField] float _radius = 1;
    [SerializeField] float _angle = math.PI;
    [SerializeField] int _octaves = 3;

    void Update()
    {
        var x = Time.time * _frequency;
        transform.localPosition = Noise.Fractal3(x, _octaves, _seed * 2) * _radius;
        transform.localRotation = Noise.FractalRotation(x, _octaves, _angle, _seed * 2 + 1);
    }
}
