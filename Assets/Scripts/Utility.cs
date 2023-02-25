using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static bool IsNullOrDestroyed(this System.Object obj)
    {

        if (object.ReferenceEquals(obj, null)) return true;

        if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;

        return false;
    }
}
