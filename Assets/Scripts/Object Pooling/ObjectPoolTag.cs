using UnityEngine;
using System.Collections;

public class ObjectPoolTag : MonoBehaviour
{
    public int poolId = 0;

	public static ObjectPoolTag Apply(GameObject go, int id)
    {
        ObjectPoolTag tag = Get(go);
        if (!tag) tag = go.AddComponent<ObjectPoolTag>();

        tag.poolId = id;

        return tag;
    }


    public static ObjectPoolTag Get(GameObject go)
    {
        return go.GetComponent<ObjectPoolTag>();
    }


    public static int GetPoolId (GameObject go)
    {
        ObjectPoolTag tag = Get(go);

        return tag ? tag.poolId : 0;
    }
}
