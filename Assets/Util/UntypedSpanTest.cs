using UnityEngine;
using System;
using Klak.Util;

sealed class UntypedSpanTest : MonoBehaviour
{
    void Start()
    {
        var array = new int[] { 1, 2, 3, 4 };
        var span = new Span<int>(array);
        var untyped = span.GetRawSpan();
        foreach (var x in untyped.Span) Debug.Log(x);
    }
}
