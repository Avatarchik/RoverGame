using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class FadeMenu : Menu
    {
        public Image background;


        public override void Close()
        {
            base.Close();
        }

        public override void Open()
        {
            base.Open();
        }


        public void Fade(float fadeTime, Color from, Color to)
        {
            if (!IsActive) Open();
            StartCoroutine(FadeCoroutine(fadeTime, from, to));
        }


        private IEnumerator FadeCoroutine(float fadeTime, Color from, Color to)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                background.color = Color.Lerp(from, to, elapsedTime / fadeTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            background.color = to;
        }

    }

}
