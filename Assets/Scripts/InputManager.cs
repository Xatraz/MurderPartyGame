using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager
{
    public static IInputSystem GetInputSystem()
    {
#if UNITY_STANDALONE
        return new PCInput();
        Debug.Log("PC");
#endif

#if UNITY_ANDROID || UNITY_EDITOR
        return new MobileInput();
        Debug.Log("Mob");
#endif

#if UNITY_WEBGL
        return new WebInput();
        Debug.Log("Web");
#endif
    }
}