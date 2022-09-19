// Tween with exponential interpolation

using Unity.Mathematics;

namespace Klak.Math {

public static class ExpTween
{
    #region float support

    public static float Step(float x, float target, float speed)
      => Step(x, target, speed, UnityEngine.Time.deltaTime);

    public static float Step(float x, float target, float speed, float dt)
      => math.lerp(target, x, math.exp(-speed * dt));

    #endregion

    #region float2 support

    public static float2 Step(float2 x, float2 target, float speed)
      => Step(x, target, speed, UnityEngine.Time.deltaTime);

    public static float2 Step(float2 x, float2 target, float speed, float dt)
      => math.lerp(target, x, math.exp(-speed * dt));

    #endregion

    #region float3 support

    public static float3 Step(float3 x, float3 target, float speed)
      => Step(x, target, speed, UnityEngine.Time.deltaTime);

    public static float3 Step(float3 x, float3 target, float speed, float dt)
      => math.lerp(target, x, math.exp(-speed * dt));

    #endregion

    #region float4 support

    public static float4 Step(float4 x, float4 target, float speed)
      => Step(x, target, speed, UnityEngine.Time.deltaTime);

    public static float4 Step(float4 x, float4 target, float speed, float dt)
      => math.lerp(target, x, math.exp(-speed * dt));

    #endregion

    #region quaternion support

    public static quaternion Step(quaternion x, quaternion target, float speed)
      => Step(x, target, speed, UnityEngine.Time.deltaTime);

    public static quaternion Step(quaternion x, quaternion target, float speed, float dt)
      => math.nlerp(target, x, math.exp(-speed * dt));

    #endregion
}

} // namespace Klak.Math
