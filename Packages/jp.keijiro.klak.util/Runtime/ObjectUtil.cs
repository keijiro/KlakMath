using UnityEngine;

namespace Klak.Util {

public static class ObjectUtil
{
    public static T TryDestroy<T>(T o) where T : Object
    {
        if (o == null) return null;
        if (Application.isPlaying)
            Object.Destroy(o);
        else
            Object.DestroyImmediate(o);
        return null;
    }
}

} // namespace Klak.Util
