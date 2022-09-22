using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;

namespace Klak.Util {

// Used to pass Span<T> to Burst functions
public unsafe readonly ref struct UntypedSpan
{
    public readonly void* Pointer;
    public readonly int Length;

    public Span<T> GetTyped<T>(int ext = 0)
      => new Span<T>(Pointer, Length + ext);

    public UntypedSpan(void* ptr, int len)
    {
        Pointer = ptr;
        Length = len;
    }
}

public static class UnsafeExtensions
{
    public unsafe static void*
      GetUnsafePtr<T>(this NativeArray<T> array) where T : unmanaged
        => NativeArrayUnsafeUtility.GetUnsafePtr(array);

    public unsafe static Span<T>
      GetSpan<T>(this NativeArray<T> array) where T : unmanaged
        => new Span<T>(GetUnsafePtr(array), array.Length);

    public unsafe static UntypedSpan
      GetUntyped<T>(this Span<T> span) where T : unmanaged
    {
        fixed (T* p = span) return new UntypedSpan(p, span.Length);
    }

    public unsafe static UntypedSpan
      GetUntypedSpan<T>(this NativeArray<T> array) where T : unmanaged
        => new UntypedSpan(GetUnsafePtr(array), array.Length);
}

} // namespace Klak.Util
