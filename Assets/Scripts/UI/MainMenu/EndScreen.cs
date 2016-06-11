using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Colorful;

namespace Sol
{
    public class EndScreen : Menu
    {
        public CanvasGroup cg;
        public Texture2D cursorImage;

        public float fadeTime = 3f;

        public int levelToLoad = 0;

        public override void Open()
        {
            base.Open();
        }


        public override void Close()
        {
            base.Close();
        }


        private IEnumerator FadeOut()
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                cg.alpha = 1f - (elapsedTime / fadeTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cg.alpha = 0;

            SceneManager.LoadScene(levelToLoad);
            StopAllCoroutines();
        }


        private IEnumerator FadeIn()
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                cg.alpha = elapsedTime / fadeTime;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cg.alpha = 1;
            StartCoroutine(FadeOut());
        }


        private void Update()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);
        }


        private void Awake()
        {
            StartCoroutine(FadeIn());
        }
    }
}

