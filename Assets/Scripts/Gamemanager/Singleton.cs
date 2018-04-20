using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>:MonoBehaviour where T:Component
{
    public static T _instance;
    public static T getInstance
    {
        get
        {
            if(_instance==null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if(_instance ==null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = (T)obj.AddComponent(typeof(T));
                }
            }
            return _instance;
        }
    }
    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(_instance==null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
