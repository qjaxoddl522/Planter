using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemLoader : MonoBehaviour
{
    private static SystemLoader _instance;
    private Dictionary<Type, IManager> services = new Dictionary<Type, IManager>();

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

    // 서비스 등록
    public void Register<T>(IManager service) where T : class
    {
        services[typeof(T)] = service;
    }

    // 서비스 가져오기
    public T Get<T>() where T : class
    {
        if (services.TryGetValue(typeof(T), out IManager service))
        {
            return service as T;
        }
        return null;
    }

    // 등록된 모든 매니저 Init()
    public void InitAll()
    {
        foreach (var service in services.Values)
        {
            service.Init();
        }
    }
}
