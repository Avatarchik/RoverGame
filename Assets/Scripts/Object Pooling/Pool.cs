using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    int id = 0;
    public List<GameObject> used = new List<GameObject>();
    public List<GameObject> free = new List<GameObject>();

    private Transform instances;


    public static Pool Create(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.parent = parent;

        return go.AddComponent<Pool>();
    }


    public GameObject Request(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        if (id == 0) id = prefab.GetInstanceID ();

        if(id != prefab.GetInstanceID())
        {
            Debug.LogError(string.Format("Cannot create an isntance of {0} from pool {1}", prefab.name, id));
            return null;
        }

        GameObject instance;

        if(free.Count > 0)
        {
            instance = free[0];
            free.Remove(instance);
        }
        else
        {
            instance = (GameObject)GameObject.Instantiate(prefab);
        }

        PlaceObject(instance.transform, parent, position, rotation);

        used.Add(instance);
        instance.name = prefab.name;

        return instance;
    }


    public void Clear()
    {
        used.ForEach(delegate (GameObject go) { Destroy(go); });
        used.Clear();
        free.ForEach(delegate (GameObject go) { Destroy(go); });
        free.Clear();
    }


    public void PlaceObject (Transform child, Transform parent, Vector3 position, Quaternion rotation)
    {
        child.SetParent(parent, false);

        if(parent)
        {
            child.gameObject.layer = parent.gameObject.layer;
            child.localScale = Vector3.one;
            child.localPosition = position;
            child.localRotation = rotation;
        }
        else
        {
            child.position = position;
            child.rotation = rotation;
        }
    }
}
