using UnityEngine;
using System.Collections;

public class Harvesting : MonoBehaviour
{
    public GameObject root;
    public bool open;


    public void Toggle()
    {
        open = !open;
        root.SetActive(open);
    }


    private IEnumerator ScanTier()
    {
        yield return null;
    }
}
