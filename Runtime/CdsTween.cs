// Tween with critically damped spring model

using Unity.Mathematics;

namespace Klak.Math
{
    #region float support

    public partial struct CdsTween
    {
        public float Current { get; set; }
        public float Speed { get; set; }

        float _velocity;

        public CdsTween(float initial, float speed)
        {
            Current = initial;
            Speed = speed;
            _velocity = 0;
        }

        public float Step(float target)
        {
            Current = Step(Current, target, ref _velocity, Speed);
            return Current;
        }
    }

    #endregion

    #region float2 support

    public struct CdsTween2
    {
        public float2 Current { get; set; }
        public float Speed { get; set; }

        float2 _velocity;

        public CdsTween2(float2 initial, float speed)
        {
            Current = initial;
            Speed = speed;
            _velocity = 0;
        }

        public float2 Step(float2 target)
        {
            Current = CdsTween.Step(Current, target, ref _velocity, Speed);
            return Current;
        }
    }

    #endregion

    #region float3 support

    public struct CdsTween3
    {
        public float3 Current { get; set; }
        public float Speed { get; set; }

        float3 _velocity;

        public CdsTween3(float3 initial, float speed)
        {
            Current = initial;
            Speed = speed;
            _velocity = 0;
        }

        public float3 Step(float3 target)
        {
            Current = CdsTween.Step(Current, target, ref _velocity, Speed);
            return Current;
        }
    }

    #endregion

    #region float4 support

    public struct CdsTween4
    {
        public float4 Current { get; set; }
        public float Speed { get; set; }

        float4 _velocity;

        public CdsTween4(float4 initial, float speed)
        {
            Current = initial;
            Speed = speed;
            _velocity = 0;
        }

        public float4 Step(float4 target)
        {
            Current = CdsTween.Step(Current, target, ref _velocity, Speed);
            return Current;
        }
    }

    #endregion

    #region quaternion support

    public struct CdsTweenQ
    {
        public quaternion Current { get; set; }
        public float Speed { get; set; }

        float4 _velocity;

        public CdsTweenQ(quaternion initial, float speed)
        {
            Current = initial;
            Speed = speed;
            _velocity = 0;
        }

        public quaternion Step(quaternion target)
        {
            Current = CdsTween.Step(Current, target, ref _velocity, Speed);
            return Current;
        }
    }

    #endregion

    #region Static method implementation

    public partial struct CdsTween
    {
        public static float Step
            (float current, float target, ref float velocity, float speed)
        {
            var dt = UnityEngine.Time.deltaTime;
            var n1 = velocity - (current - target) * (speed * speed * dt);
            var n2 = 1 + speed * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static float2 Step
            (float2 current, float2 target, ref float2 velocity, float speed)
        {
            var dt = UnityEngine.Time.deltaTime;
            var n1 = velocity - (current - target) * (speed * speed * dt);
            var n2 = 1 + speed * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static float3 Step
            (float3 current, float3 target, ref float3 velocity, float speed)
        {
            var dt = UnityEngine.Time.deltaTime;
            var n1 = velocity - (current - target) * (speed * speed * dt);
            var n2 = 1 + speed * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static float4 Step
            (float4 current, float4 target, ref float4 velocity, float speed)
        {
            var dt = UnityEngine.Time.deltaTime;
            var n1 = velocity - (current - target) * (speed * speed * dt);
            var n2 = 1 + speed * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static quaternion Step
            (quaternion current, quaternion target,
             ref float4 velocity, float speed)
        {
            // We can use either of vtarget/-vtarget. Use closer one.
            if (math.dot(current, target) < 0) target.value *= -1;
            var res = Step(current.value, target.value, ref velocity, speed);
            return math.normalize(math.quaternion(res));
        }
    }

    #endregion
}
