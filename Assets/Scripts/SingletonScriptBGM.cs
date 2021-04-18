using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptBGM : MonoBehaviour
{
    static GameObject singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = gameObject;
            DontDestroyOnLoad(singleton);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
