using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Sol
{
    public class EndDemo : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                //we need to end the demo.
                StartCoroutine(EndDemoCoroutine());
            }
        }


        private IEnumerator EndDemoCoroutine()
        {
            //TODO take out all these magic numbers!
            UIManager.GetMenu<FadeMenu>().Fade(0.5f, Color.clear, Color.black);
            GameObject.FindObjectOfType<PlayerStats>().DisableMovement();
            yield return new WaitForSeconds(0.5f);
            UIManager.GetMenu<MessageMenu>().Open("To Be Continued...");
            yield return new WaitForSeconds(3f);
        }
    }
}