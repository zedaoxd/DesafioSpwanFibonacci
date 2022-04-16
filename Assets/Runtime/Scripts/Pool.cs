using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pool<T> where T : MonoBehaviour, IPooledObject
{
    [SerializeField] private Transform poolRoot;
    [SerializeField] private T prefab;
    [SerializeField] private int initialObjectCount = 10;

    private List<T> objects;

    public void Initialize()
    {
        objects = new List<T>(initialObjectCount);
        for (int i = 0; i < initialObjectCount; i++)
        {
            objects.Add(InstantiateObject());
        }
    }

    public T GetFromPool(Vector3 position, Quaternion rotation, Transform parent)
    {
        T obj;
        if (objects.Count > 0)
        {
            obj = objects[objects.Count - 1];
            objects.RemoveAt(objects.Count - 1);
        }
        else 
        {
            obj = InstantiateObject();
        }

        SetupObject(obj, position, rotation, parent);
        obj.gameObject.SetActive(true);
        obj.OnEnabledFromPool();

        return obj;
    }

    public void ReturnToPool(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(poolRoot);
            obj.OnDisabledFromPool();
            objects.Add(obj);
        }
    }

    private void SetupObject(T obj, Vector3 position, Quaternion rotation, Transform parent)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.SetParent(parent);
    }

    private T InstantiateObject()
    {
        var obj = UnityEngine.Object.Instantiate(prefab, poolRoot);
        obj.OnInstantiated();
        obj.gameObject.SetActive(false);
        return obj;
    }
}
