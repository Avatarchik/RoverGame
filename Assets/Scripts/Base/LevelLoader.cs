using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public int levelToLoad = 0;

    public Image fillSprite;

    private IEnumerator Load()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);

        while(!async.isDone)
        {
            fillSprite.fillAmount = async.progress / 0.9f;
            yield return null;
        }
    }


    private void Start ()
    {
        StartCoroutine(Load());
	}
	
}
