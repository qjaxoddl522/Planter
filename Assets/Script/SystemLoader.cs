using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemLoader : MonoBehaviour
{
    private static SystemLoader _instance;
    private Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static SystemLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("System Loader").AddComponent<SystemLoader>();
            }
            return _instance;
        }
    }

    // ���� ���
    public void Register<T>(T service) where T : class
    {
        services[typeof(T)] = service;
    }

    // ���� ��������
    public T Get<T>() where T : class
    {
        if (services.TryGetValue(typeof(T), out object service))
        {
            return service as T;
        }
        return null;
    }
}
