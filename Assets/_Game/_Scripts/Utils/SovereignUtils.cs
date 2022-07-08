using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SovereignUtils
{
    //public 
    public static void Log(object s)
    {

#if UNITY_EDITOR
        Debug.Log(s);
#endif
    }

    public static void LogError(object s)
    {

#if UNITY_EDITOR
        Debug.LogError(s);
#endif
    }
}
