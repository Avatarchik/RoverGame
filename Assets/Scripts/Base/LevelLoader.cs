using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    private const string FILL_TEXT_FORMAT = "{0}%";

    public int levelToLoad = 0;

    public Image fillSprite;
    public Text fillText;

    private IEnumerator Load()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);

        while(!async.isDone)
        {
            fillSprite.fillAmount = async.progress / 0.9f;
            fillText.text = string.Format(FILL_TEXT_FORMAT, (async.progress/0.9f * 100).ToString("F2"));
            yield return null;
        }
    }


    private void Start ()
    {
        StartCoroutine(Load());
	}
	
}
