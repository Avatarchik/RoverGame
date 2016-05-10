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

    /// <summary>
    /// Create instance of new object pool by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ObjectPool Create (string name)
    {
        return Create(name, null);
    }

    /// <summary>
    /// Create instance of new object pool by name under specified parent
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static ObjectPool Create(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.parent = parent;

        return go.AddComponent<ObjectPool>();
    }

    /// <summary>
    /// Clear out pool's cached objects
    /// </summary>
    /// <param name="prefab"></param>
    public static void Clear (GameObject prefab=null)
    {
        if(Exists && prefab)
        {
            Pool pool = GetPool(prefab);
            pool.Clear();
        }
    }

    /// <summary>
    /// Request an object of type <T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static T Request<T>(T prefab) where T : Component
    {
        return Request(prefab.gameObject).GetComponent<T>();
    }

    /// <summary>
    /// Request object of type T to specified parent
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static T Request<T>(T prefab, Transform parent) where T : Component
    {
        return Request(prefab.gameObject, parent).GetComponent<T>();
    }

    /// <summary>
    /// Request object of type T to specified position
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static T Request<T>(T prefab, Vector3 position) where T : Component
    {
        return Request(prefab.gameObject, position).GetComponent<T>();
    }

    /// <summary>
    /// Request object of type T to specified position and rotation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static T Request<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return Request(prefab.gameObject, position, rotation).GetComponent<T>();
    }

    /// <summary>
    /// Request object of type T to specified position, rotation, and parent
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static T Request<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
    {
        return Request(prefab.gameObject, parent, position, rotation).GetComponent<T>();
    }

    /// <summary>
    /// Request instance of pooled prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject Request(GameObject prefab)
    {
        return Request(prefab, null, Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Request instance of pooled prefab to specified parent
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject Request(GameObject prefab, Transform parent)
    {
        return Request(prefab, parent, Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Request instance of pooled prefab to specified position
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject Request(GameObject prefab, Vector3 position)
    {
        return Request(prefab, null, position, Quaternion.identity);
    }

    /// <summary>
    /// Request instance of pooled prefab to specified position and rotation
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static GameObject Request(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Request(prefab, null, position, rotation);
    }

    /// <summary>
    /// Request instance of pooled prefab to specified position, rotation, and parent
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static GameObject Request(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        return GetPool(prefab).Request(prefab, parent, position, rotation);
    }

    /// <summary>
    /// Recycle object in use for re-use
    /// </summary>
    /// <param name="prefab"></param>
    public static void Recycle(GameObject prefab)
    {
        GetPool(prefab).Recycle(prefab);
    }


    /// <summary>
    /// Get/create pool on specified game object
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
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