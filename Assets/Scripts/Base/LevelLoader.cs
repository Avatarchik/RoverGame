using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    private const string FILL_TEXT_FORMAT = "{0}";

    public int levelToLoad = 0;

    public Image fillSprite;
    public Text fillText;

    private IEnumerator Load()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);

        while (async.progress < 0.9f)
        {
            fillSprite.fillAmount = async.progress;
            fillText.text = string.Format(FILL_TEXT_FORMAT, (Mathf.RoundToInt(async.progress * 100)).ToString());

            if (async.progress == 0.9f)
                yield return null;
        }
        Debug.Log(0);
        float elapsedTime = 0f;
        float desiredTime = 8f;

        while (elapsedTime < desiredTime)
        {
            Debug.Log(1);

            fillSprite.fillAmount = Mathf.Lerp(0.9f, 1f, elapsedTime / desiredTime);
            fillText.text = string.Format(FILL_TEXT_FORMAT, Mathf.RoundToInt((Mathf.Lerp(0.9f, 1f, elapsedTime / desiredTime) * 100)).ToString());

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log(2);

    }


    private void Start ()
    {
        StartCoroutine(Load());
	}
	
}
