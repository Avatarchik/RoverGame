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
        public Glitch glitch;

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
            SceneManager.LoadScene(1);
            StopAllCoroutines();
            glitch.enabled = false;
        }


        public void QuitGame()
        {
            Application.Quit();
        }


        public void OpenOptions()
        {
            //nothing for now
        }


        private IEnumerator GlitchOut()
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            glitch.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.3f, 1f));
            glitch.enabled = false;

            StartCoroutine(GlitchOut());
        }


        private void Awake()
        {
            StartCoroutine(GlitchOut());
            startGameButton.onClick.AddListener(StartGame);
        }
    }
}

