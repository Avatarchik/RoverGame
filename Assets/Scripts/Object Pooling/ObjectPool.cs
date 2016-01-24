using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;

    private Dictionary<int, Pool> pools = new Dictionary<int, Pool>();


    public static bool Exists
    {
        get { return Instance != null; }
    }


    public static ObjectPool Instance
    {
        get
        {
            if(!instance && Application.isPlaying)
            {
                instance = FindObjectOfType<ObjectPool>() as ObjectPool;
                if (!instance) instance = Create("_Object Pool");

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    } 

    public static ObjectPool Create (string name)
    {
        return Create(name, null);
    }


    public static ObjectPool Create(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.parent = parent;

        return go.AddComponent<ObjectPool>();
    }


    public static void Clear (GameObject prefab=null)
    {
        if(Exists && prefab)
        {
            Pool pool = GetPool(prefab);
            pool.Clear();
        }
    }


    public static T Request<T>(T prefab) where T : Component
    {
        return Request(prefab.gameObject).GetComponent<T>();
    }


    public static T Request<T>(T prefab, Transform parent) where T : Component
    {
        return Request(prefab.gameObject, parent).GetComponent<T>();
    }


    public static T Request<T>(T prefab, Vector3 position) where T : Component
    {
        return Request(prefab.gameObject, position).GetComponent<T>();
    }


    public static T Request<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return Request(prefab.gameObject, position, rotation).GetComponent<T>();
    }


    public static T Request<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
    {
        return Request(prefab.gameObject, parent, position, rotation).GetComponent<T>();
    }


    public static GameObject Request(GameObject prefab)
    {
        return Request(prefab, null, Vector3.zero, Quaternion.identity);
    }


    public static GameObject Request(GameObject prefab, Transform parent)
    {
        return Request(prefab, parent, Vector3.zero, Quaternion.identity);
    }


    public static GameObject Request(GameObject prefab, Vector3 position)
    {
        return Request(prefab, null, position, Quaternion.identity);
    }


    public static GameObject Request(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Request(prefab, null, position, rotation);
    }


    public static GameObject Request(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        return GetPool(prefab).Request(prefab, parent, position, rotation);
    }



    private static Pool GetPool (GameObject go)
    {
        if (!Exists) return null;

        int poolId = ObjectPoolTag.GetPoolId(go);

        if (poolId == 0) poolId = go.GetInstanceID();

        if(!Instance.pools.ContainsKey (poolId))
        {
            Instance.pools[poolId] = Pool.Create(go.name + "[#" + poolId + "]", Instance.transform);
        }

        return Instance.pools[poolId];
    }
}
