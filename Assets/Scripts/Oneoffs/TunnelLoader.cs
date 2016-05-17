using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TunnelLoader : MonoBehaviour
{
    public int levelToLoad = 0;



    void Start ()
    {
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Additive);
	}
}
