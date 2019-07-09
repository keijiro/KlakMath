// Tween with exponential interpolation

using Unity.Mathematics;

namespace Klak.Math
{
    #region float support

    public partial struct ExpTween
    {
        public float Current { get; set; }
        public float Speed { get; set; }

        public ExpTween(float initial, float speed)
        {
            Current = initial;
            Speed = speed;
        }

        public float Step(float target)
        {
            Current = Step(Current, target, Speed);
            return Current;
        }
    }

    #endregion

    #region float2 support

    public struct ExpTween2
    {
        public float2 Current { get; set; }
        public float Speed { get; set; }

        public ExpTween2(float2 initial, float speed)
        {
            Current = initial;
            Speed = speed;
        }

        public float2 Step(float2 target)
        {
            Current = ExpTween.Step(Current, target, Speed);
            return Current;
        }
    }

    #endregion

    #region float3 support

    public struct ExpTween3
    {
        public float3 Current { get; set; }
        public float Speed { get; set; }

        public ExpTween3(float3 initial, float speed)
        {
            Current = initial;
            Speed = speed;
        }

        public float3 Step(float3 target)
        {
            Current = ExpTween.Step(Current, target, Speed);
            return Current;
        }
    }

    #endregion

    #region float4 support

    public struct ExpTween4
    {
        public float4 Current { get; set; }
        public float Speed { get; set; }

        public ExpTween4(float4 initial, float speed)
        {
            Current = initial;
            Speed = speed;
        }

        public float4 Step(float4 target)
        {
            Current = ExpTween.Step(Current, target, Speed);
            return Current;
        }
    }

    #endregion

    #region quaternion support

    public struct ExpTweenQ
    {
        public quaternion Current { get; set; }
        public float Speed { get; set; }

        public ExpTweenQ(quaternion initial, float speed)
        {
            Current = initial;
            Speed = speed;
        }

        public quaternion Step(quaternion target)
        {
            Current = ExpTween.Step(Current, target, Speed);
            return Current;
        }
    }

    #endregion

    #region Static method implementation

    public partial struct ExpTween
    {
        public static float Step(float current, float target, float speed)
        {
            var exp = math.exp(-speed * UnityEngine.Time.deltaTime);
            return math.lerp(target, current, exp);
        }

        public static float2 Step(float2 current, float2 target, float speed)
        {
            var exp = math.exp(-speed * UnityEngine.Time.deltaTime);
            return math.lerp(target, current, exp);
        }

        public static float3 Step(float3 current, float3 target, float speed)
        {
            var exp = math.exp(-speed * UnityEngine.Time.deltaTime);
            return math.lerp(target, current, exp);
        }

        public static float4 Step(float4 current, float4 target, float speed)
        {
            var exp = math.exp(-speed * UnityEngine.Time.deltaTime);
            return math.lerp(target, current, exp);
        }

        public static quaternion Step
            (quaternion current, quaternion target, float speed)
        {
            var exp = math.exp(-speed * UnityEngine.Time.deltaTime);
            return math.nlerp(target, current, exp);
        }
    }

    #endregion
}
