using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Colorful;

namespace Sol
{
    public class MainMenu : Menu
    {
        public Button startGameButton;
        public Button quitbutton;
        public Glitch glitch;
        public CanvasGroup cg;
        public Texture2D cursorImage;

        public AudioSource audioSource;

        public float fadeTime = 3f;

        public override void Open()
        {
            base.Open();
        }


        public override void Close()
        {
            base.Close();
        }


        public void StartGame()
        {
            audioSource.Play();
            StartCoroutine(FadeOut());
        }


        public void QuitGame()
        {
            audioSource.Play();

            Debug.Log("quitting");

            StartCoroutine(FadeOut());
        }


        public void OpenOptions()
        {
            //nothing for now
        }


        private IEnumerator FadeOut(bool quit = false)
        {
            float elapsedTime = 0f;

            while(elapsedTime < fadeTime)
            {
                cg.alpha = 1f - (elapsedTime / fadeTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cg.alpha = 0;

            SceneManager.LoadScene(1);
            StopAllCoroutines();
            glitch.enabled = false;

            if (quit) Application.Quit();
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
        }


        private void Update()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);
        }


        private void Awake()
        {
            StartCoroutine(FadeIn());
            startGameButton.onClick.AddListener(StartGame);
            quitbutton.onClick.AddListener(QuitGame);
        }
    }
}

