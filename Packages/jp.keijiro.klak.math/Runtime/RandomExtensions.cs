// Extension methods for Random

using Unity.Mathematics;

namespace Klak.Math {

public static class RandomExtensions
{
    // On unit disk
    public static float2 NextFloat2OnDisk(ref this Random self)
      => self.NextFloat2Direction() * math.sqrt(self.NextFloat());

    // In unit sphere
    public static float3 NextFloat3InSphere(ref this Random self)
      => self.NextFloat3Direction() * math.pow(self.NextFloat(), 1.0f / 3);
}

} // namespace Klak.Math
