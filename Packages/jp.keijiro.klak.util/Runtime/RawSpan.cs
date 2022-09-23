using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;

namespace Klak.Util {

// Blittable form of Span
public unsafe readonly ref struct RawSpan<T> where T : unmanaged
{
    public readonly void* Pointer;
    public readonly int Length;

    public Span<T> Span
      =>new Span<T>(Pointer, Length);

    public Span<T> GetSpan(int ext = 0)
      => new Span<T>(Pointer, Length + ext);

    public RawSpan(void* ptr, int len)
    {
        Pointer = ptr;
        Length = len;
    }
}

public static class RawSpanExtensions
{
    unsafe static void*
      GetUnsafePtr<T>(this NativeArray<T> array) where T : unmanaged
        => NativeArrayUnsafeUtility.GetUnsafePtr(array);

    public unsafe static Span<T>
      GetSpan<T>(this NativeArray<T> array) where T : unmanaged
        => new Span<T>(GetUnsafePtr(array), array.Length);

    public unsafe static RawSpan<T>
      GetRawSpan<T>(this Span<T> span) where T : unmanaged
    {
        fixed (T* p = span) return new RawSpan<T>(p, span.Length);
    }

    public unsafe static RawSpan<T>
      GetRawSpan<T>(this NativeArray<T> array) where T : unmanaged
        => new RawSpan<T>(GetUnsafePtr(array), array.Length);
}

} // namespace Klak.Util
