using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFlag : MonoBehaviour
{
    [SerializeField]
    private bool _isDebug = false;

    static public bool IsDebug(){
        return _instance._isDebug;
    }
    static private DebugFlag _instance;

    void Awake(){
        _instance = this;
    }
}
